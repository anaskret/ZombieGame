using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKitController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerModel.ChangeNumberOfFirstAidKits(1);
            Destroy(gameObject);
        }
    }
}
