﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionFader : ScreenFader
{
    [SerializeField]
    private float _lifetime = 1f;

    [SerializeField]
    private float _delay = 0.3f;
    public float Delay { get => _delay;}

    protected void Awake()
    {
        _lifetime = Mathf.Clamp(_lifetime, FadeOnDuration + FadeOffDuration + _delay, 10f);
    }

    private IEnumerator PlayRoutine()
    {
        SetAlpha(_clearAlpha);
        yield return new WaitForSeconds(_delay);

        FadeOn();
        float onTime = _lifetime - (FadeOffDuration + _delay);

        yield return new WaitForSeconds(onTime);

        FadeOff();
        Object.Destroy(gameObject, FadeOffDuration);
    }

    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    public static void PlayTransition(TransitionFader transitionFader)
    {
        if(transitionFader != null)
        {
            TransitionFader instance = Instantiate(transitionFader, Vector3.zero, Quaternion.identity);
            instance.Play();
        }
    }

}
