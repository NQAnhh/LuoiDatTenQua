using GameProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Transform previousCheckpoint; // Bien luu tru checkpoint trc do
    private Health playerHealth;
    UIManager uiManager;
    GameObject fallDetector;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        playerHealth.OnPlayerDeath += CheckRespawn;
        uiManager = FindObjectOfType<UIManager>();

        if (uiManager == null)
        {
            Debug.LogError("UIManager not found");
        }
    }

    public void CheckRespawn()
    {

        Debug.Log("Checking Respawn");
        if (currentCheckpoint == null)
        {
            uiManager.GameOver();
            return;
        }
        playerHealth.Respawn();
        transform.position = currentCheckpoint.position;
        

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            if (currentCheckpoint != null)
            {
                currentCheckpoint.GetComponent<Collider2D>().enabled = false;
                currentCheckpoint.GetComponent<Animator>().SetTrigger("disappear");
            }

            previousCheckpoint = currentCheckpoint; // Luu tru Checkpoint thanh Checkpoint trc do
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
        else if (collision.transform.tag == "FallDetector")
        {            
            playerHealth.TakeDamage(playerHealth.currentHealth);
        }

    }
}
