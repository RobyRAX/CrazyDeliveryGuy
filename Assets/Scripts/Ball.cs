using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // [SerializeField] float bounceResistance;
    // [SerializeField] float sleepAdd;
    // [SerializeField] float sleepLimit;
    private Rigidbody rb;
    public Rigidbody RB
    {
        get{return rb;}
        set{rb = value;}
    }

    [SerializeField] float velLimit;
    [SerializeField] float slowDown;
    public bool slowing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // void OnCollisionEnter(Collision col)
    // {
    //     if(col.collider.tag == "Ground")
    //     {
    //         rb.velocity /= bounceResistance;
    //         rb.sleepThreshold += sleepAdd;

    //         Debug.Log("Sleep Threshold --> " + rb.sleepThreshold);
    //     }
    // }

    void FixedUpdate()
    {
        if(rb.velocity.magnitude < velLimit)
        {
            rb.velocity = rb.velocity * slowDown;
            slowing = true;
        }

        if(rb.velocity.magnitude < 0.25)
        {
            rb.Sleep();
            slowing = false;
        }        
    }
}
