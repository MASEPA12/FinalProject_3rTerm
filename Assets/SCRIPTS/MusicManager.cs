using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicManager : MonoBehaviour
{
    public static MusicManager sharedInstance;

    
    //Sound effect
    public AudioClip jumpSound;
    public AudioClip recollectSound;
    public AudioClip powerUpSound1;
    public AudioClip powerUpSound2;
    public AudioClip fireballSound;
    public AudioClip damageSound;
    public AudioClip restoreSound;
    public AudioClip hungrySound;
    public AudioClip winSound;
    public AudioClip loseSound;

    //Background music
    public AudioClip[] backgroundSound;

    private AudioSource _audioSource;


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

    private void Start()
    {
        
    }

    public void PlaySound(AudioClip sound)
    {
        _audioSource.clip = sound;
        _audioSource.Play();
    }

    public void changeVolumen(float volumen) {
        _audioSource.volume = volumen;
    }
}
