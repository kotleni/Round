using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private void Start()
    {
        GameSettings.Load();
    }

    public void OpenGame()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Scenes/SettingsScene");
    }
}