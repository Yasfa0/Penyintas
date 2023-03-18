using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionProp : InteractiveProp
{
    protected Animator animator;
    [SerializeField] protected string animName;
    [SerializeField] protected int distractionID;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Interact()
    {
        if (useCounter < useLimit)
        {
            useCounter++;
            animator.SetBool(animName, true);
            Debug.Log("Distraction Start");
        }
    }

    public void Distraction()
    {
        Debug.Log("Distraction Method");
        foreach (DistractEffect distractEffect in FindObjectsOfType<DistractEffect>())
        {
            if (distractEffect.GetDistractID() == distractionID)
            {
                distractEffect.DistractedAction();
            }
        }
    }
}
