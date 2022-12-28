using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameText : MonoBehaviour
{
    [SerializeField] private TextMeshPro playerName;
    [SerializeField] private bool isPlayer;
    private GameObject cam;

    [SerializeField] private List<string> name;
    public string Name;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        Name = playerName.text = isPlayer ? PlayerStats.instance.playerName : name[Random.Range(0, name.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer) return;
        transform.LookAt(cam.transform);
    }
    
    
}
