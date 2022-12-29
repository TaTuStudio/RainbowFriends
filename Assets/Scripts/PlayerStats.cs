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

    [System.Serializable]
    public class MapArchivements
    {
        public string mapSceneName = "";
        public int winCount = 0;
        public int loseCount = 0;
    }

    public PlayerSkinScriptObj playerSkinScriptObj;

    static string playerDataFileName = "player_data.json";
    public bool load = false;
    public bool save = false;

    public string currentPlayerSkinID = "";

    public int passedLvl = 0;

    public int coin = 0;

    public bool noAd = false;

    public string playerName;
    public bool toggleSfx;
    public bool toggleBgm;

    public List<int> mapUnlockedList = new List<int>();

    public List<string> playerUnlockedSkinList = new List<string>();

    public List<MapArchivements> mapArchivements = new List<MapArchivements>();

    private void Awake()
    {
        _MakeSingleton();

        _Load();
    }

    // Start is called before the first frame update
    void Start() { }

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

        save = true;
    }

    public void _SubCoin(int subNum)
    {
        coin -= subNum;

        if (subNum < 0)
        {
            coin = 0;
        }

        save = true;
    }

    void _CheckDefaultValues()
    {
        if (currentPlayerSkinID == "")
        {
            currentPlayerSkinID = playerSkinScriptObj.playerSkins[0].skinID;
        }

        if (playerUnlockedSkinList.Contains(currentPlayerSkinID) == false)
        {
            playerUnlockedSkinList.Add(currentPlayerSkinID);
        }

        if (mapUnlockedList.Count < 1)
        {
            mapUnlockedList.Add(0);
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


            ////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////
            ///
            //Convert json Player Unlocked Data
            if (json["playerUnlockedSkinList"] != null)
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
            if (json["mapUnlockedList"] != null)
            {
                List<int> tempMapUnlockedDataList = new List<int>();
                for (int i = 0; i < json["mapUnlockedList"].count; i++)
                {
                    int mapIndex = 0;

                    if (json["mapUnlockedList"][i] != null)
                        int.TryParse(
                            json["mapUnlockedList"][i].ToString().Replace("\"", ""),
                            out mapIndex
                        );

                    tempMapUnlockedDataList.Add(mapIndex);
                }

                mapUnlockedList = tempMapUnlockedDataList;
            }

            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            ///

            ////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////
            ///
            //Convert json map archivements Data
            if (json["mapArchivements"] != null)
            {
                List<MapArchivements> tempMapArchivementsDataList = new List<MapArchivements>();
                for (int i = 0; i < json["mapArchivements"].count; i++)
                {
                    JSONObject dataJson = json["mapArchivements"][i];

                    MapArchivements mapArchivements = new MapArchivements();

                    if (dataJson != null)
                    {
                        if(dataJson["mapSceneName"] != null)
                            mapArchivements.mapSceneName = dataJson["mapSceneName"].ToString().Replace("\"", "");

                        if (dataJson["winCount"] != null)
                            int.TryParse(dataJson["winCount"].ToString().Replace("\"", ""), out mapArchivements.winCount);

                        if (dataJson["loseCount"] != null)
                            int.TryParse(dataJson["loseCount"].ToString().Replace("\"", ""), out mapArchivements.loseCount);
                    }

                    tempMapArchivementsDataList.Add(mapArchivements);
                }

                mapArchivements = tempMapArchivementsDataList;
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
        foreach (int unlockedID in CollectionMarshal.AsSpan(mapUnlockedList))
        {
            mapUnlockedSkinDatasArr.Add(unlockedID);
        }

        jsonData.AddField("mapUnlockedList", mapUnlockedSkinDatasArr);

        /////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ///

        //Add map archivements data list to json file
        JSONObject mapArchivementsDatasArr = new JSONObject();
        foreach (MapArchivements mapArchivement in CollectionMarshal.AsSpan(mapArchivements))
        {
            JSONObject archiveData = new JSONObject();

            archiveData.AddField("mapSceneName", mapArchivement.mapSceneName);
            archiveData.AddField("winCount", mapArchivement.winCount);
            archiveData.AddField("loseCount", mapArchivement.loseCount);

            mapArchivementsDatasArr.Add(archiveData);
        }

        jsonData.AddField("mapArchivements", mapArchivementsDatasArr);

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
