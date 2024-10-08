using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // For scene management

public class DoorController : MonoBehaviour
{
    public Transform doorTransform; // Reference to the door's transform
    public float openDistance = 2f; // Distance to move the door to open it
    public float moveSpeed = 2f; // Speed at which the door opens

    private Vector3 closedPosition;
    private Vector3 openedPosition;
    private bool isOpening = false;

    void Start()
    {
        if (doorTransform != null)
        {
            closedPosition = doorTransform.position;
            openedPosition = closedPosition + new Vector3(0, openDistance, 0);
        }
    }

    void Update()
    {
        if (isOpening)
        {
            doorTransform.position = Vector3.MoveTowards(doorTransform.position, openedPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(doorTransform.position, openedPosition) < 0.1f)
            {
                isOpening = false;
                doorTransform.position = openedPosition; // Ensure it reaches the exact position
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Triggered with: " + collider.gameObject.name);
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Character collided with door.");
            gameObject.SetActive(false);
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        if (doorTransform != null)
        {
            isOpening = true;
        }
    }
}
