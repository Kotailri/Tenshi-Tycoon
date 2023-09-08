using UnityEngine;
using UnityEngine.EventSystems;

public class HasHoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public string text;
    private bool showing = false;
    private bool isEnabled = true;

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
        if (Global.hoverBox)
        {
            showing = false;
            Global.hoverBox.HideText();
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
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
