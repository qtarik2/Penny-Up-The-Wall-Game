using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PositionChanging : MonoBehaviour
{
    [SerializeField] private Vector3 endPos;

    private void Start()
    {
        transform.DOMove(endPos, 0.5f);
    }
}
