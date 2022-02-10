using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public GameObject bgMusic;
    public GameObject coin;
    private bool isSoundOn;
    private bool isMusicOn;

    [SerializeField] private GameObject soundOnButton;
    [SerializeField] private GameObject soundOffButton;

    [SerializeField] private GameObject musicOnButton;
    [SerializeField] private GameObject musicOffButton;

    private void Start()
    {
        isSoundOn = true; // save gelicek
        isMusicOn = true;
    }


 


    public void CoinSoundActive()
    {
        if (isSoundOn)
        {
            coin.SetActive(true);
            StartCoroutine(CoinSoundDeActive());
        }

    }

    private IEnumerator CoinSoundDeActive()
    {
        yield return new WaitForSeconds(0.4f);
        coin.SetActive(false);
    }

    public void SoundOn()
    {
        soundOnButton.SetActive(true);
        soundOffButton.SetActive(false);

        isSoundOn = true;
    }

    public void SoundOff()
    {
        soundOnButton.SetActive(false);
        soundOffButton.SetActive(true);

        isSoundOn = false;
    }

    public void MusicOn()
    {
        musicOnButton.SetActive(true);
        musicOffButton.SetActive(false);

        bgMusic.SetActive(true);

        isMusicOn = true;
    }

    public void MusicOff()
    {
        musicOnButton.SetActive(false);
        musicOffButton.SetActive(true);

        bgMusic.SetActive(false);

        isMusicOn = false;
    }
    
}
