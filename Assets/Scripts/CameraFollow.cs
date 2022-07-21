using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Script simples de CameraFollow, adaptado do 3D.
*/
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followedObject;
    [SerializeField] private float smoothingFactor = 1.3f;
    [SerializeField] private float distanceFromPlayer = -10f;

    private Vector3 targetLocation = new Vector3();

    private void FixedUpdate() {
        targetLocation = followedObject.transform.position;
        targetLocation.z = distanceFromPlayer; // Como isso é 2D, tenho que manter o Z sempre em -10.
        transform.position = Vector3.Lerp(transform.position, targetLocation, smoothingFactor);
    }
}
