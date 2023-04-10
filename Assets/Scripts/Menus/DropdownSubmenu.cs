using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownSubmenu : MonoBehaviour
{
    [SerializeField] private GameObject submenu;
    GameObject arrowIcon;
    private bool isOpen = true;

    private void Awake()
    {
        arrowIcon = transform.Find("Arrow").gameObject;
    }

    private void Update()
    {
        if (isOpen)
        {
            arrowIcon.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (!isOpen)
        {
            arrowIcon.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void ToggleSubmenu()
    {
        isOpen = !isOpen;
        submenu.SetActive(isOpen);
    }
}
