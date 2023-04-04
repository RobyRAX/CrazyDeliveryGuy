using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            GameObject ballClone;
            ballClone = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            Destroy(ballClone, 5f);
        }
    }
}
