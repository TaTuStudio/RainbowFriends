using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    float speedNormalize = 0f;

    public SoundEffectSO footStepFX;
    float curFootStepDelay = 0f;

    private void OnEnable()
    {
        speedNormalize = 0f;
        curFootStepDelay = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _Step();
    }

    public void _SetSpeedNormalize(float speedN)
    {
        speedNormalize = speedN;
    }

    void _Step()
    {
        if (GameplayUI.instance.settingsUI.sound == false)
        {
            return;
        }

        if (curFootStepDelay >= 0f)
        {
            curFootStepDelay -= Time.deltaTime;
        }

        if (curFootStepDelay < 0f && speedNormalize > 0.5f)
        {
            AudioSource audioSource = footStepFX.Play(gameObject);

            curFootStepDelay = audioSource.clip.length / audioSource.pitch;
        }
    }
}
