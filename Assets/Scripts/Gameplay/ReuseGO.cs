using UnityEngine;

public class ReuseGO : MonoBehaviour
{
    public bool genID;
    public string itemID = "";

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying == false)
        {
            if (genID)
            {
                genID = false;
                itemID = gameObject.name;
            }
        }
    }
#endif
}
