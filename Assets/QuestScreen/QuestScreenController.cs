using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScreenController : MonoBehaviour
{
    [SerializeField] private Text quests;
    [SerializeField] private Text completedQuests;
    void Start()
    {
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        foreach (var item in PlayerModel.ActiveQuests)
        {
            quests.text = string.Concat(quests.text, item + "\n");
        }
        foreach (var item in PlayerModel.CompletedQuests)
        {
            completedQuests.text = string.Concat(completedQuests.text, item + "\n");
        }
    }

    public void CloseQuests()
    {
        Destroy(gameObject);
    }
}
