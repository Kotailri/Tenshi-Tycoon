using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public Transform basePosition;
    public GameObject perkStoreContent;
    public List<Perk> perkList = new();

    private void Awake()
    {
        Global.perkManager = this;

        Vector3 firstPosition = basePosition.position;
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
}
