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
    public PlayerInfo(string name)
    {
        var path = Application.dataPath + "/PlayerInfo/" + name + ".dat";
        var playerFromFile = ReadFromFile(name);
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

    public PlayerInfo InitializeByName(string name)
    {
        Name = name;
        Level = 0;
        Exp = 0;
        GamesWin = 0;
        GamesLost = 0;
        ExpToNextLevel = 100;
        DeckName = "Default";
        Opt = new Options()
        {
            MusicVolume = 0.5f,
            SoundVolume = 0.5f
        };

        //var port = Resources.LoadAll<Sprite>("Leaders/")
        //PathToAvatar = port[UnityEngine.Random.Range(0, port.Length)];

        var path = Application.dataPath + "/PlayerInfo/" + name + ".dat";
        var fs = new FileStream(path, FileMode.CreateNew);
        fs.Close();

        return this;
    }

    public void SaveToFile(string name)
    {
        var path = Application.dataPath + "/PlayerInfo/" + name + ".dat";
        string data = JsonUtility.ToJson(this);
        //if (!File.Exists(Application.dataPath + "/PlayerInfo/PlayerInfo.dat"))
        //{
        //    File.Create(Application.dataPath + "/PlayerInfo/PlayerInfo.dat");
        //    File.WriteAllText(Application.dataPath + "/PlayerInfo/PlayerInfo.dat", data);

        //}
        //else
        File.WriteAllText(path, data);

    }
    public PlayerInfo ReadFromFile(string name)
    {
        var path = Application.dataPath + "/PlayerInfo/" + name + ".dat";
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

