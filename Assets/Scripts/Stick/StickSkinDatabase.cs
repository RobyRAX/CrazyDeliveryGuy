using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StickSkinDatabase : ScriptableObject
{
    public StickSkin[] stickSkins;

    public int SkinCount
    {
        get
        {
            return stickSkins.Length;
        }
    }

    public StickSkin GetStickSkin(int index)
    {
        return stickSkins[index];
    }
}
