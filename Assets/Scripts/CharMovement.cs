using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharMovement : MonoBehaviour
{
    public Slider slideMovement;

    [SerializeField] Vector3 defPos;

    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = defPos + new Vector3(slideMovement.value, 0, 0); 
    }
}
