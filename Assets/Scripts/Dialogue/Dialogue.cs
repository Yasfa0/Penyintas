using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string[] names;
    [TextArea(3,15)]
    public string[] dialogued;
}
