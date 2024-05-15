
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using static UnityEngine.EventSystems.EventTrigger;

[System.Serializable]   
public struct Data
{
    public List<Example> history;
    [System.Serializable]
public struct Example
    {
      public string Expression;
      public string Answer;
      public Example(string expression,string answer)
        {
            Expression = expression;
            Answer = answer;
        }
    }
}

public static  class HistoryManager
{
    [SerializeField] private static string savePath;
    [SerializeField] private static string saveFileName = "data.json";
    public static Data data;
    public static void Init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            savePath = Path.Combine(Application.persistentDataPath, saveFileName);
#else
        savePath = Path.Combine(Application.dataPath, saveFileName);
#endif
    }
    public static void Save(Data.Example NewExample)
    {
        if (data.history == null) 
        {
            data.history=new(); 
        }
        data.history.Add(NewExample);
        string json = JsonUtility.ToJson(data, true);
        try
        {
            File.WriteAllText(savePath, json);
        }
        catch (Exception e)
        {
            Debug.Log("{GameLog} => [GameCore] - (<color=red>Error</color>) - SaveToFile -> " + e.Message);
        }
    }
    public static void  Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("{GameLog} => [GameCore] - LoadFromFile -> File Not Found!");
        }
        try
        {
            string json = File.ReadAllText(savePath);

            data= JsonUtility.FromJson<Data>(json);
        }
        catch (Exception e)
        {
            Debug.Log("{GameLog} - [GameCore] - (<color=red>Error</color>) - LoadFromFile -> " + e.Message);
        }
    }
}
