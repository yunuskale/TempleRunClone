using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartingMotion : MonoBehaviour
{
    void Start()
    {
        transform.DOLocalMove(new Vector3(-2.4f, 5, 15), 1.3f).OnComplete(() =>
        {
            transform.DOLocalRotate(new Vector3(6.29f, 20.89f, 0), 1);
        });
        Destroy(gameObject, 2.5f);
    }
}
