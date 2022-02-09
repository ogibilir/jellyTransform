using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Touch_Mechs : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public float Start_Pos, Current_Pos_X, end_pos_X;
    public float ekle = 0f;
    private bool first_time = true;
    private int direction = 1;

    public void OnPointerDown(PointerEventData eventData)
    {

        if (first_time)
        {
           


            set_direction();
            if ((direction == 1) || (direction == 3))
            {
                Start_Pos = transform.parent.gameObject.transform.position.x;
            }
            else if ((direction == 2) || (direction == 4))
            {
                Start_Pos = transform.parent.gameObject.transform.position.z;
            }

            first_time = false;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        set_direction();
        Current_Pos_X = eventData.position.x;

        if (Start_Pos - Current_Pos_X <= 0)
        {
            if (Start_Pos - Current_Pos_X < -3) // swipe threshold ( swipe to left (- x axis))
            {
                ekle = Mathf.Clamp(ekle += 0.6f, -1f, 1f); //swipe input multiplier (left)
            }

            Start_Pos = Current_Pos_X;
        }
        if (Start_Pos - Current_Pos_X > 0)
        {
            if (Start_Pos - Current_Pos_X > 3) // swipe threshold ( swipe to right (+ x axis))
            {
                ekle = Mathf.Clamp(ekle -= 0.6f, -1f, 1f);  //swipe input multiplier (right)
            }

            Start_Pos = Current_Pos_X;
        }
    }

    void set_direction()
    {
        Debug.Log("Parent rotation :" + transform.parent.transform.rotation.y);
        float rot = transform.parent.gameObject.transform.rotation.y;

        if (rot < 0)
        {
            while (rot > 0)
            {
                rot += 360;
            }
        }
        else if (rot > 360)
        {
            while (rot <= 360)
            {
                rot -= 360;
            }
        }

        if (rot < 90)
        {
            direction = 1;
        }
        else if (rot < 180)
        {
            direction = 2;
        }
        else if (rot < 270)
        {
            direction = 3;
        }
        else if (rot < 360)
        {
            direction = 4;
        }
        else if (rot == 360)
        {
            rot = 0;
            direction = 1;
        }
        Debug.Log("outpout direction is :" + direction);
    }
}
