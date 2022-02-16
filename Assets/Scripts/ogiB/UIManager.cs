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
    public TextMeshProUGUI text2;
    private Scene _currentScene;
    public int counter;
    public int counter2;

    public Touch _touch;

    [SerializeField] GameObject _MenuPanel;
    [SerializeField] GameObject _player;

    public void Awake()
    {

    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("isStart"))
        {
            PlayerPrefs.SetInt("isStart", 0);
        }
        if (PlayerPrefs.GetInt("isStart") == 0)
        {
            _MenuPanel.SetActive(true);
            _player.GetComponent<PlayerMovement>().enabled = false;
            PlayerPrefs.SetInt("isStart", 1);
        }
        counter = Random.Range(10000, 30000);
        counter2 = Random.Range(10, 20);
        isLike = false;
        likeNumber = 0;
        _currentScene = SceneManager.GetActiveScene();
    }
    void FixedUpdate()
    {
        if(Input.touchCount > 0)
        {
            _MenuPanel.SetActive(false);
            _player.GetComponent<PlayerMovement>().enabled = true;
        }
        if (isLike && likeNumber < counter)
        {
            likeNumber += 133;
        }
        if(isSad && likeNumber < counter2)
        {
            likeNumber++;
        }
        text.text = likeNumber.ToString();
        text2.text = likeNumber.ToString();
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
        else if(_currentScene.buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
    }
}
