using DG.Tweening;
using TMPro;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public SoundEffectSO sfx;

    public TextMeshProUGUI txt;

    private void Start()
    {
        txt.DOText("Find the blocks!", 5, true,ScrambleMode.Lowercase);
    }
}
