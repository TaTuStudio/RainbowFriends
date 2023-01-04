using System.Collections;
using System.Collections.Generic;
using Lofelt.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private HapticClip btn;

    [SerializeField] private SoundEffectSO btnSound;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ClickBtn);
    }

    // Update is called once per frame
    void ClickBtn()
    {
        if (!PlayerStats.instance.toggleSfx) return;
        HapticController.Play(btn);
        btnSound.Play();
    }
}
