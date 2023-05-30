using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicManager : MonoBehaviour
{
    public static MusicManager sharedInstance;

    
    //Sound effect
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip recollectSound;
    [SerializeField] private AudioClip powerUpSound1;
    [SerializeField] private AudioClip powerUpSound2;
    [SerializeField] private AudioClip fireballSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip restoreSound;
    [SerializeField] private AudioClip hungrySound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    //Background music
    public AudioClip[] backgroundSound;

    private AudioSource _audioSource;

    //volumen
    public float backgroundVol = 1f;
    public float effectsVol = 1f;


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
        MenuMusic();
    }

    private void PlaySong(AudioClip sound)
    {
        _audioSource.clip = sound;
        _audioSource.Play();
    }

    public void changeVolumen(float volumen) {
        backgroundVol = volumen;
        _audioSource.volume = backgroundVol;
    }

    public void MenuMusic() {
        PlaySong(backgroundSound[0]);
    }

    public void LevelMusic(int level) {
        PlaySong(backgroundSound[level]);
    }

    private void PlaySoundEffect(AudioClip sound, float volumen) {
        _audioSource.PlayOneShot(sound, volumen);
    }

    public void changeEffectVolumen(float value)
    {
        effectsVol = value;
    }

    public void JumpSound() {
        PlaySoundEffect(jumpSound,effectsVol);
    }

    public void RecollectSound()
    {
        PlaySoundEffect(recollectSound, effectsVol);
    }

    public void GiantSound()
    {
        PlaySoundEffect(powerUpSound1, effectsVol);
    }

    public void FastSound()
    {
        PlaySoundEffect(powerUpSound2, effectsVol);
    }

}
