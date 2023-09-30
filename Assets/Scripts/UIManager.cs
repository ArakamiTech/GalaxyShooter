using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _lives;
    [SerializeField]
    private Image _livesImageDisplay;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private GameObject _titleScreen;
    private int _score = 0;

    public void UpdateLives(int currentLives)
    {
        _livesImageDisplay.sprite = _lives[currentLives];
    }

    public void UpdateScore()
    {
        _score += 10;
        _scoreText.text = "Score: " + _score;
    }

    public void ShowTitleScreen()
    {
        _titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        _titleScreen.SetActive(false);
        _scoreText.text = "Score: ";
    }
}
