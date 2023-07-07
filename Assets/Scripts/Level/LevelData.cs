using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public Vector2 level;
        public bool isUnlocked = false;
        public int capacityGoal;
        public int maxStock;
        public Vector2 minMaxBallShot = new Vector2(3, 7);
        public Vector2 ballShotDelay = new Vector2(0.6f, 0.9f);
    }

    public List<Data> datas = new List<Data>();
}
