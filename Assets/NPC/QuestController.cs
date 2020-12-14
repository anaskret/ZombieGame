using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesToKill;
    [SerializeField] private string questName;

    private int enemiesAlive;
    private List<EnemyModel> enemyAiList;
    // Start is called before the first frame update
    void Start()
    {
        enemiesAlive = enemiesToKill.Length;
        GetEnemyAiComponents();
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemiesAlive();
        IsQuestCompleted();
    }

    public void QuestAccepted()
    {
        PlayerModel.AddActiveQuest(questName);
    }

    private void IsQuestCompleted()
    {
        if(enemiesAlive <= 0)
            PlayerModel.QuestCompleted(questName);
    }

    private void GetEnemyAiComponents()
    {
        foreach (var enemy in enemiesToKill)
        {
            var enemyAi = enemy.GetComponent<EnemyModel>();
            enemyAiList.Add(enemyAi);
        }
    }

    private void CheckEnemiesAlive()
    {
        foreach (var enemy in enemyAiList)
        {
            if (!enemy.IsAlive)
            {
                enemiesAlive--;
                enemyAiList.Remove(enemy);
            }
        }
    }
}
