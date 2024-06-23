using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform Tf;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float moveSpeed = 20;

    // Update is called once per frame
    void LateUpdate()
    {
        //TODO fix:
        Tf.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * moveSpeed);
    }
}
