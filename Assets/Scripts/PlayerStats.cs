using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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

    public bool noAd = false;

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
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
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


            ////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////
            ///
            //Convert json Player Unlocked Data
            if(json["playerUnlockedSkinList"] != null)
            {
                List<string> tempPlayerUnlockedDataList = new List<string>();
                for (int i = 0; i < json["playerUnlockedSkinList"].count; i++)
                {
                    JSONObject playerUnlockedDataJson = json["playerUnlockedSkinList"][i];

                    string idStr = playerUnlockedDataJson.ToString().Replace("\"", "");

                    tempPlayerUnlockedDataList.Add(idStr);
                }

                playerUnlockedSkinList = tempPlayerUnlockedDataList;
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


        //Add unlocked player data list to json file
        JSONObject playerUnlockedSkinDatasArr = new JSONObject();
        foreach (string unlockedID in playerUnlockedSkinList)
        {
            playerUnlockedSkinDatasArr.Add(unlockedID);
        }

        jsonData.AddField("playerUnlockedSkinList", playerUnlockedSkinDatasArr);

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
