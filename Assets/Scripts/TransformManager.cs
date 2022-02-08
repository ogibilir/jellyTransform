using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformManager : MonoBehaviour
{
    [SerializeField] private GameObject _cheese;
    [SerializeField] private GameObject _cheeseRenderer;
    [SerializeField] private GameObject _pie;
    [SerializeField] private GameObject _pieRenderer;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _circleRenderer;

    [SerializeField] private List<GameObject> _white;
    [SerializeField] private List<GameObject> _black;


    public ParticleSystem particle;

    [SerializeField] private Material red;
    [SerializeField] private Material blue;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cheese")
        {
            PlayerMovement._stopTime = 2f;
            if (gameObject.layer == 8)
            {
                _circle.SetActive(false);
            }
            else if (gameObject.layer == 9)
            {
                _pie.SetActive(false);
            }
            _cheese.SetActive(true);
            particle.Play();
            gameObject.layer = 10;
        }
        if (other.tag == "Circle")
        {
            PlayerMovement._stopTime = 2f;
            if (gameObject.layer == 10)
            {
                _cheese.SetActive(false);
            }
            else if (gameObject.layer == 9)
            {
                _pie.SetActive(false);
            }
            _circle.SetActive(true);
            particle.Play();
            gameObject.layer = 8;
        }
        if (other.tag == "Pie")
        {
            PlayerMovement._stopTime = 2f;
            if (gameObject.layer == 8)
            {
                _circle.SetActive(false);
            }
            else if (gameObject.layer == 10)
            {
                _cheese.SetActive(false);
            }
            _pie.SetActive(true);
            particle.Play();
            gameObject.layer = 9;
        }
        if (other.tag == "Black")
        {
            PlayerMovement._stopTime = 2f;
            var Obj = other.transform.GetChild(0);
            Obj.gameObject.SetActive(true);
            for (int i = 0; i < _white.Count; i++)
            {
                _white[i].SetActive(false);
            }
            for (int i = 0; i < _black.Count; i++)
            {
                _black[i].SetActive(true);
            }
        }
        if (other.tag == "White")
        {
            PlayerMovement._stopTime = 2f;
            var Obj = other.transform.GetChild(0);
            Obj.gameObject.SetActive(true);
            for (int i = 0; i < _white.Count; i++)
            {
                _white[i].SetActive(true);
            }
            for (int i = 0; i < _black.Count; i++)
            {
                _black[i].SetActive(false);
            }
        }
        if(other.tag == "RedColor")
        {
            _circleRenderer.GetComponent<Renderer>().material = red;
            _cheeseRenderer.GetComponent<Renderer>().material = red;
            _pieRenderer.GetComponent<Renderer>().material = red;
        }
        if (other.tag == "BlueColor")
        {
            _circleRenderer.GetComponent<Renderer>().material = blue;
            _cheeseRenderer.GetComponent<Renderer>().material = blue;
            _pieRenderer.GetComponent<Renderer>().material = blue;
        }
    }
}
