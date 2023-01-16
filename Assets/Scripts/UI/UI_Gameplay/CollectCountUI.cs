using TMPro;
using UnityEngine;

public class CollectCountUI : MonoBehaviour
{
    public TextMeshProUGUI countText;

    public void _SetCount(int curCount, int totalCount)
    {
        countText.text = "" + curCount.ToString("D2") + "/" + totalCount.ToString("D2");
    }
}
