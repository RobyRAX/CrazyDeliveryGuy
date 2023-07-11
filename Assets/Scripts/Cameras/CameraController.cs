using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    SkinTabManager skinManager;

    public GameObject mainCam;
    public GameObject vehicleCam;
    public GameObject batterCam;
    public GameObject pitcherCam;
    public GameObject stickCam;

    // Start is called before the first frame update
    void Start()
    {
        skinManager = GameObject.FindObjectOfType<SkinTabManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(skinManager.currentTab == SkinTabManager.SkinTab.None)
        {
            mainCam.SetActive(true);
            vehicleCam.SetActive(false);
            batterCam.SetActive(false);
            pitcherCam.SetActive(false);
            stickCam.SetActive(false);
        }
        else if(skinManager.currentTab == SkinTabManager.SkinTab.Vehicle)
        {
            mainCam.SetActive(false);
            vehicleCam.SetActive(true);
            batterCam.SetActive(false);
            pitcherCam.SetActive(false);
            stickCam.SetActive(false);
        }
        else if(skinManager.currentTab == SkinTabManager.SkinTab.Batter)
        {
            mainCam.SetActive(false);
            vehicleCam.SetActive(false);
            batterCam.SetActive(true);
            pitcherCam.SetActive(false);
            stickCam.SetActive(false);
        }
        else if(skinManager.currentTab == SkinTabManager.SkinTab.Pitcher)
        {
            mainCam.SetActive(false);
            vehicleCam.SetActive(false);
            batterCam.SetActive(false);
            pitcherCam.SetActive(true);
            stickCam.SetActive(false);
        }
        else if(skinManager.currentTab == SkinTabManager.SkinTab.Stick)
        {
            mainCam.SetActive(false);
            vehicleCam.SetActive(false);
            batterCam.SetActive(false);
            pitcherCam.SetActive(false);
            stickCam.SetActive(true);
        }

        // if(Input.GetKey(KeyCode.C))
        // {
        //     mainCam.SetActive(false);
        //     vehicleCam.SetActive(true);
        // }
        // else
        // {
        //     mainCam.SetActive(true);
        //     vehicleCam.SetActive(false);
        // }
    }
}
