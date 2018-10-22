
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StemController : MonoBehaviour
{
    Vector3 startPos, endPos, direction;

    [Range(0.05f, 1000f)]
    [SerializeField] private float _rotateSpeed = 0.3f;

    void FixedUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPos = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            endPos = Input.GetTouch(0).position;
            direction = startPos - endPos;
            transform.Rotate(Vector3.up, direction.normalized.x * _rotateSpeed * Time.deltaTime);
        }
    }
}