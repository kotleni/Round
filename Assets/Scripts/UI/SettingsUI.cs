using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Toggle enableSmoothCameraToggle;
    [SerializeField] private Toggle enableWaterWavesToggle;
    [SerializeField] private Slider cameraSizeSlider;

    private void Start()
    {
        GameSettings.Load();

        // Load values
        enableSmoothCameraToggle.isOn = !GameSettings.isDisableSmoothCamera;
        enableWaterWavesToggle.isOn = !GameSettings.isDisableWaterWaves;
        cameraSizeSlider.value = GameSettings.cameraSize;
        
        // Set listeners
        enableSmoothCameraToggle.onValueChanged.AddListener(isEnable =>
        {
            GameSettings.isDisableSmoothCamera = !isEnable;
            GameSettings.Save();
        });
        enableWaterWavesToggle.onValueChanged.AddListener(isEnable =>
        {
            GameSettings.isDisableWaterWaves = !isEnable;
            GameSettings.Save();
        });
        cameraSizeSlider.onValueChanged.AddListener(value =>
        {
            GameSettings.cameraSize = value;
            GameSettings.Save();
        });
    }
    
    public void CloseSettings()
    {
        SceneManager.LoadScene("Scenes/MainMenuScene");
    }
}