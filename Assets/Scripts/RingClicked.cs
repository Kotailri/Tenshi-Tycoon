using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.EventSystems;

public class RingClicked : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public long clickAmount = 1;
    private Vector3 clickedScale;
    private Vector3 defaultScale;

    public int bonusRingPercent = 0;
    public bool bonusRateTime = false;

    private void Awake()
    {
        Global.ringClicker = this;
    }

    public void AddClickAmount(long _clickAmount)
    {
        clickAmount += _clickAmount;
    }

    private void Start()
    {
        defaultScale = transform.localScale;
        clickedScale = new Vector3(0.85f * transform.localScale.x, 0.85f * transform.localScale.y, transform.localScale.z);
    }

    private void OnClickAction()
    {
        if (Random.Range(0f, 1f) < ((float)bonusRingPercent/(float)100))
        {
            Global.incomeManager.AddRings(clickAmount * 10);
        }
        else
        {
            Global.incomeManager.AddRings(clickAmount);
        }
    }

    public void AddBonusRingChance(int bonusRingPercent)
    {
        this.bonusRingPercent += bonusRingPercent;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnClickAction();
        transform.localScale = defaultScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClickAction();
        transform.localScale = clickedScale;
    }
}
