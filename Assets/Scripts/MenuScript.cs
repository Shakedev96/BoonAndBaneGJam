using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("NIK_Level1_GREYBOX"); // Replace with your actual scene name
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed"); // Only visible in the editor
    }
}