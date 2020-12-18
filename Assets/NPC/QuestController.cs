using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesToKill;
    [SerializeField] private string questName;

    private int enemiesAlive;
    private List<EnemyModel> enemyAiList = new List<EnemyModel>();

    private bool questAccepted = false;

    // Start is called before the first frame update
    void Start()
    {
        enemiesAlive = enemiesToKill.Length;
        GetEnemyAiComponents();
    }

    // Update is called once per frame
    void Update()
    {
        if (questAccepted)
        {
            EnemiesAlive();
            Debug.Log(enemyAiList.Count);
            IsQuestCompleted();
        }
    }

    public void QuestAccepted()
    {
        PlayerModel.AddActiveQuest(questName);
        questAccepted = true;
    }

    private void IsQuestCompleted()
    {
        if (enemiesAlive <= 0)
        { 
            PlayerModel.QuestCompleted(questName);
            gameObject.GetComponent<NpcController>().questCompleted = true;
        }
    }

    private void GetEnemyAiComponents()
    {
        foreach (var enemy in enemiesToKill)
        {
            var addEnemy = enemy.GetComponent<EnemyModel>();
            enemyAiList.Add(addEnemy);
            /*var nameOfEnemy = enemy.tag;
            switch (nameOfEnemy)
            {
                case "BaseEnemy":
                    Debug.Log("Basic");
                    var basicEnemy = enemy.GetComponent<BasicEnemyAi>();
                    enemyAiList.Add(basicEnemy);
                    break;
                case "SpittingEnemy":
                    Debug.Log("Spit");
                    var spittingEnemy = enemy.GetComponent<SpittingEnemyAi>();
                    enemyAiList.Add(spittingEnemy);
                    break;
                case "Boomer":
                    Debug.Log("boomt");
                    var boomer = enemy.GetComponent<BoomerAi>();
                    enemyAiList.Add(boomer);
                    break;
                case "TrailEnemy":
                    Debug.Log(enemy.GetComponent<TrailEnemyAi>());
                    var trailEnemy = enemy.GetComponent<TrailEnemyAi>();
                    enemyAiList.Add(trailEnemy);
                    break;
            }*/
        }
    }

    private void EnemiesAlive()
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
