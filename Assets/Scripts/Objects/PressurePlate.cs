using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private Animator Animation;
    public string Bool;

    public Animator TriggerAnimation;
    public string TriggerBool;
    public bool Wait;
    public float waitTime = 2f;

    private void Start()
    {
        Animation = GetComponent<Animator>();
    }
    private void OnTriggerStay(Collider other)
    {
        Animation.SetBool(Bool, true);
        TriggerAnimation.SetBool(TriggerBool, true);
    }

    private void OnTriggerExit(Collider other)
    {
        Animation.SetBool(Bool, false);
        if (Wait)
        {
            StartCoroutine(Delay());
        }
        else
        {
            TriggerAnimation.SetBool(TriggerBool, false);
        }
        
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(waitTime);
        TriggerAnimation.SetBool(TriggerBool, false);
    }
}