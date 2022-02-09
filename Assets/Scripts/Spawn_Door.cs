using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class Spawn_Door : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] Spawn_Point_Array;
    public GameObject[] Door_Array;

    public GameObject First_Left_Door;
    public GameObject First_Right_Door;
    
    public List<GameObject> Other_Doors_List;
    private GameObject True_Door;
    
    public List<GameObject> Added_Other_Doors_by_Order;

    private Vector3 left_pos;
    private Vector3 right_pos;
    public void Spawn_First_Doors()
    {
        int door_prefab_1_index = Random.Range(1, 4);
        int door_prefab_2_index;

        while (true)
        {
            door_prefab_2_index = Random.Range(0, 4);
            if (door_prefab_2_index != door_prefab_1_index)
            {
                break;
            }
        }

        First_Left_Door = Door_Array[door_prefab_1_index];
        First_Right_Door = Door_Array[door_prefab_2_index];

        Transform First_point = Spawn_Point_Array[0];
        
        SpawnDoors(First_point,First_Left_Door, First_Right_Door);
        
        //if (First_point.rotation.y == 0 || First_point.rotation.y == 180 )
        //{
        //    float x_pos_1 = First_point.position.x - First_point.localScale.x / 4;
        //    float x_pos_2 = First_point.position.x + First_point.localScale.x / 4;
        //    float y_pos_1 = First_point.position.y - ((First_point.localScale.y - First_Left_Door.transform.localScale.y) / 2);
        //    float y_pos_2 = First_point.position.y - ((First_point.localScale.y - First_Right_Door.transform.localScale.y) / 2);
        //    
        //    
        //    left_pos = new Vector3(x_pos_1, y_pos_1, First_point.position.z);
        //    right_pos = new Vector3(x_pos_2, y_pos_2, First_point.position.z);
        //    
        //    Instantiate(First_Left_Door, left_pos, First_point.rotation);
        //    Instantiate(First_Right_Door, right_pos, First_point.rotation);
        //}
        //else // 90*
        //{
        //    float z_pos_1 = First_point.position.z - First_point.localScale.x / 4;
        //    float z_pos_2 = First_point.position.z + First_point.localScale.x / 4;
        //    float y_pos_1 = First_point.position.y - ((First_point.localScale.y - First_Left_Door.transform.localScale.y) / 2);
        //    float y_pos_2 = First_point.position.y - ((First_point.localScale.y - First_Right_Door.transform.localScale.y) / 2);
        //    
        //    left_pos = new Vector3(First_point.position.x, y_pos_1, z_pos_1);
        //    right_pos = new Vector3(First_point.position.x, y_pos_2, z_pos_2);
        //    
        //    Instantiate(First_Left_Door, left_pos, First_point.rotation);
        //    Instantiate(First_Right_Door, right_pos, First_point.rotation);
        //}


    }

    public void Spawn_Other_Doors(int profession_type)
    {
        GameObject True_Door;
        Other_Doors_List = new List<GameObject>();
        Added_Other_Doors_by_Order = new List<GameObject>();
        
        
        //Setting True Door
        if (int.Parse(First_Left_Door.tag[First_Left_Door.tag.Length - 1].ToString()) == profession_type)
        {
            True_Door = First_Left_Door;
        }
        else
        {
            True_Door = First_Right_Door;
        }
        
        
        // Adding wrong Doors to Other_Doors_List
        for (int door_array_counter = 0; door_array_counter < Door_Array.Length; door_array_counter++)
        {
            if (Door_Array[door_array_counter] != True_Door)
            {
                GameObject Adding_Object = Door_Array[door_array_counter];
                Other_Doors_List.Add(Adding_Object);

            }
        }
        
        for (int spawn_point_counter = 1; spawn_point_counter < Spawn_Point_Array.Length ; spawn_point_counter++)
        {
            //Choose randomly wrong door
            int other_door_index = Random.Range(0, Other_Doors_List.Count-1);
            GameObject Other_Door = Other_Doors_List[other_door_index];
            
            
            Transform Current_Point = Spawn_Point_Array[spawn_point_counter];
            SpawnDoors(Current_Point, True_Door, Other_Door);
            Added_Other_Doors_by_Order.Add(Other_Door);
        }
        
        

    }
    
    

    void SpawnDoors(Transform Spawn_Point_Transform, GameObject Door_1, GameObject Door_2)
    {
        if (Spawn_Point_Transform.rotation.y == 0 || Spawn_Point_Transform.rotation.y == 180 )
        {
            float x_pos_1 = Spawn_Point_Transform.position.x - Spawn_Point_Transform.localScale.x / 4;
            float x_pos_2 = Spawn_Point_Transform.position.x + Spawn_Point_Transform.localScale.x / 4;
            float y_pos_1 = Spawn_Point_Transform.position.y - ((Spawn_Point_Transform.localScale.y - Door_1.transform.localScale.y) / 2);
            float y_pos_2 = Spawn_Point_Transform.position.y - ((Spawn_Point_Transform.localScale.y - Door_2.transform.localScale.y) / 2);
            
            
            left_pos = new Vector3(x_pos_1, y_pos_1, Spawn_Point_Transform.position.z);
            right_pos = new Vector3(x_pos_2, y_pos_2, Spawn_Point_Transform.position.z);
            
            Instantiate(Door_1, left_pos, Spawn_Point_Transform.rotation);
            Instantiate(Door_2, right_pos, Spawn_Point_Transform.rotation);
        }
        else // 90*
        {
            float z_pos_1 = Spawn_Point_Transform.position.z - Spawn_Point_Transform.localScale.x / 4;
            float z_pos_2 = Spawn_Point_Transform.position.z + Spawn_Point_Transform.localScale.x / 4;
            float y_pos_1 = Spawn_Point_Transform.position.y - ((Spawn_Point_Transform.localScale.y - Door_1.transform.localScale.y) / 2);
            float y_pos_2 = Spawn_Point_Transform.position.y - ((Spawn_Point_Transform.localScale.y - Door_2.transform.localScale.y) / 2);
            
            left_pos = new Vector3(Spawn_Point_Transform.position.x, y_pos_1, z_pos_1);
            right_pos = new Vector3(Spawn_Point_Transform.position.x, y_pos_2, z_pos_2);
            
            Instantiate(Door_1, left_pos, Spawn_Point_Transform.rotation);
            Instantiate(Door_2, right_pos, Spawn_Point_Transform.rotation);
        }
    }
    
}
