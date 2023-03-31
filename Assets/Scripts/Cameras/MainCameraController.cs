using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public Transform ball;
    public GameManager gm;

    void LateUpdate()
    {
        
    }

    public void FollowBall()
    {
        
    }

    public void ViewAnimationComplete(GameManager.States nextState)
    {
        gm.ChangeState(nextState);
    }
}
