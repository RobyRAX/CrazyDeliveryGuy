using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSkinButton : MonoBehaviour
{
    CharSkinController controller;
    public CharSkinDatabase charDB;

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindObjectOfType<CharSkinController>();

        if(gameObject.tag == "BatterSkinBtn")
        {
            if(index == controller.batterSelectedSkin)
                UpdateBatterSelectedUI();
        }
        else
        {
            if(index == controller.pitcherSelectedSkin)
                UpdatePitcherSelectedUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallUpdateSkin()
    {
        if(charDB.charSkins[index].unlocked)
        {
            if(controller.isSelectingBatter)
            {
                UpdateBatterSelectedUI();

                controller.batterSelectedSkin = index;
                StartCoroutine(controller.UpdateBatterSkinCo(index));

            }
            else
            {
                UpdatePitcherSelectedUI();

                controller.pitcherSelectedSkin = index;
                controller.UpdatePitcherSkin(index);
            }
        }      
    }

    public void UpdateBatterSelectedUI()
    {
        foreach(GameObject btn in GameObject.FindGameObjectsWithTag("BatterSkinBtn"))
        {
            btn.transform.GetChild(1).gameObject.SetActive(false);
        }
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public void UpdatePitcherSelectedUI()
    {
        foreach(GameObject btn in GameObject.FindGameObjectsWithTag("PitcherSkinBtn"))
        {
            btn.transform.GetChild(1).gameObject.SetActive(false);
        }
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void UpdateUnlockedUI()
    {
        if(charDB.charSkins[index].unlocked)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
