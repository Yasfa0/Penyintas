using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    private int route;

    public void SetupChoice(int route)
    {
        this.route = route;
    }

    public void SelectChoice()
    {
        DialogueController.Instance.ApplyChoice(route);
    }
}
