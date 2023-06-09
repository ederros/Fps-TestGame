using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Statistic
{
    public enum events{
        OnValueChanged
    }
    [System.NonSerialized]
    public SingletoneEventListener<events> eventListener = new SingletoneEventListener<events>();
    public int totalWins = 0;
    public int totalLooses = 0;

    
    static Statistic instance;
    public static Statistic Instance{
        get{
            if(instance == null&&!Load()) instance = new Statistic();
            return instance;
        }
    }
    public static void Save(){
        BinaryFormatter bf = new BinaryFormatter();
        using(FileStream fs = new FileStream("Statistic.sav",FileMode.Create)){
            bf.Serialize(fs, Instance);
        }
        
    }
    static bool Load(){
        BinaryFormatter bf = new BinaryFormatter();

        if(!File.Exists("Statistic.sav")) return false;
        using(FileStream fs = new FileStream("Statistic.sav",FileMode.Open)){
            
            instance = (Statistic)bf.Deserialize(fs);
        }
        return true;
    }
}
