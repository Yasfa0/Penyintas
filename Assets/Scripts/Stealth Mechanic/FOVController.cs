using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FOVController : MonoBehaviour
{
    List<FieldOfView> FOVList = new List<FieldOfView>();
    List<int> FOVindexList = new List<int>();
    //int activeFOVID = 0;
    int activeFOVindex = 0;
    FieldOfView activeFOV;
    bool canControl = true;

    int cycleCount;

    private void Awake()
    {
        /*foreach (FieldOfView fov in GetComponents<FieldOfView>())
        {
            FOVList.Add(fov);
        }*/
        FieldOfView[] allFov = GetComponents<FieldOfView>();
        for (int i = 0; i < allFov.Length; i++)
        {
            FOVList.Add(allFov[i]);
            FOVindexList.Add(i);
        }
        ChangeActiveFOV();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("FOV Index List Count " + FOVindexList.Count);
            Debug.Log("Active FOV Index " + activeFOVindex);
            Debug.Log("List Active FOV Index " + FOVindexList[activeFOVindex]);
        }

        if (canControl)
        {
            if (activeFOV.GetCycleCount() >= 1)
            {
                activeFOVindex++;
                if (activeFOVindex >= FOVindexList.Count)
                {
                    activeFOVindex = 0;
                    cycleCount++;
                }
                //Debug.Log("Active FOV " + activeFOVID);
                //activeFOV.SetCycleCount(0);
                ChangeActiveFOV();
            }
        }
    }

    public void ChangeFoVList(PatrolPoint patrolPoints)
    {
        FOVList.Clear();
        //FOVindexList.Clear();
        List<int> empty = new List<int>();
        FOVindexList = empty;
        activeFOVindex = 0;
        FOVindexList = patrolPoints.FOVIndexes;
        //FOVList = patrolPoints.FoVPattern;
        List<FieldOfView> tempFOV = new List<FieldOfView>();
        foreach (FieldOfView fov in GetComponents<FieldOfView>())
        {
            for (int i = 0; i < patrolPoints.FOVIndexes.Count; i++)
            {
                if (fov.GetFOVId() == patrolPoints.FOVIndexes[i])
                {
                    tempFOV.Add(fov);
                }
            }
        }
        FOVList = tempFOV;
        Debug.Log("FOV List Changed");
        ChangeActiveFOV();
    }

    public void ChangeActiveFOV()
    {
        foreach (FieldOfView FOV in GetComponents<FieldOfView>())
        {
            FOV.enabled = false;
        }
        foreach (FieldOfView FOV in FOVList)
        {
            //FOV.SetIsActiveFOV(false);
            if (FOV.GetFOVId() == FOVindexList[activeFOVindex])
            {
                Debug.Log("FOV Found");
                //FOV.SetIsActiveFOV(true);
                FOV.enabled = true;
                activeFOV = FOV;
                activeFOV.SetCycleCount(0);
            }
        }
        Debug.Log("FOV Changed");
    }

    public void SetCanControl(bool canControl)
    {
        this.canControl = canControl;
    }

    public int GetCycleCount()
    {
        return cycleCount;
    }

    public void SetCycleCount(int cycleCount)
    {
        this.cycleCount = cycleCount;
    }

    public void StopControlling()
    {
        foreach (FieldOfView FOV in GetComponents<FieldOfView>())
        {
            FOV.enabled = false;
        }
        canControl = false;
    }

}
