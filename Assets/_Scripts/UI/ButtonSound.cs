using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private SoundEffectSO btnSound;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ClickBtn);
    }

    // Update is called once per frame
    private void ClickBtn()
    {
        if (!PlayerStats.instance.toggleSfx) return;
        //HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
        btnSound.Play();
    }
}
