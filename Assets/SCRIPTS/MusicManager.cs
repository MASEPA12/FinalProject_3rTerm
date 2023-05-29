using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicManager : MonoBehaviour
{
    public static MusicManager sharedInstance;

    
    //Sound effect
    private AudioClip jumpSound;
    private AudioClip recollectSound;
    private AudioClip powerUpSound1;
    private AudioClip powerUpSound2;
    private AudioClip fireballSound;
    private AudioClip damageSound;
    private AudioClip restoreSound;
    private AudioClip hungrySound;
    private AudioClip winSound;
    private AudioClip loseSound;

    //Background music
    public AudioClip[] backgroundSound;

    private AudioSource _audioSource;

    //volumen
    public float backgroundVol;
    public float effectsVol;


    // Start is called before the first frame update
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound, float volumen )
    {
        _audioSource.clip = sound;
        _audioSource.Play();
    }

    public void changeVolumen(float volumen) {
        _audioSource.volume = volumen;
    }
}
