using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIMovements : MonoBehaviour
{
    public PlayerController playerScript;
    List<char> movements = new List<char>();
    private bool isRunning;

    [Header("InfoData")]
    public Text gameConsoleText;
    public Text instructionsText;
    public MyConsole console;
    private string instructionsMsgs = "";

    private void Update()
    {
        updateText();
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    #region UpdateConsoleText
    private void updateText()
    {
        gameConsoleText.text = console.getMessages();
        instructionsText.text = instructionsMsgs;
    }
    #endregion

    #region AddToList
    public void MoveForward()
    {
        movements.Add('w');
        instructionsMsgs += "Move forward. \n";
    }
    public void MoveRight()
    {
        movements.Add('d');
        instructionsMsgs += "Move right. \n";
    }
    public void MoveLeft()
    {
        movements.Add('a');
        instructionsMsgs += "Move left. \n";
    }
    public void MoveBack()
    {
        movements.Add('s');
        instructionsMsgs += "Move backwards. \n";
    }

    public void IfPlatform()
    {
        movements.Add('i');
        instructionsMsgs += "Check if platform. \n";
    }

    #endregion

    #region Run
    public void Run()
    {
        console.resetMessages();

        if (!isRunning) StartCoroutine(RunSequence());
    }

    public IEnumerator RunSequence()
    {
        isRunning = true;

        if (movements.Count > 0)
        {
            Debug.Log("Executing instructions...");
            yield return new WaitForSeconds(0.25f);

            for (int j = 0; j < movements.Count; j++)
            {
                char i = movements[j];

                switch (i)
                {
                    case 'w':
                        Debug.Log("Moving forward...");
                        yield return StartCoroutine(playerScript.Roll(Vector3.forward));
                        break;
                    case 's':
                        Debug.Log("Moving backwards...");
                        yield return StartCoroutine(playerScript.Roll(Vector3.back));
                        break;
                    case 'a':
                        Debug.Log("Moving to the left...");
                        yield return StartCoroutine(playerScript.Roll(Vector3.left));
                        break;
                    case 'd':
                        Debug.Log("Moving to the right...");
                        yield return StartCoroutine(playerScript.Roll(Vector3.right));
                        break;
                    case 'i':
                        Debug.Log("Checking platform...");
                        j++;
                        if (movements[j] == 'w')
                            yield return StartCoroutine(playerScript.WaitForPlatform(Vector3.forward, true));
                        if (movements[j] == 's')
                            yield return StartCoroutine(playerScript.WaitForPlatform(Vector3.back, true));
                        if (movements[j] == 'a')
                            yield return StartCoroutine(playerScript.WaitForPlatform(Vector3.left, true));
                        if (movements[j] == 'd')
                            yield return StartCoroutine(playerScript.WaitForPlatform(Vector3.right, true));
                        j++;
                        break;
                    default: break;
                }

                yield return new WaitForSeconds(0.25f);

            }
            Debug.Log("Execution complete.");
            yield return new WaitForSeconds(0.25f);
        }
        else
        {
            instructionsMsgs = "";
            Debug.Log("Please insert instructions.");
            yield return new WaitForSeconds(0.25f);
        }

        movements.Clear();

        isRunning = false;
    }
    #endregion

    public void ResetMovements()
    {
        if (!isRunning) StartCoroutine(ResetSequence());
    }

    private IEnumerator ResetSequence()
    {
        Debug.Log("Resetting...");
        yield return new WaitForSeconds(0.5f);
        console.resetMessages();
        instructionsMsgs = "";
        movements.Clear();
        Respawn respawnScript = GameObject.Find("Game Manager").GetComponent<Respawn>();
        respawnScript.RespawnPlayer();
    }


}
