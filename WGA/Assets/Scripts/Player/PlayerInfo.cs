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
    public int GamesWin;
    public int GamesLost;
    public string PathToAvatar;
    public Guid[] AvaliableCards;
    public Options Opt;
    public PlayerInfo(string path)
    {
        var playerFromFile = ReadFromFile(path);
        Level = playerFromFile.Level;
        Name = playerFromFile.Name;
        Exp = playerFromFile.Exp;
        PathToAvatar = playerFromFile.PathToAvatar;
        ExpToNextLevel = playerFromFile.ExpToNextLevel;
        DeckName = playerFromFile.DeckName;
        PathToAvatar = playerFromFile.PathToAvatar;
        Opt = playerFromFile.Opt;
        GamesWin = playerFromFile.GamesWin;
        GamesLost = playerFromFile.GamesLost;
    }


    public void BattleResults(bool win, int expGain)
    {
        if (win)
        {
            GamesWin++;

            Exp += expGain;
            if (Exp >= ExpToNextLevel)
            {
                Level++;
                Exp = Exp - ExpToNextLevel;
                ExpToNextLevel = Level * 100;
            }
        }
        else
        {
            GamesLost++;
        }
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
            GamesWin = 0,
            GamesLost = 0,
            ExpToNextLevel = 100,
            DeckName="Default",
            Opt = new Options()
            {
                MusicVolume = 0.5f,
                SoundVolume = 0.5f
            }
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
            var dataJSon = File.ReadAllText(path);
            
            var pl = JsonUtility.FromJson<PlayerInfo>(dataJSon);
            return pl;
        }
    }
    [Serializable]
    public struct Options
    {
        public float MusicVolume;
        public float SoundVolume;
    }
}

