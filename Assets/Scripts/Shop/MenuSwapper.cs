using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MenuWithButton
{
    public Button button;
    public Transform menu;
    public int index;

    [HideInInspector]
    public Vector3 offPosition;
}

public class MenuSwapper : MonoBehaviour
{
    public List<MenuWithButton> menus = new();
    private Vector3 onPosition = new(995.35f, 367.61f, 0.0f);

    void Awake()
    {
        for (int k = 0; k < menus.Count; k++)
        {
            menus[k].index = k;
            menus[k].offPosition = menus[k].menu.position;
        }

        foreach (MenuWithButton mb in menus)
        {
            mb.button.onClick.AddListener(() => { OpenMenu(mb.index); });
        }

        OpenMenu(0);   
    }

    public void OpenMenu(int index)
    {
        for (int i = 0; i < menus.Count; i++)
        {
            menus[i].menu.position = menus[i].offPosition;
            menus[i].button.interactable = true;
        }
        menus[index].menu.position = onPosition;
        menus[index].button.interactable = false;
    }
}
