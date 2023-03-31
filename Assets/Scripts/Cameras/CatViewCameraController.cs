using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatViewCameraController : MonoBehaviour
{
    public Transform target;
    public Transform ball;

    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 offsetZoom;
    [SerializeField] private bool zoom = false;
    public bool Zoom
    {
        get{return zoom;}
        set{zoom = value;}
    } 

    [SerializeField] private float zoomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if(!zoom)
        {
            TrackBall();
        }
        else
        {
            TrackBallAndZoom();
        }
    }

    void TrackBall()
    {
        transform.position = target.position + offset;

        transform.LookAt(ball);
    }

    void TrackBallAndZoom()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offsetZoom, zoomSpeed * Time.deltaTime);

        transform.LookAt(ball);
    }
}
