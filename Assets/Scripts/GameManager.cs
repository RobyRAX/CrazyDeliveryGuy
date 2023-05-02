using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum States
    {
        Menu,
        GameStart,
        GamePlay,
        GameOver,
        Result,
    }

    //

    //---------------------------------------------------------------------

    [Header("Status")]
    public States currentState;

    //---------------------------------------------------------------------

    [Header("Reference")]
    //public Button playButton;
    public PlayerDataManager playerData;
    public TextMeshProUGUI stockText;
    public TextMeshProUGUI goalText;
    public GameObject WinLoseFX;
    public GameObject StartFX;
    public GameObject ResultPanel;
    public GameObject MenuPanel;
    public GameObject ParameterPanel;
    public TextMeshProUGUI bonusStockText;
    public GameObject TextFX;
    //public GameObject SlideControl;

    //---------------------------------------------------------------------

    [Header("Parameter")] 
    public float gravity; 
    public int capacityGoal;
    public int maxStock;
    public GameObject[] stockPrefab;
    [SerializeField] float delayToResult;
    [SerializeField] float delayToBonus;
    [SerializeField] float delayToGameplay;

    //---------------------------------------------------------------------

    [Header("Variable")]
    public bool gameWin;
    public bool gameSet;
    public bool gameOverFXShown;
    [SerializeField] private float currentTimer;
    public int remainingCapacityGoal;
    public int currentStockMax;
    public int remainingStock;
    public List<GameObject> Stocks;

    bool bonusMoneyAdded;

    //---------------------------------------------------------------------
    
    void Start()
    {
        Physics.gravity = Vector3.up * gravity;

        Time.timeScale = 1f;
    }

    void Update()
    {
        if(currentState == States.Menu)    //-----------------ViewLevel
        {
            bonusMoneyAdded = false;
            gameSet = false;
            MenuPanel.SetActive(true);
            ParameterPanel.SetActive(false);
            //SlideControl.SetActive(false);
            ResultPanel.SetActive(false);
            ResultPanel.transform.GetChild(0).gameObject.SetActive(false);
            ResultPanel.transform.GetChild(1).gameObject.SetActive(false);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                currentState = States.GameStart;
            }

            //Change State --> via Button
        }
        else if(currentState == States.GameStart)
        {
            if(!gameSet)
            {
                remainingCapacityGoal = 0;
                gameOverFXShown = false;

                WinLoseFX.SetActive(false);
                WinLoseFX.transform.GetChild(0).gameObject.SetActive(false);
                WinLoseFX.transform.GetChild(1).gameObject.SetActive(false);
                MenuPanel.SetActive(false);
                ParameterPanel.SetActive(true);

                currentTimer = delayToGameplay;

                CreateStock();                 
                gameSet = true;
            }
            else if(gameSet)
            {
                currentTimer -= Time.deltaTime;

                if(currentTimer <= 2)
                {
                    StartFX.SetActive(true);
                }
                    

                if(currentTimer <= 0)
                {
                    currentState = States.GamePlay;

                    StartFX.SetActive(false);
                    //SlideControl.SetActive(true);
                }
                    
            }
        }
        else if(currentState == States.GamePlay)
        {
            if(remainingCapacityGoal >= capacityGoal)
            {
                gameWin = true;
                currentState = States.GameOver;   
            }
            if(Stocks.Count <= 0 && remainingCapacityGoal < capacityGoal && GameObject.FindGameObjectsWithTag("Package").Length == 0)
            {
                gameWin = false;
                currentState = States.GameOver;                           
            }
        }
        else if(currentState == States.GameOver)
        {
            if(!gameOverFXShown)
            {
                currentTimer = delayToResult;

                ParameterPanel.SetActive(false);
                //SlideControl.SetActive(false);

                WinLoseFX.SetActive(true);
                if(gameWin)
                { 
                    WinLoseFX.transform.GetChild(0).gameObject.SetActive(true);

                    gameOverFXShown = true;

                }
                else
                {
                    WinLoseFX.transform.GetChild(1).gameObject.SetActive(true);

                    gameOverFXShown = true;
                }
            }
            else if(gameOverFXShown)
            {
                currentTimer -= Time.deltaTime;

                if(currentTimer <= 0)
                {
                    currentState = States.Result;

                    currentTimer = delayToBonus;
                }
            }
        }
        else if(currentState == States.Result)
        {
            WinLoseFX.SetActive(false);

            ResultPanel.SetActive(true);
            if(gameWin)
            { 
                ResultPanel.transform.GetChild(0).gameObject.SetActive(true);

                gameOverFXShown = true;

                if(!bonusMoneyAdded)
                {
                    currentTimer -= Time.deltaTime;
                    bonusStockText.text = " ";

                    if(currentTimer <= delayToBonus/2)
                    {
                        bonusStockText.text = remainingStock + " / " + currentStockMax;
                    }
                    
                    if(currentTimer <= 0)
                    {

                        float stockToMoney = (maxStock/(float)capacityGoal) * remainingStock;
                        Debug.Log((maxStock/(float)capacityGoal).ToString());
                        playerData.AddMoney(stockToMoney);

                        GameObject FX = Instantiate(TextFX, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
                        FX.GetComponentInChildren<TextMeshProUGUI>().text = "+" + stockToMoney.ToString("F0");
                        FX.transform.position = bonusStockText.gameObject.transform.position + new Vector3(200, 150, 0);
                        FX.GetComponentInChildren<TextMeshProUGUI>().fontSize = 200;
                        Destroy(FX, 2f);

                        bonusMoneyAdded = true;
                    }

                    
                }
                
            }
            else
            {
                ResultPanel.transform.GetChild(1).gameObject.SetActive(true);

                gameOverFXShown = true;
            }
        }
    }

    public void ChangeState(States nextState)
    {
        currentState = nextState;
    }

    public void RestartState()
    {
        currentState = States.Menu;
    }

    public void GameStartState()
    {
        currentState = States.GameStart;
    }

    void CreateStock()
    {
        while(remainingStock <= maxStock)
        {
            GameObject stock = stockPrefab[Random.Range(0, stockPrefab.Length)];

            Stocks.Add(stock);

            remainingStock += stock.GetComponent<PackageController>().Capacity;
        }
        
        currentStockMax = remainingStock;
        UpdateStockGoalUI();
    }

    public void AddCapacity(int stockIn)
    {
        remainingCapacityGoal += stockIn;

        UpdateStockGoalUI();
    }

    public void RemoveStock(int index)
    {
        remainingStock -= Stocks[0].GetComponent<PackageController>().Capacity;

        Stocks.RemoveAt(index);   

        UpdateStockGoalUI();    
    }

    //--------------------------------------------------------------------------------------

    public void UpdateStockGoalUI()
    {
        stockText.text = remainingStock.ToString();
        goalText.text = remainingCapacityGoal + " / " + capacityGoal;
    }
}
