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
    public LevelManager levelMan;
    public FirebaseLevelTrack levelTrack;
    public PlayerDataManager playerData;
    public TextMeshProUGUI stockText;
    public TextMeshProUGUI goalText;
    public GameObject WinLoseFX;
    public GameObject StartFX;
    public GameObject ResultPanel;
    public GameObject MenuPanel;
    public GameObject HitZone;
    public GameObject ParameterPanel;
    public GameObject LevelPanel;
    public GameObject SkinPanel;
    public GameObject bonusText;
    public GameObject TextFX;
    public GameObject RestartButton;
    //public GameObject SlideControl;

//---------------------------------------------------------------------

//---------------------------------------------------------------------

    [Header("Parameter - Editable Input")] 
    public float gravity; 
    public int capacityGoal;
    public int maxStock;
    public int maxBonusPerStock;
    public GameObject[] stockPrefab;
    [SerializeField] float delayToResult;
    [SerializeField] float delayToBonus;
    [SerializeField] float delayToGameplay;

//---------------------------------------------------------------------

    [Header("Variable - No Need to Edit")]
    [HideInInspector] public bool gameWin;
    [HideInInspector] public bool gameSet;
    [HideInInspector] public bool gameOverFXShown;
    [SerializeField] private float currentTimer;
    public int remainingCapacityGoal;
    public int currentStockMax;
    public int remainingStock;
    public List<GameObject> Stocks;

    bool bonusMoneyAdded;
    bool menuInitiated;
    GameObject FX;

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
            if(!menuInitiated)
            {
                levelMan.UpdateData();
                levelMan.UpdateEnvironment();
                // levelMan.UpdateUI();

                Destroy(FX);
                bonusMoneyAdded = false;
                gameSet = false;
                MenuPanel.SetActive(true);
                ParameterPanel.SetActive(false);
                LevelPanel.SetActive(false);
                SkinPanel.SetActive(false);
                //SlideControl.SetActive(false);
                ResultPanel.SetActive(false);
                ResultPanel.transform.GetChild(0).gameObject.SetActive(false);
                ResultPanel.transform.GetChild(1).gameObject.SetActive(false);
                RestartButton.SetActive(false);

                HitZone.SetActive(false);

                menuInitiated = true;
            }
            // if(Input.GetKeyDown(KeyCode.Space))
            // {
            //     currentState = States.GameStart;
            // }

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
                LevelPanel.SetActive(true);
                HitZone.SetActive(true);

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
                GetComponent<SoundController>().PlayCheer();

                gameWin = true;                
                currentState = States.GameOver;   
            }
            if(Stocks.Count <= 0 && remainingCapacityGoal < capacityGoal && GameObject.FindGameObjectsWithTag("Package").Length == 0)
            {
                GetComponent<SoundController>().PlayBoo();

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
                if(gameWin) //-------------------------------------------------------------- WIN
                {            
                    //levelTrack.LogEventLevel();

                    levelMan.UnlockLevel();
                    levelMan.LevelCounter();

                    WinLoseFX.transform.GetChild(0).gameObject.SetActive(true);

                    gameOverFXShown = true;
                }
                else        //-------------------------------------------------------------- LOSE
                {
                    //levelTrack.LogEventLevel();

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
            LevelPanel.SetActive(false);

            ResultPanel.SetActive(true);
            if(gameWin)     //-------------------------------------------------------------- WIN
            { 
                //GetComponent<SoundController>().PlayCheer();
                ResultPanel.transform.GetChild(0).gameObject.SetActive(true);

                gameOverFXShown = true;

                if(!bonusMoneyAdded)
                {                   
                    currentTimer -= Time.deltaTime;
                    bonusText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"(100 - ({maxStock} - {capacityGoal}))";
                    bonusText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"x  {remainingStock}";

                    foreach(Transform child in bonusText.transform.GetComponentInChildren<Transform>())
                    {
                        child.gameObject.SetActive(false);
                    }

                    if(currentTimer <= delayToBonus/2)
                    {
                        bonusText.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    if(currentTimer <= delayToBonus/2 - 0.25f)
                    {
                        bonusText.transform.GetChild(2).gameObject.SetActive(true);
                    }
                    if(currentTimer <= delayToBonus/2 - 0.5f)
                    {
                        bonusText.transform.GetChild(3).gameObject.SetActive(true);
                    }
                    if(currentTimer <= delayToBonus/2 - 0.75f)
                    {
                        bonusText.transform.GetChild(1).gameObject.SetActive(true);
                    }
                    
                    if(currentTimer <= 0)
                    {
                        RestartButton.SetActive(true);

                        float stockToMoney = (maxBonusPerStock - (maxStock - capacityGoal)) / 10f * remainingStock;
                        Debug.Log(stockToMoney);
                        playerData.AddMoney((int)stockToMoney);

                        FX = Instantiate(TextFX, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
                        FX.GetComponentInChildren<TextMeshProUGUI>().text = "+" + stockToMoney.ToString("F0");
                        FX.transform.position = bonusText.gameObject.transform.parent.position + new Vector3(315, 150, 0);
                        FX.GetComponentInChildren<TextMeshProUGUI>().fontSize = 200;

                        bonusMoneyAdded = true;
                    }     
                }
                
            }
            else            //-------------------------------------------------------------- LOSE
            {
                currentTimer -= Time.deltaTime;
                ResultPanel.transform.GetChild(1).gameObject.SetActive(true);

                foreach(Transform child in bonusText.transform.GetComponentInChildren<Transform>())
                {
                    child.gameObject.SetActive(false);
                }

                if(currentTimer <= delayToBonus/2)
                {
                    RestartButton.SetActive(true);

                    bonusText.transform.GetChild(4).gameObject.SetActive(true);
                }

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

        menuInitiated = false;
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
