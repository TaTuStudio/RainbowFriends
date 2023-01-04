using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSFX : MonoBehaviour
{
    public SoundEffectSO sound;

    public AudioSource source;

    public bool isStaticMonster;
    private void OnEnable()
    {
        sound.Play(gameObject, true, isStaticMonster, source);
    }
}
