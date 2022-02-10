using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Counter_Text : MonoBehaviour
{
    private Text Level_Counter;
    void Start()
    {
        Level_Counter = GetComponent<Text>();
        Level_Counter.text = SceneManager.GetActiveScene().name;
    }
}
