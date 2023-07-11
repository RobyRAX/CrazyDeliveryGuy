using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VehicleSkinDatabase : ScriptableObject
{
    public VehicleSkin[] vehicleSkins;

    public int SkinCount
    {
        get
        {
            return vehicleSkins.Length;
        }
    }

    public VehicleSkin GetVehicleSkin(int index)
    {
        return vehicleSkins[index];
    }
}
