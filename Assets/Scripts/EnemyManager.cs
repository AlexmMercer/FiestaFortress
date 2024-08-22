using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private List<GameObject> enemies = new List<GameObject>();

    public void Init()
    {
        // ?????????????
    }

    public void SpawnEnemy(Vector3 spawnPosition)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
    }

    public void CheckAllEnemiesDestroyed()
    {
        if (enemies.Count == 0)
        {
            GameManager.Instance.LevelManager.CheckWinCondition();
        }
    }
}
