using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class ProfileNames
{
    public string[] ProfileList;
    public int CurrentProfile;

    public ProfileNames()
    {
        
    }

    public ProfileNames(int currentProfile)
    {
        CurrentProfile = currentProfile;

        ProfileList = GetAllProfiles();
    }

    // Use this for initialization
    public static string[] GetAllProfiles()
    {
        var ret = new string[0];
        var path = Application.dataPath + "/PlayerInfo/ProfilesList.dat";
        if (!File.Exists(path))
        {
            File.Create(path);
            return ret;
        }
        else
        {
            var dataJSon = File.ReadAllText(path);

            var prof = JsonUtility.FromJson<ProfileNames>(dataJSon);
            return prof.ProfileList;
        }
    }

    public void InitializeLastProfile()
    {
        var path = Application.dataPath + "/PlayerInfo/ProfilesList.dat";
        if (!File.Exists(path))
        {
            File.Create(path);
            //SaveToFile();
        }
        else
        {
            var dataJSon = File.ReadAllText(path);

            var prof = JsonUtility.FromJson<ProfileNames>(dataJSon);
            ProfileList = prof.ProfileList;
            CurrentProfile = prof.CurrentProfile;
        }
    }

    public string GetCurrentProfileName()
    {
        //var r = GetAllProfiles();
        return ProfileList[CurrentProfile];
    }

    public void SaveToFile()
    {
        var data = JsonUtility.ToJson(this);
    
        File.WriteAllText(Application.dataPath + "/PlayerInfo/ProfilesList.dat", data);
    }



    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
