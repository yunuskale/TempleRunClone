using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    public GameObject[] platforms;
    private Movement movement;
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
            GameObject platform = Instantiate(platforms[r], other.transform.parent.parent.GetChild(0));
            platform.transform.SetParent(platform.transform.root);
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
    public void Turn() // Platformdaki sa� ve sol s�n�rlar� geri d�nd�r�r
    {
        if (platform.CompareTag("platform") && (movement.turnedRight || movement.turnedLeft)) // D�z ya da tek d�n��l� platform
        {
            movement.rightClamp = platform.transform.parent.GetChild(3).transform.position;
            movement.leftClamp = platform.transform.parent.GetChild(2).transform.position;
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
