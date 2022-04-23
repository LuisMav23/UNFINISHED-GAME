using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField] public Transform followTarget;
    [SerializeField] public float smoothSpeed = 0.00f;

    void FixedUpdate() {
        Vector3 targetPos = new Vector3(followTarget.position.x, followTarget.position.y, -15.00f);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        gameObject.transform.position = smoothedPosition;
    }
}
