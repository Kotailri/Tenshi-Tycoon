using UnityEngine;
using UnityEngine.EventSystems;

public class RingClicked : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public long clickAmount = 1;
    private Vector3 clickedScale = new(0.85f, 0.85f, 1);

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = clickedScale;
        Global.incomeManager.AddRings(clickAmount);
    }
}
