using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;
using Defective.JSON;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public PlayerSkinScriptObj playerSkinScriptObj;

    static string playerDataFileName = "player_data.json";
    public bool load = false;
    public bool save = false;

    public string currentPlayerSkinID = "";

    public int passedLvl = 0;

    public int coin = 0;

    public event Action<int> OnCoinChange;

    public bool noAd = false;

    public string playerName;
    public bool toggleSfx;
    public bool toggleBgm;

    public int level1win;
    public int level1lose;
    public int level2win;
    public int level2lose;
    public int level3win;
    public int level3lose;
    public int level4win;
    public int level4lose;
    public int level5win;
    public int level5lose;

    public List<string> mapUnlockedList = new List<string>();

    public List<string> playerUnlockedSkinList = new List<string>();

    private void Awake()
    {
        _MakeSingleton();

        _Load();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (load)
        {
            load = false;

            _Load();
        }

        if (save)
        {
            save = false;

            _Save();
        }
    }

    public void _AddCoin(int addNum)
    {
        coin += addNum;

        OnCoinChange?.Invoke(coin);

        save = true;
    }

    public void _SubCoin(int subNum)
    {
        coin -= subNum;
        
        if(subNum < 0)
        {
            coin = 0;
        }
        
        OnCoinChange?.Invoke(coin);

        save = true;
    }

    void _CheckDefaultValues()
    {
        if(currentPlayerSkinID == "")
        {
            currentPlayerSkinID = playerSkinScriptObj.playerSkins[0].skinID;
        }

        if(playerUnlockedSkinList.Contains(currentPlayerSkinID) == false)
        {
            playerUnlockedSkinList.Add(currentPlayerSkinID);
        }
    }

    void _MakeSingleton()
    {
        PlayerStats[] playerStats = FindObjectsOfType<PlayerStats>();

        if (playerStats.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    void _Load()
    {
        string filePath = Path.Combine(Application.persistentDataPath, playerDataFileName);

        Debug.Log("Load path = " + filePath);

        if (File.Exists(filePath))
        {
            string dataText = File.ReadAllText(filePath);

            JSONObject json = new JSONObject(dataText);

            if (json["currentPlayerSkinID"] != null)
                currentPlayerSkinID = json["currentPlayerSkinID"].ToString().Replace("\"", "");

            if (json["passedLvl"] != null)
                int.TryParse(json["passedLvl"].ToString().Replace("\"", ""), out passedLvl);

            if (json["coin"] != null)
                int.TryParse(json["coin"].ToString().Replace("\"", ""), out coin);

            if (json["noAd"] != null)
                bool.TryParse(json["noAd"].ToString().Replace("\"", ""), out noAd);
            
            if (json["playerName"] != null)
                playerName = json["playerName"].stringValue;
            
            if (json["toggleSfx"] != null)
                bool.TryParse(json["toggleSfx"].ToString().Replace("\"", ""), out toggleSfx);
            
            if (json["toggleBgm"] != null)
                bool.TryParse(json["toggleBgm"].ToString().Replace("\"", ""), out toggleBgm);

            if (json["level1win"] != null)
                int.TryParse(json["level1win"].ToString().Replace("\"", ""), out level1win);

            if (json["level1lose"] != null)
                int.TryParse(json["level1lose"].ToString().Replace("\"", ""), out level1lose);

            if (json["level2win"] != null)
                int.TryParse(json["level2win"].ToString().Replace("\"", ""), out level2win);

            if (json["level2lose"] != null)
                int.TryParse(json["level2lose"].ToString().Replace("\"", ""), out level2lose);

            if (json["level3win"] != null)
                int.TryParse(json["level3win"].ToString().Replace("\"", ""), out level3win);

            if (json["level3lose"] != null)
                int.TryParse(json["level3lose"].ToString().Replace("\"", ""), out level3lose);

            if (json["level4win"] != null)
                int.TryParse(json["level4win"].ToString().Replace("\"", ""), out level4win);

            if (json["level4lose"] != null)
                int.TryParse(json["level4lose"].ToString().Replace("\"", ""), out level4lose);
            
            if (json["level5win"] != null)
                int.TryParse(json["level5win"].ToString().Replace("\"", ""), out level5win);

            if (json["level5lose"] != null)
                int.TryParse(json["level5lose"].ToString().Replace("\"", ""), out level5lose);


            ////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////
            ///
            //Convert json Player Unlocked Data
            if(json["playerUnlockedSkinList"] != null)
            {
                List<string> tempPlayerUnlockedDataList = new List<string>();
                for (int i = 0; i < json["playerUnlockedSkinList"].count; i++)
                {
                    JSONObject mapUnlockedDataJson = json["playerUnlockedSkinList"][i];

                    string idStr = mapUnlockedDataJson.ToString().Replace("\"", "");

                    tempPlayerUnlockedDataList.Add(idStr);
                }

                playerUnlockedSkinList = tempPlayerUnlockedDataList;
            }

            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ///

            ////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////
            ///
            //Convert json map Unlocked Data
            if (json["playerUnlockedSkinList"] != null)
            {
                List<string> tempMapUnlockedDataList = new List<string>();
                for (int i = 0; i < json["mapUnlockedList"].count; i++)
                {
                    JSONObject playerUnlockedDataJson = json["mapUnlockedList"][i];

                    string idStr = playerUnlockedDataJson.ToString().Replace("\"", "");

                    tempMapUnlockedDataList.Add(idStr);
                }

                mapUnlockedList = tempMapUnlockedDataList;
            }

            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ///
            _CheckDefaultValues();
            //////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////
            /////////////////////////

            Debug.Log("PlayerStats load success");
        }
        else
        {
            Debug.Log("Load path does not exist");

            _Save();
        }
    }

    void _Save()
    {
        _CheckDefaultValues();

        string filePath = Path.Combine(Application.persistentDataPath, playerDataFileName);

        Debug.Log("Save path = " + filePath);

        string jsonString = "";

        JSONObject jsonData = new JSONObject();

        jsonData.AddField("currentPlayerSkinID", currentPlayerSkinID);

        jsonData.AddField("passedLvl", passedLvl);

        jsonData.AddField("coin", coin);

        jsonData.AddField("noAd", noAd);

        jsonData.AddField("playerName", playerName);
        jsonData.AddField("toggleSfx", toggleSfx);
        jsonData.AddField("toggleBgm", toggleBgm);

        jsonData.AddField("level1win", level1win);
        jsonData.AddField("level1lose", level1lose);
        jsonData.AddField("level2win", level2win);
        jsonData.AddField("level2lose", level2lose);
        jsonData.AddField("level3win", level3win);
        jsonData.AddField("level3lose", level3lose);
        jsonData.AddField("level4win", level4win);
        jsonData.AddField("level4lose", level4lose);
        jsonData.AddField("level5win", level5win);
        jsonData.AddField("level5lose", level5lose);


        //Add unlocked player data list to json file
        JSONObject playerUnlockedSkinDatasArr = new JSONObject();
        foreach (string unlockedID in CollectionMarshal.AsSpan(playerUnlockedSkinList))
        {
            playerUnlockedSkinDatasArr.Add(unlockedID);
        }

        jsonData.AddField("playerUnlockedSkinList", playerUnlockedSkinDatasArr);

        /////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ///

        //Add unlocked map data list to json file
        JSONObject mapUnlockedSkinDatasArr = new JSONObject();
        foreach (string unlockedID in CollectionMarshal.AsSpan(mapUnlockedList))
        {
            mapUnlockedSkinDatasArr.Add(unlockedID);
        }

        jsonData.AddField("mapUnlockedList", mapUnlockedSkinDatasArr);

        /////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ///

        jsonString = jsonData.ToString();

        Debug.Log("save jsonString = " + jsonString);

        try
        {
            File.WriteAllText(filePath, jsonString);

            Debug.Log("PlayerStats save success");
        }
        catch (FileLoadException e)
        {
            Debug.Log("PlayerStats save error");
        }
    }
}
