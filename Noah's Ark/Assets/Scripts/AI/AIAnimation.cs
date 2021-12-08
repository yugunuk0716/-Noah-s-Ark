using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AIAnimation : MonoBehaviour
{
    private Animator animator;

    private int runModeHash = Animator.StringToHash("RunModeHash");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }



    public void SetRunmode(int mode)
    {
        
    }
}
