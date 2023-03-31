using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Camera cam;
    public BallLauncher launcher;

    [SerializeField] private Vector3 previousPos;
    [SerializeField] private bool targetMoved;
    [SerializeField] private bool targetClicked;
    public bool TargetMoved
    {
        get {return targetMoved;}
        set {targetMoved = value;}
    }

    void Update()
    {
        if(launcher.distance <= launcher.MaxDistance)
        {
            previousPos = transform.position;
        }

        if(Input.GetMouseButtonUp(0))
        {
            targetMoved = false;
            targetClicked = false;
        }
    }

    void LateUpdate()
    {
        if(launcher.distance >= launcher.MaxDistance)
        {
            transform.position = previousPos;
        }
    }

    public void ObjectToMouse()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray rayC = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(rayC, out RaycastHit hitC, float.PositiveInfinity))
            {
                if(hitC.collider.name == "ClickRadius")
                {
                    targetClicked = true;
                }
            }
        }

        if(Input.GetMouseButton(0) && targetClicked)
        {
            Ray rayA = cam.ScreenPointToRay(Input.mousePosition);
            Ray rayB = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(rayA, out RaycastHit hitA, float.PositiveInfinity))
            {
                if(hitA.collider.name == "ClickRadius")
                {
                    if(Physics.Raycast(rayB, out RaycastHit hitB, float.PositiveInfinity, 3))
                    {
                        if(hitB.collider.tag == "Ground")
                        {
                            transform.position = Vector3.Lerp(transform.position, hitB.point, 30 * Time.deltaTime);
                            targetMoved = true;
                        }                    
                    }
                }                    
            }
        }
    }
}
