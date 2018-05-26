using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;

[Serializable]
public class PlayerInfo {
    public string Name;
    public string DeckName;
    public int Level;
    public int Exp;
    public int ExpToNextLevel;
    public string PathToAvatar;
    public PlayerInfo(string path)
    {
        var playerFromFile = ReadFromFile(path);
        this.Name = playerFromFile.Name;
        this.Level = playerFromFile.Level;
        this.Exp = playerFromFile.Exp;
        this.PathToAvatar = playerFromFile.PathToAvatar;
        this.ExpToNextLevel = playerFromFile.ExpToNextLevel;
        this.DeckName = playerFromFile.DeckName;
        this.PathToAvatar = playerFromFile.PathToAvatar;
    }
 
    public PlayerInfo()
    {

    }
    public PlayerInfo NewPlayer()
    {
        var newPlayer = new PlayerInfo()
        {
            Name = "Default",
            Level = 0,
            Exp = 0,
            ExpToNextLevel = 100
        };
        return newPlayer;
    }
    public void SaveToFile(string path)
    {
        string data = JsonUtility.ToJson(this);
        //if (!File.Exists(Application.dataPath + "/PlayerInfo/PlayerInfo.dat"))
        //{
        //    File.Create(Application.dataPath + "/PlayerInfo/PlayerInfo.dat");
        //    File.WriteAllText(Application.dataPath + "/PlayerInfo/PlayerInfo.dat", data);

        //}
        //else
        File.WriteAllText(path, data);

    }
    public PlayerInfo ReadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            File.Create(path);
            return NewPlayer();
        }
        else
        {
            string dataJSon = File.ReadAllText(path);
            PlayerInfo pl = new PlayerInfo();
            pl = JsonUtility.FromJson<PlayerInfo>(dataJSon);
            return pl;
        }
    }
}

