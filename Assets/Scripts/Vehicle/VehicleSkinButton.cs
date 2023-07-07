using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSkinButton : MonoBehaviour
{
    VehicleSkinController controller;
    public VehicleSkinDatabase vehicleDB;

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindObjectOfType<VehicleSkinController>();

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
        if(vehicleDB.vehicleSkins[index].unlocked)
        {
            UpdateSelectedUI();

            controller.selectedSkin = index;
            controller.UpdateSkin(index);
        }      
    }

    public void UpdateSelectedUI()
    {
        foreach(GameObject btn in GameObject.FindGameObjectsWithTag("VehicleSkinBtn"))
        {
            btn.transform.GetChild(1).gameObject.SetActive(false);
        }
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void UpdateUnlockedUI()
    {
        if(vehicleDB.vehicleSkins[index].unlocked)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
