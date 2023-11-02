using GameProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Transform previousCheckpoint; // Bien luu tru checkpoint trc do
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void Respawn()
    {
        if (currentCheckpoint != null)
        {
            transform.position = currentCheckpoint.position; 
            playerHealth.Respawn();
        }
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
    }
}
