using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour
{
    public GameObject[] Lasers;

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        for (int i = 0; i < Lasers.Length; i++)
        {
            if (Physics.Raycast(Lasers[i].transform.position, Lasers[i].transform.forward, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    /*if (hit.collider.gameObject.GetComponent<ResetPlayer>())
                    {
                        hit.collider.gameObject.GetComponent<ResetPlayer>().Reset();
                    }
                    else
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }*/
                }
            }
        }
    }
}
