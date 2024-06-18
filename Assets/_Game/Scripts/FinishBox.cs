using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER))
        {
            UIManager.GetInstance.CloseAll();
            UIManager.GetInstance.OpenUI<CanvasVictory>();
            GameManager.GetInstance.UpdateGameState(GameState.Finish);
        }
        else if (other.CompareTag(Constants.TAG_BOT))
        {
            UIManager.GetInstance.CloseAll();
            UIManager.GetInstance.OpenUI<CanvasFail>();
            GameManager.GetInstance.UpdateGameState(GameState.Finish);
        }
    }
}
