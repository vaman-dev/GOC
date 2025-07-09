using UnityEngine;
using TMPro;

public class GameOutCanvas : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Movement movement;
    [SerializeField] private ScorBoard scorBoard;
    [SerializeField] private TextMeshProUGUI finalCoinCountText;

    void Start()
    {
        if (gameOverPanel == null)
        {
            Debug.LogError("GameOutCanvas: Game Over Panel is not assigned.");
            return;
        }

        if (movement == null)
        {
            Debug.LogError("GameOutCanvas: Movement script is not assigned.");
            return;
        }

        if (scorBoard == null)
        {
            Debug.LogError("GameOutCanvas: ScorBoard script is not assigned.");
            return;
        }

        if (finalCoinCountText == null)
        {
            Debug.LogError("GameOutCanvas: Final Coin Count Text is not assigned.");
            return;
        }

        gameOverPanel.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShowGameOverPanel();
        }
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        movement.enabled = false;
        finalCoinCountText.text = scorBoard.CoinCount.ToString();
        Debug.Log("Game Over! Panel displayed.");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        gameOverPanel.SetActive(false);
        Debug.Log("Game Restarted!");
    }
}