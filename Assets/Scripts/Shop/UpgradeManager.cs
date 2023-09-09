using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public Transform upgradeContent;
    private List<ShopUpgrade> upgradeList = new();
    public Transform basePosition;

    void Awake()
    {
        Vector3 firstPosition = basePosition.position;
        float yPos = 0.0f;

        foreach (Transform child in upgradeContent)
        {
            if (child.TryGetComponent(out ShopUpgrade up))
            {
                up.transform.position = firstPosition + new Vector3(0, yPos, 0);
                upgradeList.Add(up);
                yPos -= 130f;
            }
        }
    }
}
