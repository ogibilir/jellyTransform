using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static bool isLike;
    public static bool isSad;
    public int likeNumber;
    public TextMeshProUGUI text;
    private Scene _currentScene;
    public int counter;
    public int counter2;
    void Start()
    {
        counter = Random.Range(10000, 30000);
        counter2 = Random.Range(10, 20);
        isLike = false;
        likeNumber = 0;
        _currentScene = SceneManager.GetActiveScene();
    }
    void FixedUpdate()
    {
        if (isLike && likeNumber < counter)
        {
            likeNumber += 133;
        }
        if(isSad && likeNumber < counter2)
        {
            likeNumber++;
        }
        text.text = likeNumber.ToString();
        Debug.Log(SceneManager.sceneCountInBuildSettings);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(_currentScene.name);
    }
    public void ContinueButton()
    {
        Debug.Log("Olduuuu");
        if (_currentScene.buildIndex != SceneManager.sceneCountInBuildSettings-1)
        {
            SceneManager.LoadScene(_currentScene.buildIndex + 1);
        }
    }
}
