using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractEffect : MonoBehaviour
{
    [SerializeField] protected int distractID;

    public virtual void DistractedAction()
    {
        Debug.Log("Distraction Action");
    }

    public int GetDistractID()
    {
        return distractID;
    }
}
