using TMPro;
using UnityEngine;

public class PlayerCountUI : MonoBehaviour
{
    public TextMeshProUGUI countText;

    public void _SetCount(int current, int total)
    {
        countText.text = current.ToString("D2") + "/" + total.ToString("D2");
    }
}
