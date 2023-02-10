using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
  public enum FilterType { Simple, Ridged };
  public FilterType filterType;

  public SimpleNoiseSettings simpleNoiseSettings;
  public RidgedNoiseSettings ridgedNoiseSettings;

  [Serializable]
  public class SimpleNoiseSettings
  {
    public float strength = 1;
    [Range(1, 8)]
    public int numLayers = 1;
    public float baseRoughness = 1;
    public float roughness = 2;
    public float persistence = 0.5f;
    public Vector3 center;
    public float minValue;
  }

  [Serializable]
  public class RidgedNoiseSettings : SimpleNoiseSettings
  {
    public float weightMultiplier = .8f;
  }
}
