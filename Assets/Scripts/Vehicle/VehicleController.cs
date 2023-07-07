using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public GameManager gm;

    bool isOpened;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.currentState == GameManager.States.GameStart)
        {
            GetComponent<Animator>().SetBool("Open", true);       
        }
    }
}
