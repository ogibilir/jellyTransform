using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Object_Spawner : MonoBehaviour
{

    public UIManager UI_Manager;
    public GameObject[] Collectable_Objects_Array;
    public Container_Script[] Objects_Spawn_Points;
    
    
    public Spawn_Door SpawnDoor_Ref;
    private player Player;
    

    private List<GameObject> False_Objects;
    private List<GameObject> True_Objects = null;

    public int max_true_object = 0;
    private void Start()
    {

    }

    public void Spawn_Objects()
    {
        Player = UI_Manager._player;

        for (int door_counter = 0; door_counter < SpawnDoor_Ref.Spawn_Point_Array.Length; door_counter++)
        {
            int true_type, false_type = 0;
            if (door_counter == 0)
            {
                int.TryParse(Player.Profession_Type.ToString(), out true_type);
                if (int.Parse(SpawnDoor_Ref.First_Left_Door.tag[SpawnDoor_Ref.First_Left_Door.tag.Length - 1].ToString()) != Player.Profession_Type)
                {
                    int.TryParse(SpawnDoor_Ref.First_Left_Door.tag[SpawnDoor_Ref.First_Left_Door.tag.Length - 1].ToString(),out false_type);
                }
                if (int.Parse(SpawnDoor_Ref.First_Right_Door.tag[SpawnDoor_Ref.First_Right_Door.tag.Length - 1].ToString()) != Player.Profession_Type)
                {
                    int.TryParse(SpawnDoor_Ref.First_Right_Door.tag[SpawnDoor_Ref.First_Right_Door.tag.Length - 1].ToString(),out false_type);
                }

                Set_True_and_False_Objects(true_type, false_type);
                for (int object_counter = 0; object_counter < Objects_Spawn_Points[door_counter].Spawn_Points.Length; object_counter++)
                {
                    Transform Object_Transform = Objects_Spawn_Points[door_counter].Spawn_Points[object_counter];
                    Spawn_Single_Object(Object_Transform);
                }
            }
            else
            {
                true_type = Player.Profession_Type;
                false_type = int.Parse(SpawnDoor_Ref.Added_Other_Doors_by_Order[door_counter - 1]
                    .tag[SpawnDoor_Ref.Added_Other_Doors_by_Order[door_counter - 1].tag.Length - 1].ToString());
                Set_True_and_False_Objects(true_type, false_type);
                
                for (int object_counter = 0; object_counter < Objects_Spawn_Points[door_counter].Spawn_Points.Length; object_counter++)
                {
                    Transform Object_Transform = Objects_Spawn_Points[door_counter].Spawn_Points[object_counter];
                    Spawn_Single_Object(Object_Transform);
                }
            }
            False_Objects.Clear();
            True_Objects.Clear();
        }
    }

    void Spawn_Single_Object( Transform Spawn_Transform)
    {
        if (Spawn_Transform.CompareTag("True_Spawn_Object"))
        {
            int index = Random.Range(0, True_Objects.Count - 1);  //choose object randomly in True_Objects list
            Instantiate(True_Objects[index], Spawn_Transform.position, Spawn_Transform.rotation);
            max_true_object += 1;
        }
        else if (Spawn_Transform.CompareTag("False_Spawn_Object"))
        {
            int index = Random.Range(0, False_Objects.Count - 1);  //choose object randomly in False_Objects list
            Instantiate(False_Objects[index], Spawn_Transform.position, Spawn_Transform.rotation);
            
        }
        Destroy(Spawn_Transform.gameObject);
    }

    private void Set_True_and_False_Objects(int profession_type, int other_type)
    {
        False_Objects = new List<GameObject>();
        True_Objects = new List<GameObject>();
        
        for (int object_counter = 0; object_counter < Collectable_Objects_Array.Length; object_counter++)
        {
            string current_object_tag = Collectable_Objects_Array[object_counter].tag;
            
            if (int.Parse(current_object_tag[current_object_tag.Length-1].ToString()) == profession_type)
            {
                //Set True Spawnable Object List 
                True_Objects.Add(Collectable_Objects_Array[object_counter]);
            }
            else if (int.Parse(current_object_tag[current_object_tag.Length-1].ToString()) == other_type)
            {
                //Set False Spawnable Object List 
                False_Objects.Add(Collectable_Objects_Array[object_counter]);
            }
            
        }
    }
    
    
}
