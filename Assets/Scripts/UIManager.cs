using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class UIManager : MonoBehaviour
{

    public Text Last_Level;
    public TextMeshProUGUI professionTypeText;
    public UIManager _UIManager;
    public TextMeshProUGUI coinNumMain;
    public TextMeshProUGUI coinNumPlaying;
    [HideInInspector]
    public TextMeshProUGUI coinNumStore;
    public TextMeshProUGUI Current_Level_coin;

    public GameObject coins;
    public GameObject items;

    public GameObject playingCanvas;
    public GameObject startMenuCanvas;
    public GameObject levelCompleteCanvas;
    public GameObject levelFailCanvas;

    public GameObject levelScreen;
    public player _player;
    public player male_player, female_player, idle_player;

    private Scene_Container SceneContainer;

    public GameObject touchPanel;
    private GameObject Constructer;
    public Camera Main_Camera;

    public Button Choose_Male_Button;
    public Button Choose_Female_Button;

    private bool isGameStarted;
    private bool isAdWatched = false;

    private bool Is_Ad_Played = false;


    public Scene_Container_Class Container_SO;
    private void Awake()
    {
        //Container_SO = Resources.Load<Scene_Container_Class>("Sources/Scene_Container_SO.asset");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != Get_Last_Level())
        {
            SceneManager.LoadScene(Get_Last_Level());
        }
        


        levelFailCanvas.SetActive(false);

        isGameStarted = false;
        startMenuCanvas.SetActive(true);
        playingCanvas.SetActive(false);

        if (female_player.Rigidbody)
        {
            female_player.Rigidbody.useGravity = false;
        }
        if (male_player.Rigidbody)
        {
            male_player.Rigidbody.useGravity = false;
        }


        SceneContainer = FindObjectOfType<Scene_Container>();
        Constructer = GameObject.FindGameObjectWithTag("const");
        if (Constructer.activeSelf)
        {
            Constructer.SetActive(false);
        }
        Choose_Male_Button.onClick.AddListener(On_Click_Male_Button);
        Choose_Female_Button.onClick.AddListener(On_Click_Female_Button);

    }

    // Update is called once per frame
    void Update()
    {

        if (_player != null)
            professionTypeText.text = _player.ProffesionTypeText;
        coinNumMain.text = PlayerPrefs.GetInt("coin").ToString();
        coinNumPlaying.text = PlayerPrefs.GetInt("coin").ToString();

        if (_player != null)
        {
            if ((_player.At_End)||(_player.harmfulObject))
            {
                if (_player.openWinScreen)
                {
                    Current_Level_coin.text = SceneContainer.Collected_Coin_amount.ToString();
                    levelCompleteCanvas.SetActive(true);

                    _player.Percent_TEXT.enabled = false;
                }
                if (_player.openFailScreen)
                {
                    levelFailCanvas.SetActive(true);

                    _player.Percent_TEXT.enabled = false;
                }
            }
        }

        if (isGameStarted)
        {
            _player.animator.SetBool("walking", isGameStarted);
            items.SetActive(true);
            coins.SetActive(true);


            touchPanel.SetActive(true);
            playingCanvas.SetActive(true);
        }
        else
        {

            //stop player Movement

            touchPanel.SetActive(false);
            items.SetActive(false);
            coins.SetActive(false);
            playingCanvas.SetActive(false);


            //levelCompleteCanvas.SetActive((false));
            startMenuCanvas.SetActive((true));
        }

    }


    public void TapToStart()
    {

        Constructer.SetActive(true);



    }

    public void Choose()
    {
        isGameStarted = true;
    }

    public void OpenLevelScreen()
    {
        //Setting First Screen Button interactables

        for (int level_counter = 1; level_counter < 5; level_counter++)
        {

            if (level_counter < Container_SO.max_level[Container_SO.max_level.Count-1] + 1)
            {
                Button level_button = GameObject.Find(level_counter.ToString()).GetComponentInChildren<Button>();
                level_button.interactable = true;
            }
        }


    }

    public void Next_Level_Button()
    {
        // Yeni Level eklendiginde, her screen'de 4 tane level olacagini goze alarak her  screen_counter artisinda 4 tane button'un interactable'ini degistirebilirsin


        GameObject parent = GameObject.Find("Next_Button").transform.parent.gameObject;
        int screen_counter = Convert.ToInt32(new string(parent.name[parent.name.Length - 1], 1));




        if (screen_counter != 0)
        {
            if (screen_counter == 1)  // Redunant exceptionS
            {
                for (int level_counter = 1; level_counter < 5; level_counter++)
                {
                    if (level_counter < Container_SO.max_level[Container_SO.max_level.Count-1] + 1)
                    {
                        Debug.Log("level_counter :"+level_counter);
                        Button level_button = GameObject.Find(level_counter.ToString()).GetComponentInChildren<Button>();
                        level_button.interactable = true;
                    }
                }
            }
            else if (screen_counter == 2)
            {
                for (int level_counter = 5; level_counter < 9; level_counter++)
                {
                    if (level_counter < Container_SO.max_level[Container_SO.max_level.Count-1] + 1)
                    {
                        Button level_button = GameObject.Find(level_counter.ToString()).GetComponentInChildren<Button>();
                        level_button.interactable = true;
                    }
                }
            }
            else if (screen_counter == 3)
            {
                for (int level_counter = 9; level_counter < 13; level_counter++)
                {
                    if (level_counter < Container_SO.max_level[Container_SO.max_level.Count-1] + 1)
                    {
                        Button level_button = GameObject.Find(level_counter.ToString()).GetComponentInChildren<Button>();
                        level_button.interactable = true;
                    }
                }
            }
            else if (screen_counter == 4)
            {
                for (int level_counter = 9; level_counter < 16; level_counter++)
                {
                    if (level_counter < Container_SO.max_level[Container_SO.max_level.Count-1] + 1)
                    {
                        Button level_button = GameObject.Find(level_counter.ToString()).GetComponentInChildren<Button>();
                        level_button.interactable = true;
                    }
                }
            }
        }


    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void LoadNextScene()
    {
        // No thanks
        isAdWatched = true;
        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        string next_Scene_name = SceneManager.GetActiveScene().name;
        string new_String = "";

        for (int i = 0; i < next_Scene_name.Length - 1; i++)
        {
            new_String += next_Scene_name[i];
        }

        int new_int;
        int.TryParse("" + next_Scene_name[next_Scene_name.Length - 1], out new_int);
        new_String += (new_int + 1).ToString();   

            
            
        Container_SO.Set_Last_Scene_Name(new_String);

        if (SceneManager.GetActiveScene().buildIndex != 14)
        {
            
            if (SceneManager.GetActiveScene().buildIndex + 1 > Container_SO.max_level[Container_SO.max_level.Count-1])
            {
                Container_SO.Set_max_level(SceneManager.GetActiveScene().buildIndex + 1);
                
                //PlayerPrefs.SetInt("max_reachable_level", SceneManager.GetActiveScene().buildIndex + 1);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else  //last level state
        {
            SceneManager.LoadScene(0);
        }
        isAdWatched = false;

        
    }

    String Get_Last_Level()
    {
        return Container_SO.Last_Scene_Name[Container_SO.Last_Scene_Name.Count-1];
    }
    


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void RemoveAds()

    {

    }
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }



    //public void Tenx_Button_Func()
    //{
        // 10x reklam konulacak
        // reward reklam izle

        //GoogleAds.Instance.CallAds();
       // isAdWatched = true;
        //reklam izlendiyse 
        //if (isAdWatched == true)
        //{
        //    {
    //            int coin_amount = PlayerPrefs.GetInt("coin") + SceneContainer.Collected_Coin_amount * 10;
    //            PlayerPrefs.SetInt("coin", coin_amount);

    //            int new_max_level = int.Parse(new string(SceneManager.GetActiveScene().name[SceneManager.GetActiveScene().name.Length - 1], 1)) + 1;
    //            _player.Write_Json(new_max_level);

    //            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //            isAdWatched = false;
    //        }
    //    }

    //}
    public void On_Click_Male_Button()
    {
        Constructer.SetActive(false);
        //_player = male_player;
        Set_Player(male_player);
        _player.gameObject.SetActive(true);
        startMenuCanvas.gameObject.SetActive(false);
        if (GameObject.Find("Constructer"))
        {
            GameObject.Find("Constructer").SetActive(false);
        }
    }
    public void On_Click_Female_Button()
    {
        Constructer.SetActive(false);
        Set_Player(female_player);
        _player.gameObject.SetActive(true);
        startMenuCanvas.gameObject.SetActive(false);
        if (GameObject.Find("Constructer"))
        {
            GameObject.Find("Constructer").SetActive(false);
        }
    }

    public void Set_Player(player new_player)
    {
        _player = new_player;
    }





}
