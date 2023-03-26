using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionProp : InteractiveProp
{
    //protected Animator animator;
    //[SerializeField] protected string animName;
    [SerializeField] protected AudioClip audioClip;
    [SerializeField] protected int distractionID;
    private bool isUsed = false;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    protected override void Interact()
    {
        /*if (useCounter < useLimit)
        {
            useCounter++;
            //animator.SetBool(animName, true);
            Debug.Log("Distraction Start");
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground") && !isUsed)
        {
            AudioManager.Instance.PlayAudio(audioClip, 1);
            Distraction();
            isUsed = true;
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
