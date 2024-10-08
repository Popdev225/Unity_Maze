using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // For scene management

public class PlayerHandler : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float collisionOffset = 0.02f;
    public float boostSpeed = 2.5f;
    public float slowSpeed = 1f;
    public ContactFilter2D movementFilter;

    public bool samsung = false;
    public bool computer = false;
    public bool _switch = false;
    public bool phone = false;

    public int currentLives = 3;
    public Image[] lifeIcons; // Array to hold the life icon images
    public Sprite fullLifeSprite; // Sprite for a full life
    public Sprite lostLifeSprite; // Sprite for a lost life

    bool lockmovement = false;
    Vector2 movementInput;

    SpriteRenderer spriterenderer;
    Rigidbody2D rb;
    PlayerHealth playerHealth;

    Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public Button continueButton; // The restart button in the UI
    public Button goToFirstScreenButton_GameOver; // The restart button in the UI
    public Button goToFristScreenButton_Success; // The restart button in the UI

    public GameObject GameSuccessPanel;
    public GameObject gameOverPanel;
    public GameObject gameExitPanel;
    public Collider2D doorCollider;
    public GameObject allCollectedPanel;

    private bool flagOpenDoor = false;
    private bool isCollidingWithEnemy = false; // Flag to track if in collision with enemy
    private float timeInCollision = 0f; // Timer for collision
    public float collisionDuration = 2f; // Time to trigger game over on sustained collision

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
        doorCollider = GetComponent<Collider2D>();

        if (goToFirstScreenButton_GameOver != null)
            goToFirstScreenButton_GameOver.onClick.AddListener(RestartGameFromFirst);

        if (goToFristScreenButton_Success != null)
            goToFristScreenButton_Success.onClick.AddListener(RestartGameFromFirst);

        UpdateLivesUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnableButtons();
            gameExitPanel.SetActive(true);
            Time.timeScale = 0;
        }

        // Check if the character is in a sustained collision with the enemy
        if (isCollidingWithEnemy)
        {
            timeInCollision += Time.deltaTime;

            // If the collision lasts longer than the set duration, reduce all lives and trigger game over
            if (timeInCollision >= collisionDuration)
            {
                LoseAllLives();
                GameOver();
            }
        }
    }

        public void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            UpdateLivesUI();
        }
    }

    public void RestartGameFromFirst()
    {
        Time.timeScale = 1;
        GameSuccessPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        // Reload the current scene to restart the game
        SceneManager.LoadScene("StartMenu");
    }

    public void UpdateLivesUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (i < currentLives)
            {
                lifeIcons[i].sprite = fullLifeSprite; // Set sprite for full life
            }
            else
            {
                lifeIcons[i].sprite = lostLifeSprite; // Set sprite for lost life
            }
        }

        // If out of lives, trigger game over
        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    public void LoseAllLives()
    {
        currentLives = 0; // Set lives to 0
        UpdateLivesUI(); // Immediately update the UI to reflect no lives
    }

    private void GameOver()
    {
        lockmovement = true; // Lock player movement
        gameOverPanel.SetActive(true); // Display game over panel
        Time.timeScale = 0; // Pause the game
    }

    private void FixedUpdate()
    {
        if (lockmovement == false)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("isMoving", success);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            if (movementInput.x < 0)
            {
                spriterenderer.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriterenderer.flipX = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SamSung"))
        {
            samsung = true;
            Destroy(other.gameObject);
        }

        if (samsung)
        {
            if (other.CompareTag("Phone"))
            {
                phone = true;
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("Switch"))
            {
                _switch = true;
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("Computer"))
            {
                computer = true;
                Destroy(other.gameObject);
            }
        }

        if (samsung && computer && _switch && phone && !flagOpenDoor)
        {
            allCollectedPanel.SetActive(true);
            flagOpenDoor = true;
            Time.timeScale = 0;
        }

        // Trigger enemy collision
        if (other.CompareTag("Enemy"))
        {
            isCollidingWithEnemy = true; // Start counting collision time
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            isCollidingWithEnemy = false; // Reset collision state
            timeInCollision = 0f; // Reset the timer when the collision ends
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue MovementValue)
    {
        movementInput = MovementValue.Get<Vector2>();
    }

    public void TriggerDeathAnimation()
    {
        lockmovement = true;
        animator.SetBool("isDead", true);
    }

    public void EnableButtons()
    {
        if (continueButton != null)
        {
            continueButton.interactable = true; // Enables the button
        }
    }
}
