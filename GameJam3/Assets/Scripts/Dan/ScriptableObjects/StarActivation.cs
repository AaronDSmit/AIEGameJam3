﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Star Activation", menuName = "Star Activation", order = 1)]
public class StarActivation : ScriptableObject {
    [SerializeField] private float scaleTime;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float borderFadeTime;
    [SerializeField] private float increaseScale;
    [SerializeField] private float increaseScaleTime;
    [SerializeField] private AnimationCurve increaseCurve;
    [SerializeField] private float glowFadeTime;

    public float ScaleTime { get { return scaleTime; } }
    public AnimationCurve ScaleCurve { get { return scaleCurve; } }
    public float BorderFadeTime { get { return borderFadeTime; } }
    public float IncreaseScale { get { return increaseScale; } }
    public float IncreaseScaleTime { get { return increaseScaleTime; } }
    public AnimationCurve IncreaseCurve { get { return increaseCurve; } }
    public float GlowFadeTime { get { return glowFadeTime; } }

}
