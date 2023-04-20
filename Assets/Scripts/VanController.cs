using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanController : MonoBehaviour
{
    public GameManager gm;

    bool isOpened;

    // Start is called before the first frame update
    void Start()
    {
        
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
