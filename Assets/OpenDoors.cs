using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(PlayerModel.CompletedQuests.Count >= 2)
        {
            Destroy(gameObject);
        }
    }
}
