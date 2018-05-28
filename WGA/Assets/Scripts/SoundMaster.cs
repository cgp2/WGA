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
    public static AudioSource MusicSource;
    public static AudioSource SoundSource;
    public static float PauseTime;
    public static AudioClip CurrentClip;

    private bool isToogled = false;

    public PlayerInfo pl;


    // Use this for initialization
    void Start()
    {
        var pn = new ProfileNames();
        pn.InitializeLastProfile();
        var plName = pn.GetCurrentProfileName();
        pl = new PlayerInfo(plName);

        SoundLevel = pl.Opt.SoundVolume;
        MusicLevel = pl.Opt.MusicVolume;
            
        SoundSource = gameObject.AddComponent<AudioSource>();
        MusicSource = gameObject.AddComponent<AudioSource>();
        MusicSource.volume = MusicLevel;

        //Clips = new List<AudioClip>();
        //var r = Resources.LoadAll<AudioClip>("Music/");
        if (Clips == null)
        {
            Clips = Resources.LoadAll<AudioClip>("Music/");
        }
        
        PlayMusic();
    }

    public static void ToggleMusicSound()
    {
        MusicSource.volume = MusicSource.volume == 0f ? MusicLevel : 0f;
    }

    public static void PauseMusic()
    {
        if (MusicSource != null)
        {
            MusicSource.Pause();
            PauseTime = MusicSource.time;
            CurrentClip = MusicSource.clip;
        }
    }

    public static void ChangeMusicVolume(float newValue)
    {
        MusicLevel = newValue;
        if (MusicSource)
        {
            MusicSource.volume = MusicLevel;
        }
    }

    public static void ChangeSoundVolume(float newValue)
    {
        SoundLevel = newValue;
        if (SoundSource)
        {
            SoundSource.volume = MusicLevel;
        }
    }

    public static void ResetMusic()
    {
        PauseTime = 0f;
        CurrentClip = null;
    }

    public static void PlayMusic()
    {
        if (PauseTime == 0f)
        {
            MusicSource.clip = Clips[Random.Range(0, Clips.Length)];
            MusicSource.Play();
        }
        else
        {
            MusicSource.clip = CurrentClip;
            MusicSource.time = PauseTime;
            PauseTime = 0f;
            MusicSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MusicSource)
        {
            if (!MusicSource.isPlaying && PauseTime == 0f && Application.isFocused && !isToogled)
            {
                isToogled = true;
                ResetMusic();
                var delay = Random.Range(5, 15);
                StartCoroutine(NextClipStart(delay));
            }
        }
    }

    private IEnumerator NextClipStart(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PlayMusic();
        isToogled = false;
    }
}
