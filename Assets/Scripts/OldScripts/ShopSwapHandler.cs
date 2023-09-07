using UnityEngine;
using UnityEngine.UI;

public class ShopSwapHandler : MonoBehaviour
{
    public Button shopButton;
    public Button upgradeButton;

    [Space(15.0f)]
    public Transform Shop;
    public Transform Upgrades;

    private Vector3 onPos;
    private Vector3 offPos;

    void Start()
    {
        onPos = Shop.position;
        offPos = Upgrades.position;

        shopButton.interactable = false;
        upgradeButton.interactable = true;
    }

    public void ShopButtonPressed()
    {
        shopButton.interactable = false;
        upgradeButton.interactable = true;
        Upgrades.position = offPos;
        Shop.position = onPos;
    }
    public void UpgradeButtonPressed()
    {
        shopButton.interactable = true;
        upgradeButton.interactable = false;
        Upgrades.position = onPos;
        Shop.position = offPos;
    }
}
