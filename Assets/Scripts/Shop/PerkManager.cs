using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public Transform basePosition;
    public GameObject perkStoreContent;
    public List<Perk> perkList = new();

    private Vector3 firstPosition;

    private void Awake()
    {
        Global.perkManager = this;
        firstPosition = basePosition.position;
    }

    private void Start()
    {
        PositionPerks();
        CheckPerkRequirements();
    }

    public void PositionPerks()
    {
        float yPos = 0.0f;
        foreach (Transform t in perkStoreContent.transform)
        {
            if (t.TryGetComponent(out Perk perk))
            {
                perkList.Add(perk);

                t.transform.position = firstPosition + new Vector3(0, yPos, 0);
                yPos -= 170f;
            }
        }
    }

    private void SortPerksByPrice()
    {
        foreach (Perk perk in perkList.OrderBy(o => o.PerkPrice).ToList())
        {
            print(perk.PerkName);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) 
        {
            SortPerksByPrice(); 
        }
    }

    public Perk FindPerkByName(string _perkName)
    {
        foreach (Perk perk in perkList)
        {
            if (perk.PerkName.Equals(_perkName))
            {
                return perk;
            }
        }
        Debug.LogWarning($"Perk '{_perkName}' not found!");
        return null;
    }

    public void CheckPerkRequirements()
    {
        foreach (Perk perk in perkList)
        {
            perk.CheckRequiredPerk();
        }
    }

    public void ApplyPerkUpdates()
    {
        foreach (Perk perk in perkList)
        {
            perk.ApplyPerkUpdate();
        }
    }
}
