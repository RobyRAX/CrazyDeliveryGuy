using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDataManager : MonoBehaviour
{
    public int money;

    public TextMeshProUGUI moneyUI;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        moneyUI.text = money.ToString();
    }

    public void AddMoney(int moneyAdded)
    {
        money += moneyAdded;

        UpdateUI();
    }
}
