using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private static List<IUpdateable> behaviour = new List<IUpdateable>();

    void Update()
    {
        for (int i = 0; i < behaviour.Count; i++)
        {
            behaviour[i].Tick();
        }
    }

    public static void RegisterLogic(IUpdateable obj)
    {
        if (obj != null)
        {
            behaviour.Add(obj);
        }
    }

    public static void UnregisterLogic(IUpdateable obj)
    {
        if (obj != null)
        {
            behaviour.Remove(obj);
        }
    }
}