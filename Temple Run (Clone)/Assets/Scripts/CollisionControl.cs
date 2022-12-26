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
        // Platformlardaki kapýlardan geçtiðinde yeni platform oluþturur
        if(other.CompareTag("platform")) // Düz ya da tek dönüþlü platform
        {
            int r = Random.Range(0, platforms.Length);
            GameObject platform = Instantiate(platforms[r], other.transform.parent.parent.GetChild(0)); // Platformu o an üzerinde bulunulan platformun nextinde oluþturur (konum ve açýsýný bu sayede alýr)
            platform.transform.SetParent(platform.transform.root); // O anki platform destroy edildiðinde içindeki platformun da yok olmamasý için genel platforms parentine alýr
            Destroy(platform, 30f);
            if(Time.timeScale <= 5)
            {
                Time.timeScale += 0.1f;
            }
        }
        else if(other.CompareTag("platformRight")) // Çift dönüþlü platformun sað kapýsý
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
        else if (other.CompareTag("platformLeft")) // Çift dönüþlü platformun sol kapýsý
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
            // Daha önceden sað veya sol komutu aldýysa turn nesnesinin içinden geçtiðinde ilk iki koþul çalýþýr
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
            // Daha önceden bir komut almadýysa turn nesnesinden geçtiðinde çok kýsa bir süreliðine dönüþe izin verir
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
        // Ýlk iki koþul en son üzerinde bulunulan platformu hafýzaya alýr
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
    public void Turn() // Karakterimiz döndüðünde o platformdaki sað ve sol sýnýrlarýný belirler
    {
        if (platform.CompareTag("platform") && (movement.turnedRight || movement.turnedLeft)) // Düz ya da tek dönüþlü platform
        {
            movement.rightClamp = platform.transform.parent.GetChild(3).transform.position; // Platformda sað tarafa yerleþtirilen kutunun konumunu döndürür
            movement.leftClamp = platform.transform.parent.GetChild(2).transform.position; // Platformda sol tarafa yerleþtirilen kutunun konumunu döndürür
        }
        else if (platform.CompareTag("platform2")) // Çift dönüþlü platform
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
