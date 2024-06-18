using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildState : IState
{

    public void OnEnter(Bot bot)
    {
        bot.ChangeAnimation(Constants.ANIMATION_RUN);
        bot.SetRandomResetPoint();
    }

    public void OnExecute(Bot bot)
    {
        if (bot.IsReachTarget())
        {
            Debug.Log("reach target!!!!!");
            bot.SetFinalTarget();
            //bot.ChangeAnimation(Constants.ANIMATION_RUN);
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
