using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void PlayButton()
    {
        Close(0);
        UIManager.GetInstance.OpenUI<CanvasGamePlay>();
    }

    public void SettingsButton()
    {
        UIManager.GetInstance.OpenUI<CanvasSettings>().SetState(this);
    }
}
