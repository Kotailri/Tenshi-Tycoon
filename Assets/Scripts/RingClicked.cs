using UnityEngine;
using UnityEngine.EventSystems;

public class RingClicked : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public long clickAmount = 1;
    private Vector3 clickedScale;
    private Vector3 defaultScale;

    private void Start()
    {
        defaultScale = transform.localScale;
        clickedScale = new Vector3(0.85f * transform.localScale.x, 0.85f * transform.localScale.y, transform.localScale.z);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = defaultScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = clickedScale;
        Global.incomeManager.AddRings(clickAmount);
    }
}
