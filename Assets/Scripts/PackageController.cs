using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageController : MonoBehaviour
{
    [SerializeField] int capacity;
    public int Capacity
    {
        get{return capacity;}
        set{capacity = value;}
    }

    [SerializeField] private bool hitable;
    public bool Hitable
    {
        get{return hitable;}
        set{hitable = value;}
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "HitZone")
        {
            hitable = true;
        }
    }
}
