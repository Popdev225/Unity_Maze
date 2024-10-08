using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    // pulling function rom PlayerHealth class.
    public PlayerHealth playerHealth;
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // This class gives damage when enemy collides with the enemy.
   
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerHandler playerhandler = other.GetComponent<PlayerHandler>();
            //Debug.Log("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            playerHealth.TakeDamage(damage);
            playerhandler.LoseLife();
            Debug.Log("Hit");
        }
    }

}