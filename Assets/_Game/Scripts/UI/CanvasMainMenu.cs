using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void PlayButton()
    {
        Close(0);
        UIManager.GetInstance.OpenUI<CanvasGamePlay>();
        GameManager.GetInstance.UpdateGameState(GameState.GamePlay);
        List<Bot> bots = LevelManager.GetInstance.GetCurrentLevel().GetBots();
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(new PatrolState());
        } 
    }

    public void SettingsButton()
    {
        UIManager.GetInstance.OpenUI<CanvasSettings>().SetState(this);
        GameManager.GetInstance.UpdateGameState(GameState.Setting);
    }
}
