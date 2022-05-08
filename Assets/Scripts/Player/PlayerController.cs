using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Movement")]
    public float speed = 300;
    public float remainingAngle;
    private bool isMoving = false;

    [Header("GroundCheck")]
    public LayerMask groundMask;
    public float groundDist = 0.5f;
    public bool isGrounded;
    private float smoothPosSpeed = 1.5f;

    [Header("PlatformCheck")]
    public bool isPlatform;
    public LayerMask platformMask;
    public float platformDist = 1.0f;
    public bool isWait;
    public Transform target;

    [Header("Raycasts")]
    [SerializeField] float rayLength = 1.4f;
    [SerializeField] float rayOffsetX = 0f;
    [SerializeField] float rayOffsetY = 0f;
    [SerializeField] float rayOffsetZ = 0f;
    private Vector3 yOffset;
    private Vector3 zOffset;
    private Vector3 xOffset;
    private Vector3 zOrigin;
    private Vector3 xOrigin;



    #endregion

    private void Update()
    {
        Raycasts();
        Movement();
        AdjustPos();

    }

    #region Movement
    private void Raycasts()
    {
        // Set the ray positions every frame

        yOffset = transform.position + Vector3.up * rayOffsetY;
        zOffset = Vector3.forward * rayOffsetZ;
        xOffset = Vector3.right * rayOffsetX;

        zOrigin = yOffset + xOffset;
        xOrigin = yOffset + zOffset;

        // Draw Debug Rays

        Debug.DrawLine(
                zOrigin,
                zOrigin + Vector3.forward * rayLength,
                Color.red,
                Time.deltaTime);
        Debug.DrawLine(
                zOrigin,
                zOrigin + Vector3.back * rayLength,
                Color.red,
                Time.deltaTime);
        Debug.DrawLine(
                xOrigin,
                xOrigin + Vector3.left * rayLength,
                Color.red,
                Time.deltaTime);
        Debug.DrawLine(
                xOrigin,
                xOrigin + Vector3.right * rayLength,
                Color.red,
                Time.deltaTime);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, platformDist);
    }

    private void Movement()
    {
        if (isMoving) return;

        if (Input.GetKey(KeyCode.W))
            StartCoroutine(Roll(Vector3.forward));

        else if (Input.GetKey(KeyCode.S))
            StartCoroutine(Roll(Vector3.back));

        else if (Input.GetKey(KeyCode.A))
            StartCoroutine(Roll(Vector3.left));

        else if (Input.GetKey(KeyCode.D))
            StartCoroutine(Roll(Vector3.right));
    }

    public IEnumerator Roll(Vector3 direction)
    {
        // Check if the Player can move
        bool CanMove(Vector3 direction)
        {
            if (direction.z != 0)
            {
                if (Physics.Raycast(zOrigin, direction, rayLength)) return false;
            }
            else if (direction.x != 0)
            {
                if (Physics.Raycast(xOrigin, direction, rayLength)) return false;
            }
            return true;
        }


        // Roll
        if (CanMove(direction))
        {
            isMoving = true;

            remainingAngle = 90;
            Vector3 point = transform.position + (Vector3.down + direction) / 2;
            Vector3 axis = Vector3.Cross(Vector3.up, direction);

            while (remainingAngle > 0)
            {
                float rotationAngle = Mathf.Min(remainingAngle, speed * Time.deltaTime);
                transform.RotateAround(point, axis, rotationAngle);
                remainingAngle -= rotationAngle;
                yield return null;
            }
            isMoving = false;
        }
        else { Debug.Log("Could not move."); }
    }

    // CheckSphere
    public void CheckPlatform(Vector3 direction, bool val)
    {
        isWait = val;
        isPlatform = Physics.CheckSphere(transform.position, platformDist, platformMask);
        if (isPlatform)
        {
            Debug.Log("Platform in Range");
            if (isWait)
            {
                if (isMoving) return;
                // WaitForPlatform(direction);

                isWait = false;
            }
        }
    }


    /*

    public IEnumerator WaitForGround(Vector3 direction)
    {

        isWait = true;
        if (isWait)
        {
            yield return new WaitUntil(() => CanMove(direction) == true);
            // yield return new WaitForSeconds(0.5f);
            StartCoroutine(Roll(direction));
            isWait = false;
        }
    }*/

    public IEnumerator WaitForPlatform(Vector3 direction, bool val)
    {
        isWait = true;
        if (isWait)
        {
            yield return new WaitUntil(() => Physics.CheckSphere(transform.position, platformDist, platformMask) == true);
            yield return new WaitForSeconds(0.75f);
            StartCoroutine(Roll(direction));
            isWait = false;
        }
    }

    private void AdjustPos()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDist, groundMask);

        if (isGrounded && remainingAngle == 0)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Round(pos.x);
            pos.y = Mathf.Round(pos.y);
            pos.z = Mathf.Round(pos.z);
            transform.position = Vector3.Lerp(transform.position, pos, smoothPosSpeed * Time.deltaTime);
        }
    }
    #endregion
}