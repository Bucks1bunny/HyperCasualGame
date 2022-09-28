using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [field: SerializeField]
    public List<string> objectsTag
    {
        get;
        private set;
    } = new List<string>();

    [SerializeField]
    private List<Transform> positions = new List<Transform>();
    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = ObjectPool.Instance;
        objectPool.GameManager = this;
    }
    private void Start()
    {
        for (int i = 0; i < objectsTag.Count; i++)
        {
            objectPool.SpawnFromPool(objectsTag[i], positions[i].position, Quaternion.identity);
        }
    }
}
