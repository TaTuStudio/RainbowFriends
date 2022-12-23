// using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "NewSoundEffect", menuName = "Audio/New Sound Effect")]
public class SoundEffectSO : ScriptableObject
{
    #region config

    private PlayerSettingsSO settings;
    private const float SemitonesToPitchConversionUnit = 1.05946f;

    // [Required] 
    public AudioClip[] clips;

    // [MinMaxSlider(0, 1)] [BoxGroup("config")]
    public Vector2 volume = new Vector2(1f, 1f);

    //Pitch / Semitones
    // [LabelWidth(100)] [HorizontalGroup("config/pitch")]
    public bool useSemitones;

    // [HideLabel]
    // [ShowIf("useSemitones")]
    // [HorizontalGroup("config/pitch")]
    // [MinMaxSlider(-10, 10)]
    // [OnValueChanged("SyncPitchAndSemitones")]
    public Vector2Int semitones = new Vector2Int(0, 0);

    // [HideLabel]
    // [HideIf("useSemitones")]
    // [MinMaxSlider(0, 3)]
    // [HorizontalGroup("config/pitch")]
    // [OnValueChanged("SyncPitchAndSemitones")]
    public Vector2 pitch = new Vector2(1, 1);

    // [BoxGroup("config")] 
    [SerializeField] private SoundClipPlayOrder playOrder;

    // [DisplayAsString] [BoxGroup("config")] 
    [SerializeField] private int playIndex = 0;
    
    [Range(0.0f, 1.0f)]
    [SerializeField] private float spatialBlend = 0;
    [Range(1.0f, 1000.0f)]
    [SerializeField] private float maxDistance = 1;
    #endregion

    #region PreviewCode

#if UNITY_EDITOR
    private AudioSource previewer;

    private void OnEnable()
    {
        Addressables.LoadAssetAsync<PlayerSettingsSO>("PlayerSettings").Completed += (_) => settings = _.Result;
        
        previewer = EditorUtility
            .CreateGameObjectWithHideFlags("AudioPreview", HideFlags.HideAndDontSave,
                typeof(AudioSource))
            .GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        DestroyImmediate(previewer.gameObject);
    }


    // [ButtonGroup("previewControls")]
    // [GUIColor(.3f, 1f, .3f)]
    // [Button(ButtonSizes.Gigantic)]
    private void PlayPreview()
    {
        Play(null, previewer);
    }

    // [ButtonGroup("previewControls")]
    // [GUIColor(1, .3f, .3f)]
    // [Button(ButtonSizes.Gigantic)]
    // [EnableIf("@previewer.isPlaying")]
    private void StopPreview()
    {
        previewer.Stop();
    }
#endif

    #endregion
    

    public void SyncPitchAndSemitones()
    {
        if (useSemitones)
        {
            pitch.x = Mathf.Pow(SemitonesToPitchConversionUnit, semitones.x);
            pitch.y = Mathf.Pow(SemitonesToPitchConversionUnit, semitones.y);
        }
        else
        {
            semitones.x = Mathf.RoundToInt(Mathf.Log10(pitch.x) / Mathf.Log10(SemitonesToPitchConversionUnit));
            semitones.y = Mathf.RoundToInt(Mathf.Log10(pitch.y) / Mathf.Log10(SemitonesToPitchConversionUnit));
        }
    }

    private AudioClip GetAudioClip()
    {
        // get current clip
        var _clip = clips[playIndex >= clips.Length ? 0 : playIndex];

        // find next clip
        playIndex = playOrder switch
        {
            SoundClipPlayOrder.InOrder => (playIndex + 1) % clips.Length,
            SoundClipPlayOrder.Random => Random.Range(0, clips.Length),
            SoundClipPlayOrder.Reverse => (playIndex + clips.Length - 1) % clips.Length,
            _ => playIndex
        };

        // return clip
        return _clip;
    }

    public AudioSource Play(GameObject parent = null, bool isBgm = false, AudioSource audioSourceParam = null)
    {
        if (clips.Length == 0)
        {
            Debug.LogError($"Missing sound clips for {name}");
            return null;
        }

        var _source = audioSourceParam;
        if (ReferenceEquals(_source, null))
        {
            var _obj = new GameObject("Sound", typeof(AudioSource));
            if (!ReferenceEquals(parent, null))
            {
                _obj.transform.parent = parent.transform;
            }
            _source = _obj.GetComponent<AudioSource>();
        }

        // set source config:
        _source.mute = isBgm ? settings.Bgm : settings.Sfx;
        _source.loop = isBgm;
        _source.rolloffMode = AudioRolloffMode.Linear;
        _source.spatialBlend = spatialBlend;
        _source.maxDistance = maxDistance;
        _source.clip = GetAudioClip();
        _source.volume = Random.Range(volume.x, volume.y);
        _source.pitch = useSemitones
            ? Mathf.Pow(SemitonesToPitchConversionUnit, Random.Range(semitones.x, semitones.y))
            : Random.Range(pitch.x, pitch.y);

        _source.Play();

#if UNITY_EDITOR
        if (_source != previewer)
        {
            Destroy(_source.gameObject, _source.clip.length / _source.pitch);
        }
#else
                Destroy(_source.gameObject, _source.clip.length / _source.pitch);
#endif
        return _source;
    }

    private enum SoundClipPlayOrder
    {
        Random = 0,
        InOrder = 1,
        Reverse = 2
    }
}