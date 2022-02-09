using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confetti : MonoBehaviour
{
    public GameObject confetti1;
    public GameObject confetti2;
    private player Player;


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag=="Player")
        {
            Player = FindObjectOfType<player>();
            if (Player.gameObject.name == collision.gameObject.name)
            {
                if (Player.Bar.fillAmount > 0.99)
                {
                    confetti1.SetActive(true);
                    confetti2.SetActive(true);
                }
            }
        }
    }
}
