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
        Close(0);
        UIManager.GetInstance.CloseAll();
        LevelManager.GetInstance.GetPlayer().ClearBrick();
        LevelManager.GetInstance.GetPlayer().OnInit();
        LevelManager.GetInstance.ResetPool();
        LevelManager.GetInstance.OnLoadNextLevel();
        UIManager.GetInstance.OpenUI<CanvasMainMenu>();
        GameManager.GetInstance.UpdateGameState(GameState.MainMenu);
    }

    public void RetryButton()
    {
        Close(0);
        UIManager.GetInstance.CloseAll();
        LevelManager.GetInstance.GetPlayer().ClearBrick();
        LevelManager.GetInstance.GetPlayer().OnInit();
        LevelManager.GetInstance.ResetPool();
        LevelManager.GetInstance.OnRetryLevel();
        UIManager.GetInstance.OpenUI<CanvasGamePlay>();
        GameManager.GetInstance.UpdateGameState(GameState.GamePlay);
    }
}
