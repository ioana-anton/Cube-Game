using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] KeyCode respawnKey = KeyCode.R;
    private GameObject player;
    private Transform respawnPoint;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        respawnPoint = GameObject.FindWithTag("Respawn").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(respawnKey))
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        // Reduce Momentum
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 300f * Time.deltaTime);

        // Spawn Player
        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;
    }
}
