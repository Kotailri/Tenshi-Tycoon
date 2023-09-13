using UnityEngine;
using UnityEngine.EventSystems;

public class HasHoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public string text;
    private bool showing = false;
    private bool isEnabled = true;

    private bool cursorIn = false;

    public void EnableHoverBox()
    {
        isEnabled = true;
    }

    public bool IsShowing()
    {
        return showing;
    }

    public void DisableHoverBox()
    {
        isEnabled = false;
        showing = false;
        Global.hoverBox.HideText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Global.hoverBox)
        {
            cursorIn = false;
            showing = false;
            Global.hoverBox.HideText();
        }
    }

    public void Refresh()
    {
        if (cursorIn)
        {
            if (GetInfoText() != "")
            {
                text = GetInfoText();
                Global.hoverBox.HideText();
                Global.hoverBox.ShowText(text);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (Global.hoverBox && !showing && isEnabled)
        {
            cursorIn = true;
            showing = true;
            if (GetInfoText() != "")
            {
                text = GetInfoText();
                Global.hoverBox.ShowText(text);
            }

        }

    }

    public virtual string GetInfoText()
    {
        return "";
    }
}
