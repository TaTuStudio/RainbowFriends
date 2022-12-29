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

    Vector3 lookOffset = new(0f, 180f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        if (Camera.main != null) cam = Camera.main.gameObject;

        Name = playerName.text = isPlayer ? PlayerStats.instance.playerName : name[Random.Range(0, name.Count)];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isPlayer) return;

        Quaternion lookAt = Quaternion.LookRotation(cam.transform.position - transform.position);
        Quaternion correction = Quaternion.Euler(lookOffset);

        transform.rotation = lookAt * correction;
    }
    
    
}
