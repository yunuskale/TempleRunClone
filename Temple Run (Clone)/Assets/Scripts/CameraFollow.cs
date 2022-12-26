using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    private void LateUpdate()
    {
        if(GameManager.isPlay)
        {

            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, playerTransform.eulerAngles, 0.3f);

            if (playerTransform.eulerAngles.y < 2) // Düz yönde gidildiðinde çalýþýr
            {
                transform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z - 7f);
            }
            else if (playerTransform.eulerAngles.y < 92) // Sað yönde gidildiðinde çalýþýr
            {
                transform.position = new Vector3(playerTransform.position.x - 7f, transform.position.y, playerTransform.position.z);
            }
            else if (playerTransform.eulerAngles.y < 182) // Ters yönde gidildiðinde çalýþýr
            {
                transform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z + 7f);
            }
            else if (playerTransform.eulerAngles.y < 272) // Sol yönde gidildiðinde çalýþýr
            {
                transform.position = new Vector3(playerTransform.position.x + 7f, transform.position.y, playerTransform.position.z);
            }
        } 
    }
}
