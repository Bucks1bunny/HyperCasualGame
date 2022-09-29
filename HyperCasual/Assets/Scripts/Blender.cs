using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blender : MonoBehaviour
{
    public event Action<Color> MixedButtonPressed = delegate { };

    [SerializeField]
    private GameObject liquid;
    private Animator anim;
    private List<GameObject> foodList = new List<GameObject>();

    private void Awake()
    {
        liquid.SetActive(false);
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            foodList.Add(other.gameObject);
        }
    }

    public void MixColors()
    {
        float totalRed = 0f;
        float totalGreen = 0f;
        float totalBlue = 0f;

        foreach (var food in foodList)
        {
            Color color = food.GetComponent<Object>().objectColor;
            totalRed += color.r;
            totalGreen += color.g;
            totalBlue += color.b;
            Destroy(food);
        }

        float colorsCount = foodList.Count;
        Color newColor = new Color(totalRed / colorsCount, totalGreen / colorsCount, totalBlue / colorsCount);

        liquid.SetActive(true);
        var liquidRenderer = liquid.GetComponent<Renderer>();
        liquidRenderer.material.SetColor("_Main_Color", newColor);
        liquidRenderer.material.SetColor("_Surface_Color", newColor * 1.5f);

        anim.SetBool("Open", false);

        float fill = 0f;
        DOTween.To(() => fill, x => fill = x, 0.6f, 5f).OnUpdate(() =>
        liquidRenderer.material.SetFloat("_Amount", fill)).SetEase(Ease.InSine).OnComplete(() => MixedButtonPressed(newColor));

        GameManager.isMixed = true;
    }
}
