using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public static bool isDisableSmoothCamera = false;
    public static bool isDisableWaterWaves = false;
    public static float cameraSize = 10f;
    
    public static void Load()
    {
        isDisableSmoothCamera = PlayerPrefs.GetInt("isDisableSmoothCamera", 0) == 1;
        isDisableWaterWaves = PlayerPrefs.GetInt("isDisableWaterWaves", 0) == 1;
        cameraSize = PlayerPrefs.GetFloat("cameraSize", 10f);
    }

    public static void Save()
    {
        PlayerPrefs.SetInt("isDisableSmoothCamera", isDisableSmoothCamera ? 1 : 0);
        PlayerPrefs.SetInt("isDisableWaterWaves", isDisableWaterWaves ? 1 : 0);
        PlayerPrefs.SetFloat("cameraSize", cameraSize);
    }
}
