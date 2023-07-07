using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public Vector2 level = new Vector2(1, 1);
    LevelData levelData;

    [Header("UI Style - Gameplay")]
    public Color32 completedColor;
    public Color32 currentColor;
    public Color32 nextColor;

    [Header("Reference - Gameplay")]
    public GameManager gm;
    public Pitcher pitcher;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public GameObject[] levelImage;
    public GameObject[] stageImage;
    public GameObject[] Environments;

    [Header("UI Style - Level Selector")]
    public Color32 unlockedColor;
    public Color32 lockedColor;

    [Header("Reference - Level Selector")]
    public GameObject content;
    public GameObject stagePrefab;
    public GameObject levelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
        UpdateEnvironment();

        levelData = GetComponent<LevelData>();

        SpawnButton();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     UpdateLevelSelectColor();
        // }
    }

    public void UnlockLevel()
    {
        for(int i = 0; i < levelData.datas.Count; i++)
        {
            if(levelData.datas[i].level == level)
            {
                levelData.datas[i + 1].isUnlocked = true;

                break;
            }
        }
    }

    public void UpdateLevelSelectColor()
    {
        foreach(LevelData.Data data in levelData.datas)
        {
            GameObject stgBtn = GameObject.Find($"{data.level.x}-{data.level.y}");

            if(data.isUnlocked)
            {
                stgBtn.GetComponent<Image>().color = unlockedColor;
            }
            else
            {
                stgBtn.GetComponent<Image>().color = lockedColor;
            }
        }
    }

    public void SpawnButton()
    {
        foreach(LevelData.Data data in levelData.datas)
        {
            if(data.level.y == 1)
            {
                GameObject lvlImg;
                lvlImg = Instantiate(levelPrefab);
                lvlImg.transform.SetParent(content.transform);
                lvlImg.GetComponent<RectTransform>().localScale = Vector3.one;
                lvlImg.name = $"{data.level.x}";
                lvlImg.GetComponentInChildren<TextMeshProUGUI>().text = lvlImg.name;               
            }

            GameObject stgBtn;
            stgBtn = Instantiate(stagePrefab);
            stgBtn.transform.SetParent(content.transform);
            stgBtn.GetComponent<RectTransform>().localScale = Vector3.one;
            stgBtn.name = $"{data.level.x}-{data.level.y}";
            stgBtn.GetComponentInChildren<TextMeshProUGUI>().text = stgBtn.name;
            if(data.isUnlocked)
            {
                stgBtn.GetComponent<Image>().color = unlockedColor;
            }
            else
            {
                stgBtn.GetComponent<Image>().color = lockedColor;
            }
        }
    }

//------------------------------------------------------------------------------------------------

    public void UpdateData()
    {
        foreach(LevelData.Data data in levelData.datas)
        {
            if(level == data.level)
            {
                pitcher.minMaxBallShot = data.minMaxBallShot;
                pitcher.ballShotDelay = data.ballShotDelay;

                gm.capacityGoal = data.capacityGoal;
                gm.maxStock = data.maxStock;
            }
        }
    }

    public void LevelCounter()
    {
        level.y++;

        if(level.y > stageImage.Length)
        {
            level.x++;
            level.y = 1;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        currentLevelText.text = level.x.ToString();
        nextLevelText.text = (level.x + 1).ToString();

        foreach(GameObject obj in stageImage)
        {
            obj.GetComponent<Image>().color = nextColor;
        }

        for(int i = 0; i <= level.y - 1; i++)
        {
            stageImage[Mathf.Clamp(i - 1, 0, stageImage.Length - 1)].GetComponent<Image>().color = completedColor;
            stageImage[Mathf.Clamp(i, 0, stageImage.Length - 1)].GetComponent<Image>().color = currentColor;
        }
    }

    public void UpdateEnvironment()
    {
        GameObject currentEnvi =  GameObject.FindGameObjectWithTag("Envi");
        Destroy(currentEnvi);

        Instantiate(Environments[Mathf.RoundToInt(level.x) - 1]);
    }

    public void SelectLevel()
    {
        
    }
}
