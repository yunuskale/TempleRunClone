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
            transform.eulerAngles = playerTransform.eulerAngles;

            if (playerTransform.eulerAngles.y < 2) // D�z y�nde gidildi�inde �al���r
            {
                transform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z - 7f);
            }
            else if (playerTransform.eulerAngles.y < 92) // Sa� y�nde gidildi�inde �al���r
            {
                transform.position = new Vector3(playerTransform.position.x - 7f, transform.position.y, playerTransform.position.z);
            }
            else if (playerTransform.eulerAngles.y < 182) // Ters y�nde gidildi�inde �al���r
            {
                transform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z + 7f);
            }
            else if (playerTransform.eulerAngles.y < 272) // Sol y�nde gidildi�inde �al���r
            {
                transform.position = new Vector3(playerTransform.position.x + 7f, transform.position.y, playerTransform.position.z);
            }
        } 
    }
}
