using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] target;
    public bool isLooping = true;

    int index = 0;

    [Header("Settings")]
    public float speed = 2f;
    public float waitTime = 2f;
    public float minDist2Target = 0.001f;

    bool isWaiting = false;

    // ─────────────────────────────────────────────────────────────────────────────────────────────────────────

    private void Update()
    {
        Vector3 a = transform.position;
        Vector3 b = target[index].position;

        if (isWaiting == false)
        {
            transform.position = Vector3.MoveTowards(a, b, speed * Time.deltaTime);
        }

        if (Vector3.Distance(a, b) <= minDist2Target)
        {
            if (isLooping)
            {
                StartCoroutine(SetIndex());
            }
        }
    }
    IEnumerator SetIndex()
    {
        isWaiting = true;

        index++;
        if (index >= target.Length)
        {
            index = 0;
        }

        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }
}
