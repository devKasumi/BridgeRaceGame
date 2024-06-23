using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{

    public void OnEnter(Bot bot)
    {
        bot.ChangeAnimation(Constants.ANIMATION_RUN);
    }

    public void OnExecute(Bot bot)
    {
        bot.SetTarget(bot.GetTargetBrickPosition());

        if (bot.BuildBridge())
        {
            bot.ChangeState(new BuildState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
