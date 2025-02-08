using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;  // Assign this in the Inspector

    private bool isGameOver = false;

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        gameOverUI.SetActive(true);  // Show Game Over screen
        Time.timeScale = 0f;  // Pause game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;  // Resume game speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("Game Closed"); // Only visible in the Unity editor
    }
}