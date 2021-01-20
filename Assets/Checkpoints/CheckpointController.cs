using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!ReferenceEquals(PlayerModel.CurrentCheckpoint, gameObject) && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Checkpoint reached");
            //SavingService.SaveGame("newCheckpoint.json");
            PlayerModel.SetCheckpoint(gameObject);
        }
    }
}
