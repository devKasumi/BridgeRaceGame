using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildState : IState
{

    public void OnEnter(Bot bot)
    {
        bot.SetRandomResetPoint();
    }

    public void OnExecute(Bot bot)
    {
        if (bot.IsReachTarget())
        {
            Debug.Log("reach target!!!!!");
            bot.SetFinalTarget();

        }
    }

    public void OnExit(Bot bot)
    {

    }
}
