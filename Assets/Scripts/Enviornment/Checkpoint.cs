using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public CheckpointManager checkpointManager;

    void Start()
    {
        checkpointManager = GameObject.FindGameObjectWithTag("checkpointManager").GetComponent<CheckpointManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            checkpointManager.CurrentCheckpoint = transform;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            checkpointManager.CurrentCheckpoint = null;
        }
    }
}
