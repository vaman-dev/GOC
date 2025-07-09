using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [Header("Pool Settings")]
    [SerializeField] private GameObject hurdlePrefab;
    [SerializeField] private int poolSize = 20;

    private readonly List<GameObject> availableObjects = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Optional: Uncomment to persist across scenes
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(hurdlePrefab);
            obj.SetActive(false);
            availableObjects.Add(obj);
        }
    }

    public GameObject GetHurdle(Vector3 position, Quaternion rotation)
    {
        if (availableObjects.Count == 0)
        {
            Debug.LogWarning("ObjectPool: No available hurdles to spawn!");
            return null;
        }

        GameObject obj = availableObjects[0];
        availableObjects.RemoveAt(0);

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        return obj;
    }

    public void ReturnHurdle(GameObject obj)
    {
        if (obj == null || availableObjects.Contains(obj)) return;

        obj.SetActive(false);
        availableObjects.Add(obj);
    }

    public void ClearPool()
    {
        foreach (var obj in availableObjects)
        {
            if (obj != null)
                Destroy(obj);
        }

        availableObjects.Clear();
    }
}
