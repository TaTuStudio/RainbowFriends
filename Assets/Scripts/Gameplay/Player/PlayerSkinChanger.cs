using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinChanger : MonoBehaviour
{
    string currentSKin = "";

    public string playerSkinMat = "";
    public string boxSkinMat = "";

    public SkinnedMeshRenderer playerSkinnedMesh;
    public SkinnedMeshRenderer boxSkinnedMesh;
    public SkinnedMeshRenderer holdBoxSkinnedMesh;

    public bool change = false;

    private void OnEnable()
    {
        if (currentSKin != PlayerStats.instance.currentPlayerSkinID)
        {
            currentSKin = PlayerStats.instance.currentPlayerSkinID;

            change = true;
        }
    }

    private void Update()
    {
        if(change)
        {
            change = false;

            _ChangeSkin();
        }
    }

    void _ChangeSkin()
    {
        PlayerSkinScriptObj.Skin2DInfo skinInfo = PlayerManager.instance.playerSkinScriptObj._GetSkinInfo(currentSKin);

        PlayerManager.instance.playerSkinScriptObj._Change(ref playerSkinnedMesh, playerSkinMat, boxSkinMat, skinInfo);
        PlayerManager.instance.playerSkinScriptObj._Change(ref boxSkinnedMesh, playerSkinMat, boxSkinMat, skinInfo);
        PlayerManager.instance.playerSkinScriptObj._Change(ref holdBoxSkinnedMesh, playerSkinMat, boxSkinMat, skinInfo);
    }


}
