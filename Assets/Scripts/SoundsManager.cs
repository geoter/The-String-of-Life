using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusicSource;
    [SerializeField] private AudioSource _effectsAudioSource;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip swipeClickSound;
    private void Start()
    {
        _backgroundMusicSource.loop = true;
        _backgroundMusicSource.clip = backgroundMusic;
        _backgroundMusicSource.volume = 0.4f;
        _backgroundMusicSource.Play();
    }

    public void cardSwiped()
    {
        _effectsAudioSource.PlayOneShot(swipeClickSound, 1);
    }
}
