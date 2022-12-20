using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Delivery/PlayerSkinDataObj", order = 1)]
public class PlayerSkinScriptObj : ScriptableObject
{
    [System.Serializable]
    public class Skin2DInfo
    {
        public string skinID = "";
        public string skinName = "";
        public Texture2D boxTex;
        public Texture2D playerTex;
        public Sprite skinAvatar;
    }

    public List<Skin2DInfo> playerSkins = new List<Skin2DInfo>();

    public Skin2DInfo _GetSkinInfo(string skinID)
    {
        foreach (Skin2DInfo s in playerSkins)
        {
            if (skinID == s.skinID)
            {
                return s;
            }
        }

        return null;
    }

    public void _Change(ref SkinnedMeshRenderer skinnedMesh, string playerSkinMatName, string boxSkinMatName, PlayerSkinScriptObj.Skin2DInfo info)
    {

        foreach (Material mat in skinnedMesh.materials)
        {
            Debug.Log("MatName = " + mat.name);

            if (mat.name.Contains(playerSkinMatName))
            {
                mat.SetTexture("_BaseMap", info.playerTex);
                //mat.mainTexture = info.playerTex;
            }
            else
            if (mat.name.Contains(boxSkinMatName))
            {
                mat.SetTexture("_BaseMap", info.boxTex);
                //mat.mainTexture = info.boxTex;
            }
        }
    }
}
