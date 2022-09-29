using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPool Instance;
    public GameManager GameManager
    {
        get;
        set;
    }
    public List<Pool> pools = new List<Pool>();

    private Dictionary<string, Queue<GameObject>> poolDict = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var pool in pools)
        {
            if (!GameManager.objectsTag.Contains(pool.tag))
            {
                continue;
            }
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDict.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDict.ContainsKey(tag))
        {
            Debug.LogWarning("Doesnt exist " + tag);
            return null;
        }

        GameObject objectToPool = poolDict[tag].Dequeue();

        objectToPool.SetActive(true);
        objectToPool.transform.position = position;
        objectToPool.transform.rotation = rotation;

        poolDict[tag].Enqueue(objectToPool);

        return objectToPool;
    }
}
