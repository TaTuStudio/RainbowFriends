using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class VersionDisplay : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TMP_Text>().text = Application.version;
    }
}