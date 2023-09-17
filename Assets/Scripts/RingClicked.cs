
using UnityEngine;
using UnityEngine.EventSystems;

public class RingClicked : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler
    , IPointerExitHandler
{
    public long clickAmount = 1;
    private Vector3 clickedScale;
    private Vector3 defaultScale;
    private Vector3 hoverScale;

    public int bonusRingPercent = 0;
    public int boostChancePercent = 0;

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
        hoverScale = new Vector3(1.2f * transform.localScale.x, 1.2f * transform.localScale.y, transform.localScale.z);
    }

    private void OnClickAction()
    {
        if (Random.Range(0f, 1f) < ((float)bonusRingPercent/(float)100))
        {
            Global.incomeManager.AddRings(clickAmount * 10);
            Global.clickEffectSpawner.SpawnClickEffect(clickAmount * 10, true);
            AudioManager.instance.PlaySound("blip");
        }
        else if (Global.incomeManager.boostEnabled)
        {
            Global.incomeManager.AddRings(clickAmount * Global.incomeManager.GetBoostMultiplier());
            Global.clickEffectSpawner.SpawnClickEffect(clickAmount * Global.incomeManager.GetBoostMultiplier());
        }
        else
        {
            Global.incomeManager.AddRings(clickAmount);
            Global.clickEffectSpawner.SpawnClickEffect(clickAmount);
        }
    }

    public void AddBonusRingChance(int bonusRingPercent)
    {
        this.bonusRingPercent += bonusRingPercent;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound("ringclick2");
        OnClickAction();
        transform.localScale = defaultScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound("ringclick");
        if (Random.Range(0f, 1f) < ((float)boostChancePercent / (float)100))
        {
            Global.incomeManager.StartBoost();
            AudioManager.instance.PlaySound("success2");
        }
        OnClickAction();
        transform.localScale = clickedScale;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = defaultScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = hoverScale;
    }
}
