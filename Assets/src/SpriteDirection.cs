using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpriteDirection : MonoBehaviour
{
    public AIPath aiPath;
    private SpriteRenderer spriteRenderer;
    public Transform character; // Reference to the player's character position
    Animator enemyanimator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyanimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (aiPath.desiredVelocity.x != 0.00f){
            enemyanimator.SetBool("isTracking", true);
        }
        else{
            enemyanimator.SetBool("isTracking", false);
        }
        
                // Check if the enemy is on the positive or negative X-axis relative to the character
        float enemyPositionX = transform.position.x; // Enemy's X position
        float characterPositionX = character.position.x; // Character's X position

        if (enemyPositionX > characterPositionX)
        {
            // Enemy is on the right (positive X-axis relative to character)
            Debug.Log("Enemy is to the right of the character");
            spriteRenderer.flipX = false; // Face right
        }
        else if (enemyPositionX < characterPositionX)
        {
            // Enemy is on the left (negative X-axis relative to character)
            Debug.Log("Enemy is to the left of the character");
            spriteRenderer.flipX = true; // Face left
        }

        
    }
}