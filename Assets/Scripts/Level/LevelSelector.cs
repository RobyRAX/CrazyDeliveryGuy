using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    LevelManager levelMan;

    void Start()
    {
        levelMan = GameObject.FindObjectOfType<LevelManager>();
    }

    public void SelectLevel()
    {   
        levelMan.level = Level(gameObject);

        levelMan.UpdateData();
        levelMan.UpdateEnvironment();
        levelMan.UpdateUI();
    }

    public Vector2 Level(GameObject obj)
    {
        string name = obj.name;
        char[] nameArray;
        int level, stage;

        nameArray = name.ToCharArray();
        level = (int)Char.GetNumericValue(nameArray[0]);
        stage = (int)Char.GetNumericValue(nameArray[2]);

        return new Vector2(level, stage);
    }
}
