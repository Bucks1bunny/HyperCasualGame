using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<float> ColorMixed = delegate { };

    [field: SerializeField]
    public List<string> objectsTag
    {
        get;
        private set;
    } = new List<string>();

    [SerializeField]
    private GameObject sample;
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
        blender.MixedButtonPressed += OnMixedButtonPressed;
        blender.GetComponent<Animator>().SetBool("Open", true);

        for (int i = 0; i < objectsTag.Count; i++)
        {
            objectPool.SpawnFromPool(objectsTag[i], positions[i].position, Quaternion.identity);
        }
    }

    private void OnMixedButtonPressed(Color mixedColor)
    {
        Color sampleColor = sample.GetComponent<Renderer>().material.GetColor("_Main_Color");
        var mixR = Math.Abs(mixedColor.r - sampleColor.r);
        var mixG = Math.Abs(mixedColor.g - sampleColor.g);
        var mixB = Math.Abs(mixedColor.b - sampleColor.b);

        float percentage = 100 - ((mixR + mixG + mixB) * 100);

        ColorMixed(percentage);
    }
}
