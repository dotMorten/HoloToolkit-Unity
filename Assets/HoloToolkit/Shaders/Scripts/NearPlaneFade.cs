﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class NearPlaneFade : MonoBehaviour
{
    public float FadeDistanceStart = 0.85f;
    public float FadeDistanceEnd = 0.5f;

    public bool NearPlaneFadeOn = true;

    private const string FadeKeywordOn = "_NEAR_PLANE_FADE_ON";

    private int fadeDistancePropertyID;

    void Start()
    {
        fadeDistancePropertyID = Shader.PropertyToID("_NearPlaneFadeDistance");
        UpdateShaderParams();
    }

    void Update()
    {
    }

    private void OnValidate()
    {
        FadeDistanceStart = Mathf.Max(FadeDistanceStart, 0);
        FadeDistanceEnd = Mathf.Max(FadeDistanceEnd, 0);
        FadeDistanceStart = Mathf.Max(FadeDistanceStart, FadeDistanceEnd);

        UpdateShaderParams();
    }

    private void UpdateShaderParams()
    {
        float rangeInverse = 1.0f / (FadeDistanceStart - FadeDistanceEnd);
        var fadeDist = new Vector4(-FadeDistanceEnd * rangeInverse, rangeInverse, 0, 0);

        Shader.SetGlobalVector(fadeDistancePropertyID, fadeDist);

        if (NearPlaneFadeOn)
        {
            Shader.EnableKeyword(FadeKeywordOn);
        }
        else
        {
            Shader.DisableKeyword(FadeKeywordOn);
        }
    }
}