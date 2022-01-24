using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    private enum SoundState
    {
        Reset,
        Played
    }
    
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;
    private SoundsManager _soundsManager;
    private SoundState _soundState = SoundState.Reset;
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _soundsManager = FindObjectOfType<SoundsManager>();
    }

    private void FixedUpdate()
    {
        //Checking the side
        var cardPosition = transform.position;
        if (cardPosition.x > 2)  //Right side
        {
            PlaySoundIfNeeded();
            _spriteRenderer.color = Color.green;
            if (!Input.GetMouseButton(0))
            {
                
            }
        } else if (cardPosition.x < -2)  //Left side
        {
            PlaySoundIfNeeded();
            _spriteRenderer.color = Color.red;
            if (!Input.GetMouseButton(0))
            {
                
            }
        }
        else
        {
            _soundState = SoundState.Reset;
            _spriteRenderer.color = Color.white;
        }
    }

    private void PlaySoundIfNeeded()
    {
        if (_soundState == SoundState.Reset)
        {
            _soundState = SoundState.Played;
            _soundsManager.cardSwiped();
        }
    }
}
