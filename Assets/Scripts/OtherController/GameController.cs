using UnityEngine;

public static class GameController
{
    public static void GoToMenu()
    {
        Application.LoadLevel("Menu");
    }
    public static void LoadLevelSnake()
    {
        Application.LoadLevel("StartGame");
	}
    public static void LoadLevelSnakeThirdPerson()
    {
        Application.LoadLevel("StartGameThirdPerson");
    }
    public static void ExitGame()
    {
        Application.Quit();
    }
}
