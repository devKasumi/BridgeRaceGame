using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSettings : UICanvas
{
    //[SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject closeButton;

    public void SetState(UICanvas canvas)
    {
        //for (int i = 0; i < buttons.Length; i++)
        //{
        //    buttons[i].gameObject.SetActive(false);
        //}
        mainMenuButton.SetActive(false);
        continueButton.SetActive(false);
        closeButton.SetActive(false);

        if (canvas is CanvasMainMenu)
        {
            //buttons[2].gameObject.SetActive(true);
            closeButton.SetActive(true);
        }
        else if (canvas is CanvasGamePlay)
        {
            //buttons[0].gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
            //buttons[1].gameObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
        }
    }

    public void MainMenuButton()
    {
        UIManager.GetInstance.CloseAll();
        UIManager.GetInstance.OpenUI<CanvasMainMenu>();
        GameManager.GetInstance.UpdateGameState(GameState.MainMenu);
    }

    public void ContinueButton()
    {
        //UIManager.GetInstance.CloseAll();
        //UIManager.GetInstance.OpenUI<CanvasGamePlay>();
        Close(0);
        GameManager.GetInstance.UpdateGameState(GameState.GamePlay);
    }

    public void CloseButton()
    {
        //UIManager.GetInstance.CloseAll();
        //UIManager.GetInstance.OpenUI<CanvasMainMenu>();
        Close(0);
        GameManager.GetInstance.UpdateGameState(GameState.MainMenu);
    }
}
