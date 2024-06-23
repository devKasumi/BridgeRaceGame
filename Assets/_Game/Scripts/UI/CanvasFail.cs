using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasFail : UICanvas
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetBestScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void RetryButton()
    {
        UIManager.GetInstance.CloseAll();
        //TODO fix: 
        UIManager.GetInstance.OpenUI<CanvasMainMenu>();
        LevelManager.GetInstance.OnRetryLevel();
        GameManager.GetInstance.UpdateGameState(GameState.MainMenu);
    }
}
