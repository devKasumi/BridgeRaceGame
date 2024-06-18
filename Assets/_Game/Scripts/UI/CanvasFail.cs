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
        Close(0);
        LevelManager.GetInstance.OnRetryLevel();
        UIManager.GetInstance.OpenUI<CanvasGamePlay>();
        GameManager.GetInstance.UpdateGameState(GameState.GamePlay);
    }
}
