using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSourc;

    [Header("Audio Clip")]
    public AudioClip bk;
    public AudioClip bullet;

    private void Start()
    {
        musicSource.clip = bk;
        musicSource.Play();
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSourc.PlayOneShot(clip);
    }
}
