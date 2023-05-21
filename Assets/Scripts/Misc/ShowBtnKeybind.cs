using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowBtnKeybind : MonoBehaviour
{
    Text btnText;
    private void Awake()
    {
        btnText = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
      btnText.text = KeybindSaveSystem.currentKeybind.interact.ToString();
    }
}
