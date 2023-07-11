using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinTabManager : MonoBehaviour
{
    public Color32 unselect;
    public Color32 selected;

    public GameObject vehicleTab;
    public Button vehicleBtn;
    public Transform vehicleRoot;
    Vector3 vehicleDefPos;
    Quaternion vehicleDefRot;
    public Transform vehicleSelectTransform;

    public GameObject charTab;
    public GameObject batterTab;
    public Transform batterRoot;
    Vector3 batterDefPos;
    Quaternion batterDefRot;
    public Transform batterSelectTransform;

    public GameObject pitcherTab;
    public Button charBtn;
    public GameObject stickTab;
    public Button stickBtn;

    public enum SkinTab
    {
        None,
        Vehicle,
        Batter,
        Pitcher,
        Stick,
    }

    public SkinTab currentTab;

    // Start is called before the first frame update
    void Start()
    {
        vehicleDefPos = vehicleRoot.transform.position;
        vehicleDefRot = vehicleRoot.transform.rotation;

        batterDefPos = batterRoot.transform.position;
        batterDefRot = batterRoot.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseSkinTab()
    {
        currentTab = SkinTab.None;

        StartCoroutine(ResetVehicleCo());
        StartCoroutine(ResetBatterCo());
    }

    public void OpenVehicle()
    {
        currentTab = SkinTab.Vehicle;

        vehicleTab.SetActive(true);
        charTab.SetActive(false);
        stickTab.SetActive(false);

        vehicleBtn.image.color = selected;
        charBtn.image.color = unselect;
        stickBtn.image.color = unselect;

        StartCoroutine(RotateVehicleCo());
        StartCoroutine(ResetBatterCo());
    }

    IEnumerator RotateVehicleCo()
    {
        // GameObject vehicle = GameObject.FindGameObjectWithTag("Vehicle");
        //defVehiclePos = vehicle.transform;
        float elapsedTime = 0;
        float waitTime = 1;

        while(elapsedTime < waitTime)
        {
            vehicleRoot.transform.position = Vector3.Lerp(vehicleRoot.transform.position, vehicleSelectTransform.position, (elapsedTime / waitTime));
            vehicleRoot.transform.rotation = Quaternion.Slerp(vehicleRoot.transform.rotation, vehicleSelectTransform.rotation, (elapsedTime / waitTime));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator ResetVehicleCo()
    {
        float elapsedTime = 0;
        float waitTime = 1;

        while(elapsedTime < waitTime)
        {
            vehicleRoot.transform.position = Vector3.Lerp(vehicleRoot.transform.position, vehicleDefPos, (elapsedTime / waitTime));
            vehicleRoot.transform.rotation = Quaternion.Slerp(vehicleRoot.transform.rotation, vehicleDefRot, (elapsedTime / waitTime));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    public void OpenChar()
    {
        if(GetComponent<CharSkinController>().isSelectingBatter)
        {
            currentTab = SkinTab.Batter;

            StartCoroutine(RotateBatterCo());

        }         
        else if(!GetComponent<CharSkinController>().isSelectingBatter)
        {
            currentTab = SkinTab.Pitcher;

            StartCoroutine(RotateBatterCo());
        }
            
        vehicleTab.SetActive(false);
        charTab.SetActive(true);
        stickTab.SetActive(false);

        vehicleBtn.image.color = unselect;
        charBtn.image.color = selected;
        stickBtn.image.color = unselect;

        StartCoroutine(ResetVehicleCo());
    }

    IEnumerator RotateBatterCo()
    {
        float elapsedTime = 0;
        float waitTime = 1;

        while(elapsedTime < waitTime)
        {
            batterRoot.transform.position = Vector3.Lerp(batterRoot.transform.position, batterSelectTransform.position, (elapsedTime / waitTime));
            batterRoot.transform.rotation = Quaternion.Slerp(batterRoot.transform.rotation, batterSelectTransform.rotation, (elapsedTime / waitTime));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator ResetBatterCo()
    {
        float elapsedTime = 0;
        float waitTime = 1;

        while(elapsedTime < waitTime)
        {
            batterRoot.transform.position = Vector3.Lerp(batterRoot.transform.position, batterDefPos, (elapsedTime / waitTime));
            batterRoot.transform.rotation = Quaternion.Slerp(batterRoot.transform.rotation, batterDefRot, (elapsedTime / waitTime));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    public void OpenStick()
    {
        currentTab = SkinTab.Stick;

        vehicleTab.SetActive(false);
        charTab.SetActive(false);
        stickTab.SetActive(true);

        vehicleBtn.image.color = unselect;
        charBtn.image.color = unselect;
        stickBtn.image.color = selected;

        StartCoroutine(ResetVehicleCo());
        StartCoroutine(ResetBatterCo());
    }

    public void SwitchChar()
    {
        if(GetComponent<CharSkinController>().isSelectingBatter)
        {
            currentTab = SkinTab.Pitcher;
            GetComponent<CharSkinController>().isSelectingBatter = false;

            batterTab.SetActive(false);
            pitcherTab.SetActive(true);

            charBtn.GetComponentInChildren<Text>().text = "Pitcher";
        }
        else if(!GetComponent<CharSkinController>().isSelectingBatter)
        {
            currentTab = SkinTab.Batter;
            GetComponent<CharSkinController>().isSelectingBatter = true;

            batterTab.SetActive(true);
            pitcherTab.SetActive(false);

            charBtn.GetComponentInChildren<Text>().text = "Batter";
        }
    }

    
}
