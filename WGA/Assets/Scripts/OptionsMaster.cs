using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMaster : MonoBehaviour
{
    public static float SoundLevel;

    public static float MusicLevel;

    public static int Difficult;
	// Use this for initialization
	void Start ()
	{
	    GameObject.Find("music").GetComponent<Slider>().value = SoundMaster.MusicLevel * 100;
	    GameObject.Find("sound").GetComponent<Slider>().value = SoundMaster.SoundLevel * 100;
	}

    public void ChangeMusicValue()
    {
        MusicLevel = GameObject.Find("music").GetComponent<Slider>().value / 100f;
        MusicLevel = Mathf.Clamp(MusicLevel, 0, 1);
        SoundMaster.ChangeMusicVolume(MusicLevel);
    }

    public void ChangeSoundValue()
    {
        SoundLevel = GameObject.Find("sound").GetComponent<Slider>().value / 100f;
        SoundLevel = Mathf.Clamp(SoundLevel, 0, 1);
        SoundMaster.SoundLevel = SoundLevel;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
