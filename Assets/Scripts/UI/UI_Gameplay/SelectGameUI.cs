using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectGameUI : MonoBehaviour
{
    string selected = "";

    [SerializeField] private TextMeshProUGUI[] win;
    [SerializeField] private TextMeshProUGUI[] lose;
    [SerializeField] private PlayerStats stats;
    
    private void OnEnable()
    {
        selected = "";
        
        win[0].text = "Survive: " + stats.level1win;
        win[1].text = "Survive: " + stats.level2win;
        win[2].text = "Survive: " + stats.level3win;
        win[3].text = "Survive: " + stats.level4win;
        win[4].text = "Survive: " + stats.level5win;
        
        lose[0].text = "Death: " + stats.level1lose;
        lose[1].text = "Death: " + stats.level2lose;
        lose[2].text = "Death: " + stats.level3lose;
        lose[3].text = "Death: " + stats.level4lose;
        lose[4].text = "Death: " + stats.level5lose;

    }

    public void _PlayButton(string sceneName)
    {
        MapManager.instance.selectedMap = sceneName;

        MapManager.instance._SpawnMap();
    }
    
    
}
