using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadreCrateController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerModel.ChangeNumberOfGrenades(1);
            Destroy(gameObject);
        }
    }
}
