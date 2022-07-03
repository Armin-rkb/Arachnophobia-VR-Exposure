using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAlpha : MonoBehaviour
{
    [SerializeField]
    private Material material;

    IEnumerator FadeIn()
    {
        Color c = material.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            material.color = c;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        Color c = material.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            material.color = c;
            yield return null;
        }
    }
}
