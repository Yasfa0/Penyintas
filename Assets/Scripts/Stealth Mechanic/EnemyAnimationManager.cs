using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    /*
     * 0 = On Radar 
     * 1 = Off Radar
     * 2 = Trigger
     */

    [SerializeField] List<GameObject> enemyVisual = new List<GameObject>();
    [SerializeField] List<string> animStateName = new List<string>();

    private int currentState;

    /*public void AnimateOnRadar()
    {
        //Disable off radar anim
        enemyVisual[1].GetComponent<Animator>().ResetTrigger(animStateName[1]);
        enemyVisual[1].SetActive(false);

        //Enable on radar anim
        enemyVisual[0].SetActive(true);
        enemyVisual[0].GetComponent<Animator>().SetTrigger(animStateName[0]);
    }*/

    public void AnimateEnemy(int stateIndex)
    {
        if (currentState != stateIndex)
        {
            for (int i = 0; i < animStateName.Count; i++)
            {
                if (i != stateIndex)
                {
                    if (enemyVisual[i].GetComponent<Animator>() && !animStateName[i].Equals(""))
                    {
                        enemyVisual[i].GetComponent<Animator>().ResetTrigger(animStateName[i]);
                    }
                    enemyVisual[i].SetActive(false);
                }
                else if (i == stateIndex)
                {
                    enemyVisual[i].SetActive(true);
                    if (enemyVisual[i].GetComponent<Animator>() && !animStateName[i].Equals(""))
                    {
                        enemyVisual[i].GetComponent<Animator>().SetTrigger(animStateName[i]);
                    }
                }
            }
        }

        currentState = stateIndex;
    } 


}
