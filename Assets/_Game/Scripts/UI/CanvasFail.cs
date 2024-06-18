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
        UIManager.GetInstance.CloseAll();
        //TODO fix: 
        LevelManager.GetInstance.GetPlayer().ClearBrick();
        LevelManager.GetInstance.GetPlayer().OnInit();
        LevelManager.GetInstance.OnRetryLevel();
        UIManager.GetInstance.OpenUI<CanvasGamePlay>();
        GameManager.GetInstance.UpdateGameState(GameState.GamePlay);
    }
}
