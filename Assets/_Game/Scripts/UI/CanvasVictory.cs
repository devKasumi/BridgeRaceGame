using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetBestScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void NextButton()
    {
        UIManager.GetInstance.CloseAll();
        LevelManager.GetInstance.OnLoadNextLevel();
        UIManager.GetInstance.OpenUI<CanvasMainMenu>();
        GameManager.GetInstance.UpdateGameState(GameState.MainMenu);
    }

    public void RetryButton()
    {
        UIManager.GetInstance.CloseAll();
        LevelManager.GetInstance.OnRetryLevel();
        UIManager.GetInstance.OpenUI<CanvasMainMenu>();
        GameManager.GetInstance.UpdateGameState(GameState.MainMenu);
    }
}
