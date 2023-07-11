using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSkinController : MonoBehaviour
{
    public CharSkinDatabase charDB;
    public Transform batterParent;
    public Transform pitcherParent;
    public Transform pitcherTransform;

    public int batterSelectedSkin;
    public int pitcherSelectedSkin;
    public RuntimeAnimatorController batterAnimControl;
    public RuntimeAnimatorController pitcherAnimControl;

    [Header("UI - Skin Selection")]
    public GameObject batterContent;
    public GameObject pitcherContent;
    public GameObject buttonPrefab;
    // public GameObject charTab;
    public bool isSelectingBatter = true;

    // Start is called before the first frame update
    void Start()
    {
        SpawnSkinSelection();
        StartCoroutine(UpdateBatterSkinCo(batterSelectedSkin));
        UpdatePitcherSkin(pitcherSelectedSkin);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     UpdateSkin(selectedDB);
        // }
    }

    // public void SwitchCharTab()
    // {
    //     if(isSelectingBatter)
    //     {
    //         isSelectingBatter = false;

    //         batterContent.SetActive(false);
    //         pitcherContent.SetActive(true);

    //         charTab.GetComponentInChildren<Text>().text = "Pitcher";
    //     }
    //     else
    //     {
    //         isSelectingBatter = true;

    //         batterContent.SetActive(true);
    //         pitcherContent.SetActive(false);

    //         charTab.GetComponentInChildren<Text>().text = "Batter";
    //     }
    // }

    public IEnumerator UpdateBatterSkinCo(int inputSelectedDB)
    {
        Destroy(GameObject.FindGameObjectWithTag("BatterSkin"));

        CharSkin charSkin = charDB.GetCharSkin(inputSelectedDB);

        GameObject skin = Instantiate(charSkin.skinObj, batterParent);
        skin.tag = "BatterSkin";
        skin.GetComponent<Animator>().runtimeAnimatorController = batterAnimControl;

        yield return new WaitForEndOfFrame();

        //Spawn Stick Again
        GetComponent<StickSkinController>().UpdateSkin(GetComponent<StickSkinController>().selectedSkin);
    }

    // public void UpdateBatterSkin(int inputSelectedDB)
    // {
    //     Destroy(GameObject.FindGameObjectWithTag("BatterSkin"));

    //     CharSkin charSkin = charDB.GetCharSkin(inputSelectedDB);

    //     GameObject skin = Instantiate(charSkin.skinObj, batterParent);
    //     skin.tag = "BatterSkin";
    //     skin.GetComponent<Animator>().runtimeAnimatorController = batterAnimControl;

    //     //GetComponent<StickSkinController>().UpdateSkin(GetComponent<StickSkinController>().selectedSkin);      
    // }

    public void UpdatePitcherSkin(int inputSelectedDB)
    {
        Destroy(GameObject.FindGameObjectWithTag("PitcherSkin"));

        CharSkin charSkin = charDB.GetCharSkin(inputSelectedDB);

        GameObject skin = Instantiate(charSkin.skinObj, pitcherParent);
        skin.tag = "PitcherSkin";
        skin.GetComponent<Animator>().runtimeAnimatorController = pitcherAnimControl;
        skin.transform.localPosition = pitcherTransform.localPosition;   
        skin.transform.localRotation = pitcherTransform.localRotation;
        skin.transform.localScale = pitcherTransform.localScale;  
        Destroy(skin.transform.GetChild(1).transform.GetChild(1).gameObject);
    }

    public void SpawnSkinSelection()
    {
        for(int i = 0; i < charDB.SkinCount; i++)
        {
            GameObject button;
            button = Instantiate(buttonPrefab, batterContent.transform);

            button.name = charDB.charSkins[i].name + "_BatterSkinButton";
            button.tag = "BatterSkinBtn";
            button.GetComponentInChildren<Text>().text = charDB.charSkins[i].name;
            button.GetComponent<CharSkinButton>().index = i;
            button.GetComponent<CharSkinButton>().UpdateUnlockedUI();

            // if(vehicleDB.vehicleSkins[i].unlocked)
            // {
            //     button.transform.GetChild(0).gameObject.SetActive(false);
            // }
        }
        for(int i = 0; i < charDB.SkinCount; i++)
        {
            GameObject button;
            button = Instantiate(buttonPrefab, pitcherContent.transform);

            button.name = charDB.charSkins[i].name + "_PitcherSkinButton";
            button.tag = "PitcherSkinBtn";
            button.GetComponentInChildren<Text>().text = charDB.charSkins[i].name;
            button.GetComponent<CharSkinButton>().index = i;
            button.GetComponent<CharSkinButton>().UpdateUnlockedUI();

            // if(vehicleDB.vehicleSkins[i].unlocked)
            // {
            //     button.transform.GetChild(0).gameObject.SetActive(false);
            // }
        }
    }
}
