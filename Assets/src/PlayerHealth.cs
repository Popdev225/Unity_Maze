
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 3;
    public AudioSource audioPlayer;
    private float delayTime = 1.0f;
    private float timeElapsed;
    public GameObject Player;
    static public bool playerAlive = true;
    public GameObject gameOverPanel;

    PlayerHandler playerHandler;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
        Transition.Alive = true;
        playerHandler = GetComponent<PlayerHandler>();
        Transition.level = SceneManager.GetActiveScene().name;
        Transition.lvlindex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void changeScene()
    {
        Transition.Alive = false;
        SceneManager.LoadScene("deathAnimation");
        Destroy(Player);
    }
    public void TakeDamage(int amount)
    {
        Debug.Log("Htttttttttttttttt");
        health -= amount;
        if(health <= 0)
        {
            playerHandler.TriggerDeathAnimation();
            audioPlayer.Play();
            //Invoke("changeScene", delayTime);
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    
}
