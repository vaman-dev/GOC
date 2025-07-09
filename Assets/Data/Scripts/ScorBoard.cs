using UnityEngine;
using TMPro;

public class ScorBoard : MonoBehaviour
{
    private int coinCount = 0;
    public int finalCoinCount = 0;

    [SerializeField] private TextMeshProUGUI coinTMPText;

    // Public property to access coinCount
    public int CoinCount => coinCount;

    void Start()
    {
        coinCount = 0;
        UpdateCoinText();
    }

    public void AddCoin()
    {
        coinCount++;
        finalCoinCount = coinCount;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        if (coinTMPText != null)
        {
            coinTMPText.text = coinCount.ToString();
        }
    }
}