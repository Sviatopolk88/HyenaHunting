using UnityEngine;
using UnityEngine.SceneManagement;

public class ReGame : MonoBehaviour
{
    public void RetryGame()
    {
        GameController.Players.Clear();
        GameController.Points.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
