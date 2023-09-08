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

    public void DisableHoverBox()
    {
        isEnabled = false;
        showing = false;
        Global.hoverBox.HideText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cursorIn = false;
        if (Global.hoverBox)
        {
            showing = false;
            Global.hoverBox.HideText();
        }
    }

    public void Refresh()
    {
        if (cursorIn)
        {
            showing = false;
            Global.hoverBox.HideText();
            if (Global.hoverBox && !showing && isEnabled)
            {
                showing = true;
                if (GetInfoText() != "")
                {
                    text = GetInfoText();
                }
                Global.hoverBox.ShowText(text);

            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorIn = true;
        if (Global.hoverBox && !showing && isEnabled)
        {
            showing = true;
            if (GetInfoText() != "")
            {
                text = GetInfoText();
            }
            Global.hoverBox.ShowText(text);

        }

    }

    public virtual string GetInfoText()
    {
        return "";
    }
}
