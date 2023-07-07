using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class VehicleSkin
{
    public string name;
    public GameObject skinObj;
    public Sprite skinImg;
    public Vector3 colliderPos;
    public bool unlocked = false;
}
