using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharMovement : MonoBehaviour
{
    public Slider slideMovement;
    Camera cam;

    [SerializeField] Vector3 defPos;

    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = defPos + new Vector3(slideMovement.value, 0, 0); 

        if(Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity))
            {
                if(hit.collider.name == "RayCol")
                {
                    transform.position = new Vector3(hit.point.x + defPos.x, transform.position.y, transform.position.z);
                }
            }
        }
    }

}
