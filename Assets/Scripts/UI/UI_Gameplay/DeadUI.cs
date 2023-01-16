using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeadUI : MonoBehaviour
{
    int rewardCoin = 100;

    public TextMeshProUGUI coinText;

    public SoundEffectSO failSound;
    
    private void OnEnable()
    {
        Time.timeScale = 0f;
        failSound.Play();

        coinText.text = "" + rewardCoin;
        coinText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void _MoreLifeAd()
    {
        Debug.Log("More life ad");

        _MoreLifeAdDone();
    }

    public void _MoreLifeAdDone()
    {
        Time.timeScale = 1f;

        PlayerController spawnedPlayer = PlayerManager.instance.spawnedPlayer;

        spawnedPlayer.setDefault = true;

        spawnedPlayer._Default();

        spawnedPlayer._SetNoDam(5f);

        GameController.instance.resetWinLose = true;

        gameObject.SetActive(false);
    }

    public void _PlayAgain()
    {
        Debug.Log("DeadUI Play again");

        GameController.instance._AddLoseArchivement();

        PlayerStats.instance._AddCoin(rewardCoin);

        MapManager.instance._SpawnMap();
    }
}
