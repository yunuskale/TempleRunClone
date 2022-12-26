using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float horizontalSpeed;
    private Rigidbody rb;
    private Animator anim;
    private CollisionControl collisionControl;
    public GameManager gameManager;
    public Vector3 leftClamp, rightClamp;
    public Vector3 test;
    public bool turnAllow, turnedRight, turnedLeft, jumped;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        collisionControl = GetComponent<CollisionControl>();
    }
    private void FixedUpdate()
    {
        if(GameManager.isPlay)
        {
            Vector3 location = rb.position;
            if (transform.eulerAngles.y < 2) // Düz yönde gidildiðinde çalýþýr
            {
                Vector3 forward = Vector3.forward * runSpeed * Time.fixedDeltaTime;
                float horizontal = Input.acceleration.x * horizontalSpeed * Time.deltaTime; // Mobil cihazýn saða sola eðikliðini geri döndürür.
                location += forward;
                location.x += horizontal;
                location.x = Mathf.Clamp(location.x, leftClamp.x, rightClamp.x); // CollisionControl den gelen platforma özel yataydaki sýnýr deðerlere oyuncuyu hapseder
            }
            else if (transform.eulerAngles.y < 92) // Sað yönde gidildiðinde çalýþýr
            {
                Vector3 forward = Vector3.right * runSpeed * Time.fixedDeltaTime;
                float horizontal = Input.acceleration.x * horizontalSpeed * Time.deltaTime;
                location += forward;
                location.z -= horizontal;
                location.z = Mathf.Clamp(location.z, rightClamp.z, leftClamp.z);
            }
            else if (transform.eulerAngles.y < 182) // Ters yönde gidildiðinde çalýþýr
            {
                Vector3 forward = Vector3.back * runSpeed * Time.fixedDeltaTime;
                float horizontal = Input.acceleration.x * horizontalSpeed * Time.deltaTime;
                location += forward;
                location.x -= horizontal;
                location.x = Mathf.Clamp(location.x, rightClamp.x, leftClamp.x);
            }
            else if (transform.eulerAngles.y < 272) // Sol yönde gidildiðinde çalýþýr
            {
                Vector3 forward = Vector3.left * runSpeed * Time.fixedDeltaTime;
                float horizontal = Input.acceleration.x * horizontalSpeed * Time.deltaTime;
                location += forward;
                location.z += horizontal;
                location.z = Mathf.Clamp(location.z, leftClamp.z, rightClamp.z);
            }

            rb.position = location;

            if (rb.position.y < 1.7f)
            {
                gameManager.GameOver();
            }
        }
    }
    void Update()
    {
        if(Input.touchCount > 0 && GameManager.isPlay)
        {
            Touch finger = Input.GetTouch(0);
            if(finger.deltaPosition.y > 20 && collisionControl.onGround)
            {
                anim.Play("Jump");
                rb.AddForce(new Vector3(0f, jumpSpeed, 0f), ForceMode.Impulse);
            }

            if(finger.deltaPosition.x > 20 && turnAllow)
            {
                turnAllow = false;
                turnedRight = true;
                collisionControl.Turn();
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 90, 0f);
                Invoke(nameof(TurnedCancel), 0.1f);
            }
            else if (finger.deltaPosition.x > 20)
            {
                turnedRight = true;
                Invoke(nameof(TurnedCancel), 1f);
            }

            if (finger.deltaPosition.x < -20 && turnAllow)
            {
                turnAllow = false;
                turnedLeft = true;
                collisionControl.Turn();
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y - 90, 0f);
                Invoke(nameof(TurnedCancel), 0.1f);
            }
            else if (finger.deltaPosition.x < -20)
            {
                turnedLeft = true;
                Invoke(nameof(TurnedCancel), 1f);
            }
        }
        //if(GameManager.isPlay)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space) && collisionControl.onGround)
        //    {
        //        anim.Play("Jump");
        //        rb.AddForce(new Vector3(0f, jumpSpeed, 0f), ForceMode.Impulse);
        //    }
        //
        //    if (Input.GetKeyDown(KeyCode.RightArrow) && turnAllow)
        //    {
        //        turnAllow = false;
        //        turnedRight = true;
        //        collisionControl.Turn();
        //        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 90, 0f);
        //        Invoke(nameof(TurnedCancel), 0.1f);
        //    }
        //    else if (Input.GetKeyDown(KeyCode.RightArrow))
        //    {
        //        turnedRight = true;
        //        Invoke(nameof(TurnedCancel), 1f);
        //    }
        //
        //    if (Input.GetKeyDown(KeyCode.LeftArrow) && turnAllow)
        //    {
        //        turnAllow = false;
        //        turnedLeft = true;
        //        collisionControl.Turn();
        //        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y - 90, 0f);
        //        Invoke(nameof(TurnedCancel), 0.1f);
        //    }
        //    else if (Input.GetKeyDown(KeyCode.LeftArrow))
        //    {
        //        turnedLeft = true;
        //        Invoke(nameof(TurnedCancel), 1f);
        //    }
        //}
    }
    public void Fall()
    {
        gameManager.GameOver();
        GetComponent<CapsuleCollider>().center = new Vector3(0, 1.6f, 0.69f);
        anim.Play("Fall");
        if (transform.eulerAngles.y < 2)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(0, 0, -300));
        }
        else if (transform.eulerAngles.y < 92)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(-300, 0, 0));
        }
        else if (transform.eulerAngles.y < 182)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(0, 0, 300));
        }
        else if (transform.eulerAngles.y < 272)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(300, 0, 0));
        }
    }
    private void TurnedCancel()
    {
        turnedRight = false;
        turnedLeft = false;
    }
}
