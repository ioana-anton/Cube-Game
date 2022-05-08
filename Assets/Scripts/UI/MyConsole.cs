using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyConsole : MonoBehaviour
{

    public string output = "";
    public string stack = "";

    private int noMsgs = 0;
    private string allMessages = "\n";


    public void resetMessages() { allMessages = ""; noMsgs = 0; }
    public string getMessages() { return allMessages; }


    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;

        if (noMsgs < 10)
        {
            allMessages += output + "\n";
            noMsgs++;
        }
        else
        {
            allMessages = output + "\n";
            noMsgs = 0;
        }
    }

}


