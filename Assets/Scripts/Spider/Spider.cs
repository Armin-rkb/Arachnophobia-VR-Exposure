using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string speedMultiplierParamName;
    private float animatorSpeedMultiplier;

    [System.Obsolete]
    void Awake()
    {
        if (animator)
        {
            animatorSpeedMultiplier = Random.Range(0.5f, 1.5f);
            animator.SetFloat(speedMultiplierParamName, animatorSpeedMultiplier);
        }
    }
}
