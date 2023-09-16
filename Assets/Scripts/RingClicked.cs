using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.EventSystems;

public class RingClicked : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public long clickAmount = 1;
    private Vector3 clickedScale;
    private Vector3 defaultScale;

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
    }

    private void OnClickAction()
    {
        if (Random.Range(0f, 1f) < ((float)bonusRingPercent/(float)100))
        {
            Global.incomeManager.AddRings(clickAmount * 10);
            Global.clickEffectSpawner.SpawnClickEffect(clickAmount * 10, true);
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

        if (Random.Range(0f, 1f) < ((float)boostChancePercent / (float)100))
        {
            Global.incomeManager.StartBoost();
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
