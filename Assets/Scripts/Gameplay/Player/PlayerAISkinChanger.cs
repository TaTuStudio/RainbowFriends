using UnityEngine;

public class PlayerAISkinChanger : MonoBehaviour
{
    string currentSKin = "";

    public string playerSkinMat = "";
    public string boxSkinMat = "";

    public SkinnedMeshRenderer playerSkinnedMesh;
    public SkinnedMeshRenderer boxSkinnedMesh;
    public SkinnedMeshRenderer holdBoxSkinnedMesh;

    public bool change;

    private void OnEnable()
    {

    }

    private void Start()
    {
        if (currentSKin != PlayerStats.instance.currentPlayerSkinID)
        {
            currentSKin = PlayerStats.instance.currentPlayerSkinID;

            change = true;
        }
    }

    private void Update()
    {
        if (change)
        {
            change = false;

            _ChangeSkin();
        }
    }

    void _ChangeSkin()
    {
        int ranIndex = Random.Range(0, PlayerManager.instance.playerSkinScriptObj.playerSkins.Count);

        PlayerSkinScriptObj.Skin2DInfo skinInfo = PlayerManager.instance.playerSkinScriptObj.playerSkins[ranIndex];

        PlayerManager.instance.playerSkinScriptObj._Change(ref playerSkinnedMesh, playerSkinMat, boxSkinMat, skinInfo);
        PlayerManager.instance.playerSkinScriptObj._Change(ref boxSkinnedMesh, playerSkinMat, boxSkinMat, skinInfo);
        PlayerManager.instance.playerSkinScriptObj._Change(ref holdBoxSkinnedMesh, playerSkinMat, boxSkinMat, skinInfo);
    }
}
