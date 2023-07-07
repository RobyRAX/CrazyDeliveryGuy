using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickSkinController : MonoBehaviour
{
    public StickSkinDatabase stickDB;
    Transform parent;

    public int selectedSkin;

    [Header("UI - Skin Selection")]
    public GameObject content;
    public GameObject buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.FindGameObjectWithTag("StickParent").transform;

        SpawnSkinSelection();
        UpdateSkin(selectedSkin);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UpdateSkin(selectedSkin);
        }
    }

    public void UpdateSkin(int inputSelectedDB)
    {
        Destroy(GameObject.FindGameObjectWithTag("Stick"));

        StickSkin stickSkin = stickDB.GetStickSkin(inputSelectedDB);

        Instantiate(stickSkin.skinObj, parent);
    }

    public void SpawnSkinSelection()
    {
        for(int i = 0; i < stickDB.stickSkins.Length; i++)
        {
            GameObject button;
            button = Instantiate(buttonPrefab, content.transform);

            button.name = stickDB.stickSkins[i].name + "_SkinButton";
            button.GetComponentInChildren<Text>().text = stickDB.stickSkins[i].name;
            button.GetComponent<StickSkinButton>().index = i;
            button.GetComponent<StickSkinButton>().UpdateUnlockedUI();

            // if(vehicleDB.vehicleSkins[i].unlocked)
            // {
            //     button.transform.GetChild(0).gameObject.SetActive(false);
            // }
        }
    }
}
