using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal; 
using UnityEngine.UI;

public class finishLine : MonoBehaviour
{
    public GameObject CharacterLamp;
    public GameObject ItemsGenerator;
    public GameObject Enemy;
    public GameObject Enemy2;
    public GameObject GameSuccessPanel;
    public GameObject doorCollider;
    public Light2D roomLight; // Reference to the room's light
    public GameObject tipAlert;


    public bool isGameRunning = true;
    public GameObject successPanel; // Assign this in the Inspector
    public Text timeText; // Assign the TimeText element from the SuccessPanel
    private float elapsedTime;

    public int crashCount = 0;

    void Start () {
        
    }

    void Update() {
        if (isGameRunning)
        {
            elapsedTime += Time.deltaTime;    
        }
    }

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerHandler playerhandler = collision.GetComponent<PlayerHandler>();
            crashCount++;

            if (crashCount <= 1)
            {
                // Set the light intensity to 0 or a very low value to darken the room
                roomLight.intensity = 0f;
                CharacterLamp.SetActive(true);
                ItemsGenerator.SetActive(true);
                Enemy.SetActive(true);
                Enemy2.SetActive(true);
                
                Time.timeScale = 0;
                tipAlert.SetActive(true);
            }
            if (playerhandler.samsung && playerhandler._switch && playerhandler.computer)
            {
                //GameSuccessPanel.SetActive(true);
                EndGame();
                //Transition.Alive = true;
                gameObject.SetActive(false);
                doorCollider.SetActive(false);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                Time.timeScale = 0;
                //SceneManager.LoadScene("Game");
                //Debug.Log("MainMenu");
            }
        }
    }

    public void EndGame()
    {
        isGameRunning = false;

        string formattedTime = string.Format("{0:F2} SECONDS", elapsedTime);
        float Gb = elapsedTime*130/1024;
        string formattedGB = string.Format("{0}", Gb);

        // Display the time on the SuccessPanel
        if (successPanel != null &&  timeText != null)
        {
            timeText.text =
                $"VOUS AVEZ TERMINÉ LA PARTIE EN {formattedTime}. SACHEZ QUE PENDANT CE TEMPS, LA CARTE MICROSD PRO ULTIMATE SAMSUNG A TRANSFÉRÉ {formattedGB}";
            successPanel.SetActive(true);
            //Time.timeScale = 0; // Resume game time 
        }
    }
}
