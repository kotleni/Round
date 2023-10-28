using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OpenGame()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }       
}