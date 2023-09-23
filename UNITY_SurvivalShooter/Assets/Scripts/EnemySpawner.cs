using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] enemyPrefab;
    public Transform[] spawnPoints;

    public float generalDamage = 30f;
    public float generalHp = 100f;
    public float generalSpeed = 2f;

    public float bossDamage = 50f;
    public float bossHp = 200f;
    public float bossSpeed = 1f;

    private List<Enemy> enemies = new List<Enemy>();
    private int bossCount = 0;
    private int maxEnimyCount = 50;

    private void Start()
    {
        StartCoroutine(CreateEnemy());
    }

    private IEnumerator CreateEnemy()
    {
        while(true)
        {
            if(GameManager.instance != null && GameManager.instance.isGameover)
            {
                break;
            }

            if(enemies.Count < maxEnimyCount)
            {
                Debug.Log(enemies.Count);
                var point = spawnPoints[Random.Range(0, spawnPoints.Length)];
                var score = GameManager.instance.score;

                if(score % 50 == 0 && score / 50 > bossCount)
                {
                    var enemy = Instantiate(enemyPrefab[2], point.position, point.rotation);
                    enemy.Setup(bossHp, bossDamage, bossSpeed);
                    enemies.Add(enemy);
                    bossCount++;

                    enemy.onDeath += () =>
                    {
                        enemies.Remove(enemy);
                        Destroy(enemy.gameObject, 3f);
                        //GameManager.instance.AddScore(50);
                    };
                }
                else
                {
                    var enemy = Instantiate(enemyPrefab[Random.Range(0,2)], point.position, point.rotation);
                    enemy.Setup(generalHp, generalDamage, generalSpeed);
                    enemies.Add(enemy);

                    enemy.onDeath += () =>
                    {
                        enemies.Remove(enemy);
                        Destroy(enemy.gameObject, 10f);
                        GameManager.instance.AddScore(10);
                    };
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
