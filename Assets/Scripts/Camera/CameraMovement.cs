using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;

    public float rotationSpeed = 5f;
    private bool isRotating = false;
    private bool isDone;

    private void Update()
    {
        FollowPlayer();
        Inputs();
	}

    private void FollowPlayer()
    {
        if (target == null) target = GameObject.FindWithTag("Player");
        transform.position = target.transform.position;
    }
    private void Inputs()
    {
        if (isRotating) return;
        if (Input.GetKey(KeyCode.Q)) StartCoroutine(RotateTo(Quaternion.Euler(0, -90, 0)));
        if (Input.GetKey(KeyCode.E)) StartCoroutine(RotateTo(Quaternion.Euler(0, 90, 0)));
    }

    IEnumerator RotateTo(Quaternion Angles)
    {
        isRotating = true;
        isDone = false;
        Quaternion targetRotation = transform.rotation * Angles;

        while (isDone == false)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (transform.rotation == targetRotation) isDone = true;
            if (isDone) transform.rotation = targetRotation;
            yield return null;
        }

        isRotating = false;
    }
}
