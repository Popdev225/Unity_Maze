using System.Collections; // For IEnumerator and coroutines
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using System;  // This is needed to use Exception

public class GameTimer : MonoBehaviour
{
    public GameObject successPanel; // Assign this in the Inspector
    public Text timeText; // Assign the TimeText element from the SuccessPanel
    public int currentLives = 3; // Set initial lives
    private float elapsedTime;
    public bool isGameRunning = true;

    private void Update()
    {
        if (isGameRunning)
        {
            elapsedTime += Time.deltaTime;    
        }
    }

    public void EndGame()
    {
        isGameRunning = false;

        string formattedTime = string.Format("{0:F2} SECONDS", elapsedTime);
        float Gb = elapsedTime*130/1024;
        Debug.Log("fdsafdsafdasfds");
        string formattedGB = string.Format("{0}", Gb);

        // Display the time on the SuccessPanel
        if (successPanel != null &&  timeText != null)
        {
            timeText.text =
                $"VOUS AVEZ TERMINÉ LA PARTIE EN {formattedTime}. SACHEZ QUE PENDANT CE TEMPS, LA CARTE MICROSD PRO ULTIMATE SAMSUNG A TRANSFÉRÉ {formattedGB}";
            successPanel.SetActive(true);
        }
    }
}
