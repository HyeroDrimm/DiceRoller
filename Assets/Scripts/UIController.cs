using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GlobalChannelSO globalChannel;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text totalText;
    private int totalNum = 0;

    private void Awake()
    {
        globalChannel.onNumberLanded += UpdateScore;
        globalChannel.onDiceThrown += () => resultText.text = "?";
    }

    private void UpdateScore(int number)
    {
        totalNum += number;

        resultText.text = number.ToString();
        totalText.text = totalNum.ToString();
    }
}
