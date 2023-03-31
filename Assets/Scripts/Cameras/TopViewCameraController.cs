using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCameraController : MonoBehaviour
{
    public GameObject target;

    private Camera cam;
    private Vector3 previousPosition;

    [SerializeField] private float swipeSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        cam = transform.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!target.GetComponent<TargetController>().TargetMoved)
        {
            SwipeCamera();
        }
    }

    public void SwipeCamera()
    {
        if(Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 dir = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            //Debug.Log(dir);

            this.transform.Translate(Vector3.right * dir.y * 180 * swipeSensitivity * Time.deltaTime);
            this.transform.Translate(Vector3.back * dir.x * 180 * swipeSensitivity * Time.deltaTime);

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
