using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatrolPoint 
{
    public Transform patrolPosition;
    public int patrolAngle;
    public List<int> FOVIndexes;
}
