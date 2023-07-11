using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharSkinDatabase : ScriptableObject
{
    public CharSkin[] charSkins;

    public int SkinCount
    {
        get
        {
            return charSkins.Length;
        }
    }

    public CharSkin GetCharSkin(int index)
    {
        return charSkins[index];
    }
}
