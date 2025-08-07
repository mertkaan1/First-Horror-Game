using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public Player player;
    public Enemy enemyPrefab;
    public List<Enemy> enemies;
    public Vector2 enemyCount;
    public float spawnRange = 20f;


    public void RestartEnemyManager()
    {
        DeleteEnemies();
        CreateEnemies();
    }

    private void DeleteEnemies()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
        enemies.Clear();
    }
    bool GetRandomPointOnNavMesh(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++) // 30 deneme hakkı
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            randomPoint.y = center.y;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }
    private void CreateEnemies()
    {
        var randomEnemyCount = UnityEngine.Random.Range(enemyCount.x, enemyCount.y);
        for (int i = 0; i < randomEnemyCount; i++)
        {
            Vector3 spawnPos;
            if (GetRandomPointOnNavMesh(transform.position, spawnRange, out spawnPos))
            {
                var e = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                e.StartEnemy(player);
                enemies.Add(e.GetComponent<Enemy>());
            }
        }
       
    }
}
