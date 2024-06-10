using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : IState
{

    public void OnEnter(Bot bot)
    {
        //maxCollectedBrick
        bot.SetFinalTarget();
    }

    public void OnExecute(Bot bot)
    {
        bot.SetFinalTarget();
    }

    public void OnExit(Bot bot)
    {

    }
}
