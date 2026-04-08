using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public static bool isGameWon;
    public GameObject gameOverScreen;
    public GameObject gameWonScreen;
    private void Awake()
    {
        isGameOver = false;
        isGameWon = false;
        gameOverScreen.SetActive(false);
        gameWonScreen.SetActive(false);
    }
    private void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
        }
        if (isGameWon)
        {
            gameWonScreen.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
