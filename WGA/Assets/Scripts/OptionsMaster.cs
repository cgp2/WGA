using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMaster : MonoBehaviour
{
    public static float SoundLevel;

    public static float MusicLevel;

    public static int Difficult;

    private PlayerInfo pl;

    private ProfileNames pn;
    // Use this for initialization
    void Start()
    {
        pn = new ProfileNames();
        pn.InitializeLastProfile();

        var plName = pn.GetCurrentProfileName();
        pl = new PlayerInfo(plName);

        GameObject.Find("music").GetComponent<Slider>().value = SoundMaster.MusicLevel * 100;
        GameObject.Find("sound").GetComponent<Slider>().value = SoundMaster.SoundLevel * 100;

        GameObject.Find("Dropdown").GetComponent<Dropdown>().ClearOptions();
        var names = pn.ProfileList;
        foreach (var n in names)
        {
            GameObject.Find("Dropdown").GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(n));
        }

        GameObject.Find("Dropdown").GetComponent<Dropdown>().value = pn.CurrentProfile;
        GameObject.Find("Dropdown").GetComponent<Dropdown>().RefreshShownValue();

    }


    public void AddNewProfile()
    {
        if (GameObject.Find("NewProfile").GetComponent<Text>().text != "" && !pn.ProfileList.Contains(GameObject.Find("NewProfile").GetComponent<Text>().text))
        {
            var t = pn.ProfileList;
            var list = new List<string>(t);
            var name = GameObject.Find("NewProfile").GetComponent<Text>().text;
            list.Add(name);
            pn.ProfileList = list.ToArray();
            pn.CurrentProfile = pn.ProfileList.Length - 1;
            pn.SaveToFile();

            var newPl = new PlayerInfo();
            newPl.InitializeByName(name);
            StartCoroutine(SaveProfile(0.1f, newPl));
            pl = newPl;
        }
    }

    private IEnumerator SaveProfile(float seconds, PlayerInfo pl)
    {    
        yield return new WaitForSeconds(seconds);
        pl.SaveToFile(pl.Name);

        GameObject.Find("Dropdown").GetComponent<Dropdown>().ClearOptions();
        var names = pn.ProfileList;
        foreach (var n in names)
        {
            GameObject.Find("Dropdown").GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(n));
        }

        GameObject.Find("Dropdown").GetComponent<Dropdown>().value = pn.CurrentProfile;
        GameObject.Find("Dropdown").GetComponent<Dropdown>().RefreshShownValue();
    }


    public void ProfileChange()
    {
        var r = GameObject.Find("Dropdown").GetComponent<Dropdown>().value;
        pn.CurrentProfile = r;
        pl = new PlayerInfo(pn.GetCurrentProfileName());
        GameObject.Find("music").GetComponent<Slider>().value = pl.Opt.MusicVolume * 100;
        GameObject.Find("sound").GetComponent<Slider>().value = pl.Opt.SoundVolume * 100;
        SoundMaster.SoundLevel = pl.Opt.SoundVolume * 100;
        SoundMaster.MusicLevel = pl.Opt.MusicVolume * 100;

    }

    public void ToMainMenu()
    {
        pn.SaveToFile();

        var plName = pn.GetCurrentProfileName();
        pl.SaveToFile(plName);
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
    void Update()
    {

    }
}
