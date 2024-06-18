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

    public void MainMenuButton()
    {
        Close(0);
        UIManager.GetInstance.OpenUI<CanvasMainMenu>();
        GameManager.GetInstance.UpdateGameState(GameState.MainMenu);
    }
}
