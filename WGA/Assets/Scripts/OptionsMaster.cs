using UnityEngine;
using UnityEngine.UI;

public class OptionsMaster : MonoBehaviour
{
    public static float SoundLevel;

    public static float MusicLevel;

    public static int Difficult;

    private PlayerInfo pl;
	// Use this for initialization
	void Start ()
	{
	    pl = new PlayerInfo(Application.dataPath + "/PlayerInfo/PlayerInfo.dat");

        GameObject.Find("music").GetComponent<Slider>().value = SoundMaster.MusicLevel * 100;
	    GameObject.Find("sound").GetComponent<Slider>().value = SoundMaster.SoundLevel * 100;

    }

    public void ToMainMenu()
    {
        pl.SaveToFile(Application.dataPath + "/PlayerInfo/PlayerInfo.dat");
        SoundMaster.PauseMusic();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ChangeMusicValue()
    {
        MusicLevel = GameObject.Find("music").GetComponent<Slider>().value / 100f;
        MusicLevel = Mathf.Clamp(MusicLevel, 0, 1);
        pl.Opt.MusicVolume = MusicLevel;
        SoundMaster.ChangeMusicVolume(MusicLevel);
    }

    public void ChangeSoundValue()
    {
        SoundLevel = GameObject.Find("sound").GetComponent<Slider>().value / 100f;
        SoundLevel = Mathf.Clamp(SoundLevel, 0, 1);
        pl.Opt.SoundVolume = SoundLevel;
        SoundMaster.SoundLevel = SoundLevel;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
