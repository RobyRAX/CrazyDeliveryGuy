using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDataManager : MonoBehaviour
{
    public int money;

    public TextMeshProUGUI moneyUI;

    //[SerializeField] bool moneyAnimDone;

    void Update()
    {
       
    }

    public void AddMoney(int moneyAdded)
    {
        money += moneyAdded;
        moneyUI.GetComponent<NumberCounter>().Value = money;
    }
}
