using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue",menuName = "Scriptable Objects/Dialogue")]
public class DialogueScriptable : ScriptableObject
{
    public Dialogue[] dialogues;
    public Choice[] choices;
    public int eventIndex = -1;
}
