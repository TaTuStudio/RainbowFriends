using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    float speedNormalize;

    public SoundEffectSO footStepFX;
    float curFootStepDelay;

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
        if (PlayerStats.instance.toggleSfx == false)
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
