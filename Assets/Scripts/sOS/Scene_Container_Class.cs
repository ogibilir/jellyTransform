using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Scene_Containter", menuName = "Inventory/List", order = 1)]
public class Scene_Container_Class : ScriptableObject
{
    public List<string> Last_Scene_Name;
    public List<int> max_level ;

    private void Awake()
    {
        if (Last_Scene_Name.Count == 0)
        {
            Last_Scene_Name.Add("Level1");
        }

        if (max_level.Count == 0)
        {
            max_level.Add(1);
        }
    }

    public void Set_Last_Scene_Name(string new_name)
    {

        Last_Scene_Name.Add(new_name);
        
        
    }
    
    public void Set_max_level(int new_max_level)
    {
        if (!max_level.Contains(new_max_level))
        {
            max_level.Add(new_max_level);
        }
    }
}
