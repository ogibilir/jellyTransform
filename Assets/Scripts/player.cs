using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class player : MonoBehaviour
{
    public SoundManager soundManager;
    player new_player = null;
    public Scene_Container SceneContainer;
    public UIManager UIManager;
    [HideInInspector]
    public string ProffesionTypeText;
    [HideInInspector] public bool openWinScreen = true, openFailScreen = false;
    [HideInInspector]
    public bool Is_First_Player = true;
    public player[] All_Characters;
    [HideInInspector] public bool At_End = false;
    private float end_timer = 0f;
    public Image Bar, Empty_Percent_Image, Fill_Percent_Image;
    public TextMeshProUGUI Percent_TEXT;
    public float End_Duration = 3f;

    //Particles
    [SerializeField] private GameObject doorEffectTrue;
    [SerializeField] private GameObject doorEffectFalse;

    [SerializeField] private GameObject coinEffect;
    [SerializeField] private GameObject changePlayerEffect;
    [SerializeField] private GameObject ObjectCollectEffectTrue;
    [SerializeField] private GameObject ObjectCollectEffectFalse;



    //Animation
    public Animator animator;
    private bool anim_finish_happy = false;
    private bool anim_walk_on = false;
    private bool anim_true_door = false;
    private bool anim_false_door = false;
    private bool barFilled = false;



    public Transform Player_Transform;
    [HideInInspector] public GameObject Camera;
    [HideInInspector] public Rigidbody Rigidbody;
    [HideInInspector] public BoxCollider Collider;
    [HideInInspector] public Transform Current_Surface_Transform;

    public Spawn_Door SpawnDoor;
    public Object_Spawner ObjectSpawner_Ref;
    public Touch_Mechs Touch_Ref;

    [HideInInspector] public float last_x_pos;
    [HideInInspector] public float last_z_pos;

    [HideInInspector] public int direction = 1;
    public int Profession_Type;


    public int max_score;
    public int Score = 0;
    public int Collected_Coin;

    private float timer = 0f;
    [HideInInspector] public bool first_door = true;
    public bool On_Ground = false;
    [HideInInspector] public bool first_time_ground = true;
    public float Rotation_Duration = 3f;
    public float speed = 22f;
    private string ground_name = null;

    private Coin[] Coins_array;
    private bool isAdWatched = false;

    private Transform Artist_Bag, Artist_Hat, Police_Jop, Police_Hat, Doctor_Vaccine, Doctor_Steteschop, Teacher_Book, Teacher_Ruler;

    [HideInInspector] public bool harmfulObject = false;

    public GameObject _endPlatform;
    [SerializeField] private List<GameObject> _stops;
    public int whereShoudIGo;
    private float roadLength;

    private void Awake()
    {


        Transform[] All_Game_Objects = this.gameObject.GetComponentsInChildren<Transform>();


        for (int i = 0; i < All_Game_Objects.Length; i++)
        {
            if (All_Game_Objects[i].name == "Bag")
            {
                Artist_Bag = All_Game_Objects[i];
                Artist_Bag.gameObject.SetActive(false);
            }
            else if (All_Game_Objects[i].name == "Ruler")
            {
                Teacher_Ruler = All_Game_Objects[i];
                Teacher_Ruler.gameObject.SetActive(false);
            }
            else if (All_Game_Objects[i].name == "Hat")
            {
                Artist_Hat = All_Game_Objects[i];
                Artist_Hat.gameObject.SetActive(false);
            }
            else if (All_Game_Objects[i].name == "Police_Cap")
            {
                Police_Hat = All_Game_Objects[i];
                Police_Hat.gameObject.SetActive(false);
            }
            else if (All_Game_Objects[i].name == "Steteschop")
            {
                Doctor_Steteschop = All_Game_Objects[i];
                Doctor_Steteschop.gameObject.SetActive(false);
            }
            else if (All_Game_Objects[i].name == "vaccine")
            {
                Doctor_Vaccine = All_Game_Objects[i];
                Doctor_Vaccine.gameObject.SetActive(false);
            }
            else if (All_Game_Objects[i].name == "Jop")
            {
                Police_Jop = All_Game_Objects[i];
                Police_Jop.gameObject.SetActive(false);
            }
            else if (All_Game_Objects[i].name == "Notebook")
            {
                Teacher_Book = All_Game_Objects[i];
                Teacher_Book.gameObject.SetActive(false);
            }
        }

    }
    void Start()
    {

        
        _endPlatform = GameObject.FindGameObjectWithTag("EndPlatform");

        foreach (Transform child in _endPlatform.transform)
        {
            if (child.tag == "Stops")
            {
                _stops.Add(child.gameObject);
            }
        }

        animator.SetBool("sad", false);

        if (Touch_Ref == null)
        {
            Touch_Ref = GameObject.FindWithTag("SwipeInput").GetComponent<Touch_Mechs>();
        }

        this.gameObject.SetActive(true);

        if (Is_First_Player)
        {
            Camera = GameObject.Find("First_Camera").gameObject;
            Destroy(GameObject.Find("First_Camera").gameObject);

            Coins_array = FindObjectsOfType<Coin>();
            for (int coin_counter = 0; coin_counter < Coins_array.Length; coin_counter++)
            {
                Coins_array[coin_counter]._MeshRenderer.enabled = false;
            }
        }

        Collected_Coin = PlayerPrefs.GetInt("coin");
        SceneContainer = FindObjectOfType<Scene_Container>();


        animator = gameObject.GetComponent<Animator>();
        Set_Camera_Defaults();
        Rigidbody = gameObject.GetComponent<Rigidbody>();

        Collider = GetComponent<BoxCollider>();

        if (Is_First_Player)
        {
            SpawnDoor.Spawn_First_Doors();
        }
        UIManager = FindObjectOfType<UIManager>();
    }


    private void Update()
    {
        if (Profession_Type == 0)
        {
            ProffesionTypeText = " ";
        }
        #region ProfessionTypeUI
        if (Profession_Type == 1)
        {
            ProffesionTypeText = "Teacher";
        }
        else if (Profession_Type == 2)
        {
            ProffesionTypeText = "Doctor";
        }
        else if (Profession_Type == 3)
        {
            ProffesionTypeText = "Police";
        }
        else if (Profession_Type == 4)
        {
            ProffesionTypeText = "Artist";
        }
        #endregion

        if (On_Ground)
        {
            check_direction(direction);
            if (first_time_ground)
            {
                Rigidbody = gameObject.GetComponent<Rigidbody>();
                Rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
                first_time_ground = false;
            }

            //Rotation timer
            if (timer > 0)
            {
                if (timer < Rotation_Duration)
                {
                    Move_Forward();
                    Swipe_Actions();
                    timer += Time.deltaTime;

                }
                else
                {
                    timer = 0f;

                    Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
                    Rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
                }
            }
            else
            {
                Move_Forward();
                Swipe_Actions();
            }
        }
        //filling end image
    }

    private void FixedUpdate()
    {
        //Bar.fillamount states
        if (At_End)
        {
            if (Percent_TEXT)
                Percent_TEXT.enabled = true;
            if (end_timer > 0)
            {
                if (end_timer < End_Duration)
                {
                    //170 frame per 3 seconds
                    Fill_Percent_Image.fillAmount += (((float)Score / (float)max_score) / (float)170);
                    Percent_TEXT.text = "%" + ((int)(Fill_Percent_Image.fillAmount * 100)).ToString();
                    end_timer += Time.deltaTime;

                }
                else
                {
                    end_timer = 0f;

                    At_End = false;
                    if (!isAdWatched)
                    {
                        //GoogleAds.Instance.CallAds();
                        isAdWatched = true;
                    }
                }
            }
            else
            {
                Finish();

            }
        }

        //Profession object spawns
        if (Is_First_Player)
        {
            if (Bar.fillAmount > 0.2f)
            {
                if (Profession_Type == 1)
                {
                    //teacher
                    if (!Teacher_Ruler.gameObject.activeSelf)
                    {
                        Teacher_Ruler.gameObject.SetActive(true);
                    }
                }
                else if (Profession_Type == 2)
                {
                    //Doctor
                    if (!Doctor_Vaccine.gameObject.activeSelf)
                    {
                        Doctor_Vaccine.gameObject.SetActive(true);
                    }

                }
                else if (Profession_Type == 3)
                {
                    //Police
                    if (!Police_Hat.gameObject.activeSelf)
                    {
                        Debug.Log("Profession Type :" + Profession_Type);
                        Police_Hat.gameObject.SetActive(true);
                    }
                }
                else if (Profession_Type == 4)
                {
                    //Artist
                    if (!Artist_Hat.gameObject.activeSelf)
                    {
                        Artist_Hat.gameObject.SetActive(true);
                    }
                }
            }

            if (Bar.fillAmount > 0.4f)
            {
                if (Profession_Type == 1)
                {
                    //teacher
                    if (!Teacher_Book.gameObject.activeSelf)
                    {
                        Teacher_Book.gameObject.SetActive(true);
                    }
                }
                else if (Profession_Type == 2)
                {
                    //Doctor
                    if (!Doctor_Steteschop.gameObject.activeSelf)
                    {
                        Doctor_Steteschop.gameObject.SetActive(true);
                    }
                }
                else if (Profession_Type == 3)
                {
                    //Police
                    if (!Police_Jop.gameObject.activeSelf)
                    {
                        Debug.Log("Profession Type :" + Profession_Type);
                        Police_Jop.gameObject.SetActive(true);
                    }

                }
                else if (Profession_Type == 4)
                {
                    //Artist
                    if (!Artist_Bag.gameObject.activeSelf)
                    {
                        Artist_Bag.gameObject.SetActive(true);
                    }
                }
            }
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        //
        GameObject collided_object = collision.gameObject;
        if (collided_object.CompareTag("surface"))
        {
            timer += Time.deltaTime;
            Current_Surface_Transform = collision.gameObject.transform;
            if (!On_Ground)
            {
                On_Ground = true;
                ground_name = Current_Surface_Transform.name;
                //Check_Range(Current_Surface_Transform, Player_Transform.position);
            }
        }
    }


    private void OnCollisionStay(Collision other)
    {
        if (ground_name != other.gameObject.name)
        {
            if (other.gameObject.CompareTag("surface"))
            {
                //Check_Range(Current_Surface_Transform, Player_Transform.position);
                ground_name = other.gameObject.name;
                Current_Surface_Transform = other.gameObject.transform;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        // First triggered object should be profession which will choose.
        GameObject collided_object = other.gameObject;

        if (first_door)
        {
            if (collided_object.CompareTag("Door_Type_1") || collided_object.CompareTag("Door_Type_2") ||
                collided_object.CompareTag("Door_Type_3") || collided_object.CompareTag("Door_Type_4") ||
                collided_object.CompareTag("Door_Type_5"))
            {
                if(collided_object.GetComponent<MeshRenderer>())
                collided_object.GetComponent<MeshRenderer>().enabled = false;
                // Setting Profession_Type, Doors and Objects at the Initiate State
                if (Is_First_Player)
                {
                    int.TryParse(collided_object.tag[collided_object.tag.Length - 1].ToString(), out Profession_Type);
                    SpawnDoor.Spawn_Other_Doors(Profession_Type);
                    ObjectSpawner_Ref.Spawn_Objects();
                    set_bar_percent();
                    Destroy(collided_object);
                    first_door = false;
                    anim_walk_on = true;
                    On_Ground = true;

                    for (int coin_counter = 0; coin_counter < Coins_array.Length; coin_counter++)
                    {
                        Coins_array[coin_counter]._MeshRenderer.enabled = true;
                    }
                }
            }
        }


        else if (collided_object.CompareTag("Item_Type_1") || collided_object.CompareTag("Item_Type_2") || collided_object.CompareTag("Item_Type_3") || collided_object.CompareTag("Item_Type_4") || collided_object.CompareTag("Item_Type_5"))
        {
            Destroy(collided_object);
            {
                if (int.Parse(collided_object.tag[collided_object.tag.Length - 1].ToString()) == Profession_Type)
                {
                    Instantiate(ObjectCollectEffectTrue, collided_object.transform.position, collided_object.transform.rotation);
                    Score += 1;
                    SceneContainer.Collected_True_object++;
                }
                else
                {
                    Instantiate(ObjectCollectEffectFalse, collided_object.transform.position, collided_object.transform.rotation);

                    if (Score > 1)
                    {
                        Score -= 1;
                    }
                    SceneContainer.Collected_Wrong_object++;
                }
                set_bar_percent();
            }
        }
        else if (collided_object.CompareTag("Turn_Right_Collider"))
        {
            Destroy(collided_object);
            timer += Time.deltaTime;
            Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ;
            Turn_Right();
            Touch_Ref.ekle = 0f;

            On_Ground = true;
        }
        else if (collided_object.CompareTag("Turn_Left_Collider"))
        {
            Destroy(collided_object);
            timer += Time.deltaTime;
            Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ;
            Turn_Left();
            Touch_Ref.ekle = 0f;

            On_Ground = true;
        }
        else if (collided_object.CompareTag("Door_Type_1"))
        {
            if (int.Parse(collided_object.tag[collided_object.tag.Length - 1].ToString()) == Profession_Type)
            {


                Instantiate(doorEffectTrue, Player_Transform.position + new Vector3(0, 2, 0), Player_Transform.rotation);

                Score += 2;
                set_bar_percent();
            }
            else
            {
                Instantiate(doorEffectFalse, Player_Transform.position + new Vector3(0, 2, 0), Player_Transform.rotation);

            }
            Destroy(collided_object);
        }
        else if (collided_object.CompareTag("Door_Type_2"))
        {
            if (int.Parse(collided_object.tag[collided_object.tag.Length - 1].ToString()) == Profession_Type)
            {

                Instantiate(doorEffectTrue, Player_Transform.position + new Vector3(0, 2, 0), Player_Transform.rotation);

                Score += 2;
                set_bar_percent();
            }
            else
            {

                Instantiate(doorEffectFalse, Player_Transform.position + new Vector3(0, 2, 0), Player_Transform.rotation);
            }
            Destroy(collided_object);
        }
        else if (collided_object.CompareTag("Door_Type_3"))
        {
            if (int.Parse(collided_object.tag[collided_object.tag.Length - 1].ToString()) == Profession_Type)
            {

                Instantiate(doorEffectTrue, Player_Transform.position + new Vector3(0, 2, 0), Player_Transform.rotation);

                Score += 2;
                set_bar_percent();
            }
            else
            {

                Instantiate(doorEffectFalse, Player_Transform.position + new Vector3(0, 2, 0), Player_Transform.rotation);

            }
            Destroy(collided_object);
        }
        else if (collided_object.CompareTag("Door_Type_4"))
        {
            if (int.Parse(collided_object.tag[collided_object.tag.Length - 1].ToString()) == Profession_Type)
            {

                Instantiate(doorEffectTrue, Player_Transform.position + new Vector3(0, 2, 0), Player_Transform.rotation);

                Score += 2;
                set_bar_percent();

            }
            else
            {
                Instantiate(doorEffectFalse, Player_Transform.position + new Vector3(0, 2, 0), Player_Transform.rotation);

            }
            Destroy(collided_object);
        }
        else if (collided_object.CompareTag("Door_Type_5"))
        {
            if (int.Parse(collided_object.tag[collided_object.tag.Length - 1].ToString()) == Profession_Type)
            {

                Instantiate(doorEffectTrue, Player_Transform.position + new Vector3(0, 2, 0), Player_Transform.rotation);

                Score += 2;
                set_bar_percent();

            }
            else
            {

                Instantiate(doorEffectFalse, Player_Transform.position + new Vector3(0, 2, 0), Player_Transform.rotation);

            }
            Destroy(collided_object);
        }
        else if (collided_object.CompareTag("Coin"))
        {
            Instantiate(coinEffect, collided_object.transform.position + new Vector3(0, 2, 0), Player_Transform.rotation);
            soundManager.CoinSoundActive();


            Destroy(collided_object);
            Collected_Coin++;
            SceneContainer.Collected_Coin_amount++;
        }

        else if (collided_object.CompareTag("Stops"))
        {
            Collected_Coin *= (whereShoudIGo + 1);
            SceneContainer.Collected_Coin_amount *= (whereShoudIGo + 1);

            Finish();
        }

        else if (collided_object.CompareTag("Finish"))
        {
            Destroy(collided_object);

            if (Bar.fillAmount >= 0.5f)
            {
                roadLength = Bar.fillAmount * 100;
                Debug.Log(roadLength);
                if (roadLength <= 55)
                {
                    whereShoudIGo = 0;
                    FinalDirection();
                }
                else if (roadLength <= 60)
                {
                    whereShoudIGo = 1;
                    FinalDirection();
                }
                else if (roadLength <= 65)
                {
                    whereShoudIGo = 2;
                    FinalDirection();
                }
                else if (roadLength <= 70)
                {
                    whereShoudIGo = 3;
                    FinalDirection();
                }
                else if (roadLength <= 75)
                {
                    whereShoudIGo = 4;
                    FinalDirection();
                }
                else if (roadLength <= 80)
                {
                    whereShoudIGo = 5;
                    FinalDirection();
                }
                else if (roadLength <= 85)
                {
                    whereShoudIGo = 6;
                    FinalDirection();
                }
                else if (roadLength <= 90)
                {
                    whereShoudIGo = 7;
                    FinalDirection();
                }
                else if (roadLength <= 95)
                {
                    whereShoudIGo = 8;
                    FinalDirection();
                }
                else if (roadLength <= 100)
                {
                    whereShoudIGo = 9;
                    FinalDirection();
                }
            }
            else
            {
                Finish();
            }

        }
        //Door Opening Animation Controller
        else if (collided_object.CompareTag("DoorOpen"))
        {
            collided_object.GetComponent<Animator>().SetBool("finishdoor", true);

        }
        else if (collided_object.CompareTag("harmful"))
        {
            speed = 0;
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            harmfulObject = true;
            openFailScreen = true;

        }
        else if (collided_object.CompareTag("Stops"))
        {
            Collected_Coin *= (whereShoudIGo + 1);
        }

    }


    void Set_Camera_Defaults()
    {

        Empty_Percent_Image.enabled = false;
        Fill_Percent_Image.enabled = false;
        Percent_TEXT.enabled = false;
        Fill_Percent_Image.fillAmount = 0f;
    }


    void Turn_Right()
    {
        last_x_pos = Player_Transform.position.x;
        last_z_pos = Player_Transform.position.z;

        Vector3 rot = new Vector3(0f, 90f, 0f);
        Player_Transform.DORotate(rot, Rotation_Duration, RotateMode.LocalAxisAdd);
        if (direction == 1)
        {
            direction = 2;
        }
        else if (direction == 2)
        {
            direction = 3;
        }
        else if (direction == 3)
        {
            direction = 4;
        }
        else if (direction == 4)
        {
            direction = 1;
        }
        Touch_Ref.ekle = 0f;

        Rigidbody.constraints = RigidbodyConstraints.None;
        Rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
        Rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;


    }
    void Turn_Left()
    {
        last_x_pos = Player_Transform.position.x;
        last_z_pos = Player_Transform.position.z;

        Vector3 rot = new Vector3(0f, -90f, 0f);
        Player_Transform.DORotate(rot, Rotation_Duration, RotateMode.LocalAxisAdd);

        if (direction == 1)
        {
            direction = 4;
        }
        else if (direction == 2)
        {
            direction = 1;
        }
        else if (direction == 3)
        {
            direction = 2;
        }
        else if (direction == 4)
        {
            direction = 3;
        }

        Touch_Ref.ekle = 0f;

        Rigidbody.constraints = RigidbodyConstraints.None;
        Rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
        Rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
    }
    public void Move_Forward()
    {

        if (direction == 1)
        {

            Player_Transform.position = new Vector3(Player_Transform.position.x, Player_Transform.position.y,
Player_Transform.position.z + (speed * Time.deltaTime));

            last_z_pos = Player_Transform.position.z;
        }
        else if (direction == 2)
        {

            Player_Transform.position = new Vector3(Player_Transform.position.x + (speed * Time.deltaTime), Player_Transform.position.y,
Player_Transform.position.z);

            last_x_pos = Player_Transform.position.x;
        }
        else if (direction == 3)
        {

            Player_Transform.position = new Vector3(Player_Transform.position.x, Player_Transform.position.y,
Player_Transform.position.z - (speed * Time.deltaTime));

            last_z_pos = Player_Transform.position.z;
        }
        else if (direction == 4)
        {

            Player_Transform.position = new Vector3(Player_Transform.position.x - (speed * Time.deltaTime), Player_Transform.position.y,
Player_Transform.position.z);

            last_x_pos = Player_Transform.position.x;

        }
    }

    void Swipe_Actions()
    {

        if (Touch_Ref.ekle != 0)
        {
            if (direction == 1)
            {
                float new_pos_x = last_x_pos + (Touch_Ref.ekle * 30f * Time.deltaTime);
                if (Can_Swipe_Further(Current_Surface_Transform, new_pos_x, direction))
                {
                    Player_Transform.position = new Vector3(new_pos_x, Player_Transform.position.y, Player_Transform.position.z);
                    last_x_pos = Player_Transform.position.x;
                }
                else
                {

                }
                Touch_Ref.ekle = 0f;
            }
            else if (direction == 3)
            {


                float new_pos_x = last_x_pos - (Touch_Ref.ekle * 40f * Time.deltaTime);
                if (Can_Swipe_Further(Current_Surface_Transform, new_pos_x, direction))
                {
                    Player_Transform.position = new Vector3(new_pos_x, Player_Transform.position.y, Player_Transform.position.z);
                    last_x_pos = Player_Transform.position.x;
                }
                Touch_Ref.ekle = 0f;
            }
            else if (direction == 2)
            {


                //Debug.Log("Z Axis");
                float new_pos_z = last_z_pos - (Touch_Ref.ekle * 50f * Time.deltaTime);
                if (Can_Swipe_Further(Current_Surface_Transform, new_pos_z, direction))
                {
                    Player_Transform.position = new Vector3(Player_Transform.position.x, Player_Transform.position.y, new_pos_z);
                    last_z_pos = Player_Transform.position.z;
                }
                Touch_Ref.ekle = 0f;
            }
            else if (direction == 4)
            {

                float new_pos_z = last_z_pos + (Touch_Ref.ekle * 50f * Time.deltaTime);
                if (Can_Swipe_Further(Current_Surface_Transform, new_pos_z, direction))
                {
                    Player_Transform.position = new Vector3(Player_Transform.position.x, Player_Transform.position.y, new_pos_z);
                    last_z_pos = Player_Transform.position.z;
                }
                Touch_Ref.ekle = 0f;
            }
        }
        
    }

    bool Can_Swipe_Further(Transform Surface_Transform, float Desired_Pos, int direction)
    {
        bool ret;
        if ((direction == 1) || (direction == 3))
        {
            if (Desired_Pos < (Surface_Transform.position.x - Surface_Transform.localScale.x / 2) + 0.1f)
            {
                ret = false;
            }
            else if (Desired_Pos > (Surface_Transform.position.x + Surface_Transform.localScale.x / 2) - 0.1f)
            {
                ret = false;
            }
            else
            {
                ret = true;
            }

        }
        else //if ((direction == 2)  (direction == 4))
        {
            if (Desired_Pos < (Surface_Transform.position.z - Surface_Transform.localScale.x / 2) + 0.1f ||
                Desired_Pos > (Surface_Transform.position.z + Surface_Transform.localScale.x / 2) - 0.1f)
            {
                ret = false;
            }
            else
            {
                ret = true;
            }
        }

        return ret;
    }

    void Finish()
    {

        On_Ground = false;
        At_End = true;
        if (Bar.fillAmount >= 0.5f)
        {
            animator.SetBool("sad", false);

            animator.SetBool("walking", false);

            StartCoroutine(WaitForFinishDance());
        }
        else
        {
            animator.SetBool("happy", false);

            animator.SetBool("walking", false);

            StartCoroutine(WaitForFinishCry());
            //GoogleAds.Instance.CallAds();
        }


    }

    void check_direction(int dir)
    {
        if (dir == 5)
        {
            direction = 1;
        }

        if (dir == 0)
        {
            direction = 4;
        }
    }

    void set_bar_percent()
    {
        if (first_door)
        {
            if (Is_First_Player)
            {
                max_score = (ObjectSpawner_Ref.max_true_object * 1) + (SpawnDoor.Other_Doors_List.Count * 2);
            }


        }
        Bar.fillAmount = ((float)Score) / (((float)max_score / 10) * 7);
        PlayerPrefs.SetFloat("barPercent", Bar.fillAmount);

        if (Is_First_Player)
        {
            if (Bar.fillAmount >= 0.5f)
            {
                //player copy = null;

                for (int char_counter = 0; char_counter < All_Characters.Length; char_counter++)
                {
                    if (All_Characters[char_counter].name[All_Characters[char_counter].name.Length - 1].ToString() == this.Profession_Type.ToString())
                    {
                        //copy = All_Characters[char_counter];
                        new_player = All_Characters[char_counter];


                        if (new_player)
                        {
                            new_player.gameObject.SetActive(true);
                            new_player.Player_Transform.position = Player_Transform.position;
                            new_player.Player_Transform.rotation = Player_Transform.rotation;
                            Instantiate(changePlayerEffect, new_player.Player_Transform.position + new Vector3(0, 2, 0), new_player.Player_Transform.rotation);
                            Debug.Log("name : " + new_player.name);
                            new_player.Set_New_Player_Stats(Profession_Type, max_score, Score, Collected_Coin, false,
                                Current_Surface_Transform, last_x_pos, last_z_pos,
                                direction, first_door, barFilled, first_time_ground);
                            UIManager.Set_Player(new_player);
                            new_player.animator.SetBool("walking", true);


                            this.gameObject.SetActive(false);
                        }


                    }
                }
            }
        }
    }

    void enable_fill_image_by_percent()
    {
        Empty_Percent_Image.enabled = true;
        Fill_Percent_Image.enabled = true;
        end_timer = Time.deltaTime;
    }

    void FinalDirection()
    {
        _stops[whereShoudIGo].SetActive(true);
        Debug.Log("Bar Filled : " + Bar.fillAmount);

    }

    public void Set_New_Player_Stats(int P_Profession_Type, int P_max_score, int P_Score, int P_Collected_Coin, bool P_Is_First_Player,
        Transform P_Current_Surface_Transform, float P_last_x_pos, float P_last_z_pos, int P_direction, bool P_first_door, bool P_barFilled,
        bool P_first_time_ground)
    {
        Profession_Type = P_Profession_Type;
        Debug.Log(this.name + "'s proffesion type is :" + this.Profession_Type);
        max_score = P_max_score;
        Score = P_Score;
        Collected_Coin = P_Collected_Coin;
        Is_First_Player = P_Is_First_Player; //false

        Current_Surface_Transform = P_Current_Surface_Transform;
        last_x_pos = P_last_x_pos;
        last_z_pos = P_last_z_pos;
        direction = P_direction;
        first_door = P_first_door;
        barFilled = P_barFilled;

        first_time_ground = P_first_time_ground; /////////////////true
    }

    IEnumerator WaitForFinishCry()
    {

        animator.SetBool("sad", true);
        yield return new WaitForSeconds(3f);

        enable_fill_image_by_percent();
        yield return new WaitForSeconds(5f);
        Fill_Percent_Image.enabled = false;
        Empty_Percent_Image.enabled = false;
        Percent_TEXT.enabled = false;

        openFailScreen = true;
        openWinScreen = false;

    }

    IEnumerator WaitForFinishDance()
    {

        //animator.SetBool("sad", false);
        animator.SetBool("happy", true);

        yield return new WaitForSeconds(3f);

        enable_fill_image_by_percent();
        yield return new WaitForSeconds(5f);
        Fill_Percent_Image.enabled = false;
        Empty_Percent_Image.enabled = false;
        Percent_TEXT.enabled = false;
        openWinScreen = true;
        openFailScreen = false;
    }

    private class Json_Data_Class
    {
        public int max_reachable_level = 0;
    }

}
