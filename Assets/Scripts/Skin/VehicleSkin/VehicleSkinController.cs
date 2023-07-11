using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSkinController : MonoBehaviour
{
    public VehicleSkinDatabase vehicleDB;
    public Transform parent;
    public Transform cols;

    public int selectedSkin;

    [Header("UI - Skin Selection")]
    public GameObject content;
    public GameObject buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnSkinSelection();
        UpdateSkin(selectedSkin);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     UpdateSkin(selectedDB);
        // }
    }

    public void UpdateSkin(int inputSelectedDB)
    {
        Destroy(GameObject.FindGameObjectWithTag("Vehicle"));

        VehicleSkin vehicleSkin = vehicleDB.GetVehicleSkin(inputSelectedDB);

        Instantiate(vehicleSkin.skinObj, parent);
        cols.localPosition = vehicleSkin.colliderPos;
    }

    public void SpawnSkinSelection()
    {
        // foreach(VehicleSkin skin in vehicleDB.vehicleSkins)
        // {
        //     GameObject button;
        //     button = Instantiate(buttonPrefab, content.transform);

        //     button.GetComponentInChildren<Text>().text = skin.name;
        //     button.GetComponent<VehicleSkinButton>().index = skin.
        // }
        for(int i = 0; i < vehicleDB.vehicleSkins.Length; i++)
        {
            GameObject button;
            button = Instantiate(buttonPrefab, content.transform);

            button.name = vehicleDB.vehicleSkins[i].name + "_SkinButton";
            button.GetComponentInChildren<Text>().text = vehicleDB.vehicleSkins[i].name;
            button.GetComponent<VehicleSkinButton>().index = i;
            button.GetComponent<VehicleSkinButton>().UpdateUnlockedUI();

            // if(vehicleDB.vehicleSkins[i].unlocked)
            // {
            //     button.transform.GetChild(0).gameObject.SetActive(false);
            // }
        }
    }
}
