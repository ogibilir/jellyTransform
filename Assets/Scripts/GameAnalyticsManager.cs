using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;


public class GameAnalyticsManager : MonoBehaviour
{
   [SerializeField] private Scene_Container_Class scene_container;


    private void Awake()
    {
        GameAnalytics.Initialize();

    }
    // Start is called before the first frame update
    void Start()
    {
        
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start,scene_container.Last_Scene_Name[0], "Stage_01", "Level_Progress"); // without score

    }

    
}
