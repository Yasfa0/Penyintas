using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActivated : MonoBehaviour
{
    public GameObject tanganKanan;
    public GameObject tanganKiri;
    // Start is called before the first frame update
    void Start()
    {
        tanganKanan.SetActive(false);
        tanganKiri.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
