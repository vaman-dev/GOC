using UnityEngine;
using System.Collections.Generic;

public class HurldleInstantiate : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private int numberToSpawn = 1;
    [SerializeField] private float Delaytime = 1f; // Time to wait before returning the hurdle

    private BoxCollider2D spawnZone;
    private readonly List<GameObject> spawnedHurdles = new List<GameObject>();
    private bool hasSpawned = false;

    private void Awake()
    {
        spawnZone = GetComponent<BoxCollider2D>();

        if (spawnZone == null || !spawnZone.isTrigger)
        {
            Debug.LogError("HurldleInstantiate: Requires a BoxCollider2D set as Trigger.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || hasSpawned) return;

        hasSpawned = true;

        for (int i = 0; i < numberToSpawn; i++)
        {
            SpawnHurdleWithinBounds();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        foreach (GameObject hurdle in spawnedHurdles)
        {
            if (hurdle != null)
            {
                StartCoroutine(DelayReturnHurdle(hurdle));
            }
        }

        spawnedHurdles.Clear();
        hasSpawned = false; // Allow re-triggering
    }

    private System.Collections.IEnumerator DelayReturnHurdle(GameObject hurdle)
    {
        yield return new WaitForSeconds(Delaytime); // Wait for specified time before returning the hurdle
        Debug.Log("Returning hurdle to pool: " + Delaytime);
        ObjectPool.Instance.ReturnHurdle(hurdle);
    }

    private void SpawnHurdleWithinBounds()
    {
        if (spawnZone == null || ObjectPool.Instance == null) return;

        Bounds bounds = spawnZone.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        Vector3 spawnPos = new Vector3(randomX, randomY, 0f);

        GameObject hurdle = ObjectPool.Instance.GetHurdle(spawnPos, Quaternion.identity);

        if (hurdle != null && !spawnedHurdles.Contains(hurdle))
        {
            spawnedHurdles.Add(hurdle);
        }
    }
}