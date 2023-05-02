using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDataManager : MonoBehaviour
{
    public float money;

    public TextMeshProUGUI moneyUI;

    //[SerializeField] bool moneyAnimDone;
    [SerializeField] float moneyToBeAdded;
    [SerializeField] float setTimer;
    [SerializeField] float currentTimer;

    float defaultFontSize;

    void Start()
    {
        UpdateUI();

        currentTimer = setTimer;

        defaultFontSize = moneyUI.fontSize;
    }

    void Update()
    {
        if(moneyToBeAdded > 0)
        {
            currentTimer -= Time.deltaTime;
            moneyUI.fontSize = defaultFontSize + 50;

            if(currentTimer <= 0)
            {
                moneyToBeAdded--;
                money++;

                UpdateUI();

                currentTimer = setTimer;
            }               
        }    
        else
            moneyUI.fontSize = defaultFontSize;       
    }

    public void UpdateUI()
    {
        moneyUI.text = money.ToString();
    }

    public void AddMoney(float moneyAdded)
    {
        //moneyAnimDone = false;
        moneyToBeAdded = moneyAdded;

        //UpdateUI();
    }
}
