using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickSkinButton : MonoBehaviour
{
    StickSkinController controller;
    public StickSkinDatabase stickDB;

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindObjectOfType<StickSkinController>();

        if(index == controller.selectedSkin)
        {
            UpdateSelectedUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallUpdateSkin()
    {
        if(stickDB.stickSkins[index].unlocked)
        {
            UpdateSelectedUI();

            controller.selectedSkin = index;
            controller.UpdateSkin(index);
        }      
    }

    public void UpdateSelectedUI()
    {
        foreach(GameObject btn in GameObject.FindGameObjectsWithTag("StickSkinBtn"))
        {
            btn.transform.GetChild(1).gameObject.SetActive(false);
        }
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void UpdateUnlockedUI()
    {
        if(stickDB.stickSkins[index].unlocked)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
