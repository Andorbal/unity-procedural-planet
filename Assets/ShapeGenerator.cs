using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
  ShapeSettings settings;
  INoiseFilter[] noiseFilters;
  public MinMax elevationMinMax;

  public void UpdateSettings(ShapeSettings settings)
  {
    this.elevationMinMax = new MinMax();
    this.settings = settings;
    noiseFilters = new INoiseFilter[settings.noiseLayers.Length];

    for (var i = 0; i < noiseFilters.Length; i += 1)
    {
      noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(settings.noiseLayers[i].noiseSettings);
    }
  }

  public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
  {
    float firstLayerValue = 0;
    float elevation = 0;

    if (noiseFilters.Length > 0)
    {
      firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
      if (settings.noiseLayers[0].enabled)
      {
        elevation = firstLayerValue;
      }
    }

    for (var i = 0; i < noiseFilters.Length; i += 1)
    {
      if (settings.noiseLayers[i].enabled)
      {
        float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;
        elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
      }
    }

    elevation = settings.planetRadius * (1 + elevation);
    this.elevationMinMax.AddValue(elevation);
    return pointOnUnitSphere * elevation;
  }
}
