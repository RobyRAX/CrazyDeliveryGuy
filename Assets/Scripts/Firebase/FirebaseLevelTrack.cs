using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;

public class FirebaseLevelTrack : MonoBehaviour
{
    public LevelManager levelMan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LogEventLevel()
    {
        FirebaseAnalytics.LogEvent("player_level", "level", levelMan.level.ToString());
    }
}
