using UnityEngine;
using System.Collections.Generic;

public class LevelPooling : MonoBehaviour
{
    public static LevelPooling Instance;

    [Header("Pool Settings")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 20;

    private readonly List<GameObject> availableObjects = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            availableObjects.Add(obj);
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        GameObject obj = null;

        if (availableObjects.Count > 0)
        {
            obj = availableObjects[0];
            availableObjects.RemoveAt(0);
        }
        else
        {
            obj = Instantiate(prefab);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        if (!availableObjects.Contains(obj))
        {
            obj.SetActive(false);
            availableObjects.Add(obj);
        }
    }

    public void ClearPool()
    {
        foreach (var obj in availableObjects)
            Destroy(obj);

        availableObjects.Clear();
    }
}