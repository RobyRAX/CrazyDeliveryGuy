using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum States
    {
        ViewLevel,
        Aim,
        Power,
        Shoot,
        BallFly,
        BallStop,
    }

    [Header("Status")]
    public States currentState;
    [SerializeField] bool isAiming;

    [Header("Reference")]
    public Animator camAnim;
    public TargetController target;
    public Camera[] cameras;
    public BallLauncher launcher;
    public GameObject ball;

    [Header("Parameter")]
    [SerializeField] private float timerBeforeShoot;
    [SerializeField] private float hToActivateCatCam;
    [SerializeField] private float timerBeforeCamZoom;

    [Header("Variable")]
    [SerializeField] private float currentTimer;
    

    void Start()
    {
        
    }

    void Update()
    {
        if(currentState == States.ViewLevel)    //-----------------ViewLevel
        {
            camAnim.SetBool("ViewLevel", true);

            //State Changed by CameraController.cs --> via Animation's Event
        }
        else if(currentState == States.Aim)     //-----------------Aim
        {
            if(isAiming)
            {
                camAnim.SetBool("isAiming", true);

                target.ObjectToMouse();

                foreach(Camera cam in cameras)
                {
                    if(cam.name == "TopView")
                    {
                        cam.enabled = true;
                    }
                    else
                        cam.enabled = false;
                }
            }
            else
            {
                camAnim.SetBool("isAiming", false);

                foreach(Camera cam in cameras)
                {
                    if(cam.name == "Main Camera")
                    {
                        cam.enabled = true;
                    }
                    else
                        cam.enabled = false;
                }
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                currentState = States.Power;    //Change State to Power
            }
        }
        else if(currentState == States.Power)   //-----------------Power
        {
            launcher.calculateMiss = true;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                launcher.calculateMiss = false;
                currentState = States.Shoot;

                currentTimer = timerBeforeShoot;
            }
        }
        else if(currentState == States.Shoot)   //-----------------Shoot
        {

            if(currentTimer > 0)
            {
                currentTimer -= Time.deltaTime;
            }

            if(currentTimer <= 0)
            {
                launcher.gameObject.GetComponent<Animator>().SetTrigger("Launch");  //Execute Launch() --> via Animation Event

                currentState = States.BallFly;
            }
        }
        else if(currentState == States.BallFly)   //-----------------Ball Fly
        {
            if(ball.transform.position.y >= hToActivateCatCam)
            {
                foreach(Camera cam in cameras)
                {
                    if(cam.name == "CatView")
                    {
                        cam.enabled = true;
                    }
                    else
                        cam.enabled = false;
                }
            }

            if(ball.GetComponent<Ball>().slowing)
            {
                currentState = States.BallStop;

                currentTimer = timerBeforeCamZoom;
            }
        }
        else if(currentState == States.BallStop)
        {
            if(currentTimer > 0)
            {
                currentTimer -= Time.deltaTime;
            }

            if(currentTimer <= 0)
            {
                cameras[2].GetComponent<CatViewCameraController>().Zoom = true;
            }            
        }
    }

    public void ChangeState(States nextState)
    {
        currentState = nextState;
    }
}
