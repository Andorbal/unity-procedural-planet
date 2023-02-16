using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgedNoiseFilter : INoiseFilter
{
  NoiseSettings.RidgedNoiseSettings settings;
  Noise noise = new Noise();

  public RidgedNoiseFilter(NoiseSettings.RidgedNoiseSettings settings)
  {
    this.settings = settings;
  }

  public float Evaluate(Vector3 point)
  {
    float noiseValue = 0;
    float frequency = settings.baseRoughness;
    float amplitude = 1;
    float weight = 1;

    for (var i = 0; i < settings.numLayers; i += 1)
    {
      float v = 1 - Math.Abs(noise.Evaluate(point * frequency + settings.center));
      v *= v;
      v *= weight;
      weight = Mathf.Clamp01(v * settings.weightMultiplier);
      noiseValue += v * amplitude;
      frequency *= settings.roughness;
      amplitude *= settings.persistence;
    }

    noiseValue = noiseValue - settings.minValue;
    return noiseValue * settings.strength;
  }
}
