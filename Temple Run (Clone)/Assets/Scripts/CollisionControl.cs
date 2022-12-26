using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    public GameObject[] platforms;
    private Movement movement;
    public bool onGround;
    private void Start()
    {
        movement = GetComponent<Movement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Platformlardaki kap�lardan ge�ti�inde yeni platform olu�turur
        if(other.CompareTag("platform")) // D�z ya da tek d�n��l� platform
        {
            int r = Random.Range(0, platforms.Length);
            GameObject platform = Instantiate(platforms[r], other.transform.parent.parent.GetChild(0)); // Platformu o an �zerinde bulunulan platformun nextinde olu�turur (konum ve a��s�n� bu sayede al�r)
            platform.transform.SetParent(platform.transform.root); // O anki platform destroy edildi�inde i�indeki platformun da yok olmamas� i�in genel platforms parentine al�r
            Destroy(platform, 30f);
            if(Time.timeScale <= 5)
            {
                Time.timeScale += 0.1f;
            }
        }
        else if(other.CompareTag("platformRight")) // �ift d�n��l� platformun sa� kap�s�
        {
            int r = Random.Range(0, platforms.Length);
            GameObject platform = Instantiate(platforms[r], other.transform.parent.parent.GetChild(0));
            platform.transform.SetParent(platform.transform.root);
            Destroy(platform, 30f);
            if (Time.timeScale <= 5)
            {
                Time.timeScale += 0.1f;
            }
        }
        else if (other.CompareTag("platformLeft")) // �ift d�n��l� platformun sol kap�s�
        {
            int r = Random.Range(0, platforms.Length);
            GameObject platform = Instantiate(platforms[r], other.transform.parent.parent.GetChild(1));
            platform.transform.SetParent(platform.transform.root);
            Destroy(platform, 30f);
            if (Time.timeScale <= 5)
            {
                Time.timeScale += 0.1f;
            }
        }
        else if (other.CompareTag("turn"))
        {
            Destroy(other);
            // Daha �nceden sa� veya sol komutu ald�ysa turn nesnesinin i�inden ge�ti�inde ilk iki ko�ul �al���r
            if(movement.turnedRight)
            {
                Turn();
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 90, 0f);
            }
            else if(movement.turnedLeft)
            {
                Turn();
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y - 90, 0f);
            }
            // Daha �nceden bir komut almad�ysa turn nesnesinden ge�ti�inde �ok k�sa bir s�reli�ine d�n��e izin verir
            else
            {
                movement.turnAllow = true;
                Invoke(nameof(TurnAllowCancel), 0.4f);
            }
        }
    }

    GameObject platform;
    private void OnCollisionEnter(Collision collision)
    {
        // �lk iki ko�ul en son �zerinde bulunulan platformu haf�zaya al�r
        if(collision.gameObject.CompareTag("platform")) 
        {
            platform = collision.gameObject;
        }
        else if(collision.gameObject.CompareTag("platform2"))
        {
            platform = collision.gameObject;
        }
        else if(collision.gameObject.CompareTag("obstacle"))
        {
            movement.Fall();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            onGround = false;
        }
        else if (collision.gameObject.CompareTag("platform2"))
        {
            onGround = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            onGround = true;
        }
        else if (collision.gameObject.CompareTag("platform2"))
        {
            onGround = true;
        }
    }
    public void Turn() // Karakterimiz d�nd���nde o platformdaki sa� ve sol s�n�rlar�n� belirler
    {
        if (platform.CompareTag("platform") && (movement.turnedRight || movement.turnedLeft)) // D�z ya da tek d�n��l� platform
        {
            movement.rightClamp = platform.transform.parent.GetChild(3).transform.position; // Platformda sa� tarafa yerle�tirilen kutunun konumunu d�nd�r�r
            movement.leftClamp = platform.transform.parent.GetChild(2).transform.position; // Platformda sol tarafa yerle�tirilen kutunun konumunu d�nd�r�r
        }
        else if (platform.CompareTag("platform2")) // �ift d�n��l� platform
        {
            if (movement.turnedRight)
            {
                movement.rightClamp = platform.transform.parent.GetChild(3).transform.position;
                movement.leftClamp = platform.transform.parent.GetChild(2).transform.position;
            }
            else if (movement.turnedLeft)
            {
                movement.rightClamp = platform.transform.parent.GetChild(2).transform.position;
                movement.leftClamp = platform.transform.parent.GetChild(3).transform.position;
            }
        }
    }
    private void TurnAllowCancel()
    {
        movement.turnAllow = false;
    }
}
