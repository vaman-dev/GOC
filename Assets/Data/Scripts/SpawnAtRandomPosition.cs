using UnityEngine;
using System.Collections.Generic;

public class SpawnAtRandomPosition : MonoBehaviour
{
    [Header("Assign possible spawn points (GameObjects)")]
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();

    private bool hasSpawned = false;
    private List<GameObject> spawnedInstances = new List<GameObject>();
    private GameObject objectToSpawn;

    public void SetObjectToSpawn(GameObject prefab)
    {
        objectToSpawn = prefab;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasSpawned && objectToSpawn != null)
        {
            SpawnAtRandomPoints(objectToSpawn);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasSpawned = false;
            foreach (var instance in spawnedInstances)
            {
                if (instance != null)
                {
                    Destroy(instance);
                }
            }
            spawnedInstances.Clear();
        }
    }

    public void SpawnAtRandomPoints(GameObject prefab)
    {
        if (hasSpawned) return;

        if (spawnPoints.Count < 2)
        {
            Debug.LogWarning("At least 2 spawn points are required.");
            return;
        }

        hasSpawned = true;
        spawnedInstances.Clear();

        List<GameObject> availablePoints = new List<GameObject>(spawnPoints);

        for (int i = 0; i < 5; i++)
        {
            int index = Random.Range(0, availablePoints.Count);
            GameObject randomPoint = availablePoints[index];
            GameObject instance = Instantiate(prefab, randomPoint.transform.position, Quaternion.identity);
            spawnedInstances.Add(instance);
            availablePoints.RemoveAt(index); // Ensure uniqueness
        }
    }
}
