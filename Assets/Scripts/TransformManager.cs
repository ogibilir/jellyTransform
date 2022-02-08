using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransformManager : MonoBehaviour
{
    [SerializeField] private GameObject _cheese;
    [SerializeField] private GameObject _cheeseRenderer;
    [SerializeField] private GameObject _pie;
    [SerializeField] private GameObject _pieRenderer;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _circleRenderer;

    [SerializeField] private List<GameObject> _heart;
    [SerializeField] private List<GameObject> _star;
    [SerializeField] private List<GameObject> _snow;


    public ParticleSystem particle;

    [SerializeField] private Material red;
    [SerializeField] private Material blue;
    private void OnTriggerEnter(Collider other)
    {
        #region Kaybetme
        if (other.tag == "Saw" || other.tag == "Spikes")
        {

        }
        if(other.tag == "Space")
        {
            Destroy(Camera.main.gameObject.GetComponent<CameraScript>());
            Destroy(gameObject.GetComponent<PlayerMovement>());
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<Collider>().isTrigger = true;
            Vector3 currentPos = transform.position;
            Vector3 diePos = new Vector3(transform.position.x, -120f, transform.position.z);
            Debug.Log("Oyunu bitir");
            transform.DOMoveY(-6f, 2f).SetEase(Ease.InOutElastic);
        }
        #endregion
        #region Sekil Degistirme
        if (other.gameObject.tag == "Comb")
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
        if (other.tag == "Cube")
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
        if (other.tag == "Clasp")
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
        #endregion
        #region Dokme islemi
        if (other.tag == "Star")
        {
            for (int i = 0; i < _star.Count; i++)
            {
                _star[i].SetActive(true);
            }
        }
        if (other.tag == "Heart")
        {
            for (int i = 0; i < _heart.Count; i++)
            {
                _heart[i].SetActive(true);
            }
        }
        if(other.tag == "Snow")
        {
            for (int i = 0; i < _heart.Count; i++)
            {
                _snow[i].SetActive(true);
            }
        }
        #endregion
        #region Renk Degistirme
        if (other.tag == "RedColor")
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
        #endregion
    }
}
