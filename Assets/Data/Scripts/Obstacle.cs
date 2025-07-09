using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Assign obstacle prefabs")]
    public List<GameObject> obstaclePrefabs;

    [Header("Reference to SpawnAtRandomPosition script")]
    [SerializeField] private SpawnAtRandomPosition spawnAtRandomPosition;

    private GameObject selectedObstacle;

    void Start()
    {
        selectedObstacle = GetRandomObstacle();
        if (selectedObstacle != null)
        {
            Debug.Log("Random Obstacle Selected: " + selectedObstacle.name);
            // Pass selected prefab to the spawner
            spawnAtRandomPosition.SetObjectToSpawn(selectedObstacle);
        }
    }

    private GameObject GetRandomObstacle()
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Count == 0)
        {
            Debug.LogWarning("Obstacle list is empty or not assigned.");
            return null;
        }

        int randomIndex = Random.Range(0, obstaclePrefabs.Count);
        return obstaclePrefabs[randomIndex];
    }
}
