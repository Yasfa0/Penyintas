using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private Font pressedFont;
    [SerializeField] private Text mainText;
    private Font defaultFont;
    [SerializeField] private List<GameObject> effectIcons;

    private void Awake()
    {
        defaultFont = mainText.font;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            ApplyEffect();
        }
        else
        {
            RemoveEffect();
        }
    }

    public void ApplyEffect()
    {
        mainText.font = pressedFont;
        /*foreach (GameObject icon in effectIcons)
        {
            //icon.GetComponent<Image>().color = new Color(0,0,0,0);
            icon.SetActive(false);
        }*/
    }

    public void RemoveEffect()
    {
        mainText.font = defaultFont;
        /*foreach (GameObject icon in effectIcons)
        {
            //icon.GetComponent<Image>().color = new Color(0, 0, 0, 255);
            icon.SetActive(true);
        }*/
    }


}
