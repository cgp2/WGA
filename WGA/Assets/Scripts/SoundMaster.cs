using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundMaster : MonoBehaviour
{

    public static float SoundLevel = 1f;

    public static float MusicLevel = 1f;

    public static AudioClip[] Clips;
    public static AudioSource SoundSource;
    public static float PauseTime;
    public static AudioClip CurrentClip;

    private bool isToogled = false;


    // Use this for initialization
    void Start()
    {

        SoundSource = gameObject.AddComponent<AudioSource>();
        SoundSource.volume = MusicLevel;

        //Clips = new List<AudioClip>();
        //var r = Resources.LoadAll<AudioClip>("Music/");
        if (Clips == null)
        {
            Clips = Resources.LoadAll<AudioClip>("Music/");
        }
        
        PlaySound();
    }

    public static void ToggleMusicSound()
    {
        SoundSource.volume = SoundSource.volume == 0f ? MusicLevel : 0f;
    }

    public static void PauseSound()
    {
        if (SoundSource != null)
        {
            SoundSource.Pause();
            PauseTime = SoundSource.time;
            CurrentClip = SoundSource.clip;
        }
    }

    public static void ChangeMusicVolume(float newValue)
    {
        MusicLevel = newValue;
        if (SoundSource)
        {
            SoundSource.volume = MusicLevel;
        }
    }

    public static void ResetSound()
    {
        PauseTime = 0f;
        CurrentClip = null;
    }

    public static void PlaySound()
    {
        if (PauseTime == 0f)
        {
            SoundSource.clip = Clips[Random.Range(0, Clips.Length)];
            SoundSource.Play();
        }
        else
        {
            SoundSource.clip = CurrentClip;
            SoundSource.time = PauseTime;
            PauseTime = 0f;
            SoundSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SoundSource)
        {
            if (!SoundSource.isPlaying && PauseTime == 0f && Application.isFocused && !isToogled)
            {
                isToogled = true;
                ResetSound();
                var delay = Random.Range(5, 15);
                StartCoroutine(NextClipStart(delay));
            }
        }
    }

    private IEnumerator NextClipStart(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PlaySound();
        isToogled = false;
    }
}
