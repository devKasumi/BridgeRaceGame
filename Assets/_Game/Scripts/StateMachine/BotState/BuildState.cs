using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildState : IState
{

    public void OnEnter(Bot bot)
    {
        //maxCollectedBrick
        bot.SetRandomTarget();
    }

    public void OnExecute(Bot bot)
    {
        //Debug.Log(Vector3.Distance(bot.transform.position, ))
        //bot.SetFinalTarget();
        //Debug.Log(Vector3.Distance(bot.transform.position, bot.navMeshAgent.destination));
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
