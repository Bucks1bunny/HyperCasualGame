using System;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    public event Action<Color> ColorMixed = delegate { };

    private List<Color> colorList = new List<Color>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            colorList.Add(other.GetComponent<Object>().objectColor);
        }
    }

    public void MixColors()
    {
        float totalRed = 0f;
        float totalGreen = 0f;
        float totalBlue = 0f;

        foreach (var color in colorList)
        {
            totalRed += color.r;
            totalGreen += color.g;
            totalBlue += color.b;
        }

        float colorsCount = colorList.Count;
        Color newColor = new Color(totalRed / colorsCount, totalGreen / colorsCount, totalBlue / colorsCount);
        ColorMixed(newColor);
    }
}
