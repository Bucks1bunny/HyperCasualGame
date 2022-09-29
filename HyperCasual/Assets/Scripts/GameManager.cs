using System;
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
    private Color sampleColor;
    [SerializeField]
    private Blender blender;
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
        blender.ColorMixed += OnColorMixed;

        for (int i = 0; i < objectsTag.Count; i++)
        {
            objectPool.SpawnFromPool(objectsTag[i], positions[i].position, Quaternion.identity);
        }
    }

    private void OnColorMixed(Color mixedColor)
    {
        var rDist = Math.Abs(mixedColor.r - sampleColor.r);
        var gDist = Math.Abs(mixedColor.g - sampleColor.g);
        var bDist = Math.Abs(mixedColor.b - sampleColor.b);

        float a = 100 - ((rDist + gDist + bDist) * 100);

        Debug.Log(a);
        if (a >= 85)
        {
            Debug.Log(true);
        }
    }
}
