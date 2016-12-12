using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public enum GameTypes
    {
        Normal, Zen
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        PlayerPrefs.SetInt("GameType", (int)GameTypes.Normal);
    }

    public void StartZengame()
    {
        SceneManager.LoadScene("Game");
        PlayerPrefs.SetInt("GameType", (int)GameTypes.Zen);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
