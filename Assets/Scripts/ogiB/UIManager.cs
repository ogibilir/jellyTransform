using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static bool isLike;
    public int likeNumber;
    public TextMeshProUGUI text;
    private Scene _currentScene;
    public int counter;
    void Start()
    {
        counter = Random.Range(10000, 30000);
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
        text.text = likeNumber.ToString();
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(_currentScene.name);
    }
    public void ContinueButton()
    {
        if (_currentScene != SceneManager.GetSceneAt(SceneManager.sceneCount - 1))
        {

        }
    }
}
