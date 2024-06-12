using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    //private float randomTime;
    //private float timer;

    public void OnEnter(Bot bot)
    {
        //timer = 0f;
        //randomTime = Random.Range(4f, 7f);
        //if (bot.GetTargetBrickPosition() != null)
        //{
        //    bot.MoveToBrick(bot.GetTargetBrickPosition());

        //}
    }

    public void OnExecute(Bot bot)
    {
        //timer += Time.deltaTime;
        bot.MoveToBrick(bot.GetTargetBrickPosition());
        //if (bot.GetTargetBrickPosition() != null)
        //{

        //}
        //else
        //{
        //    if (timer < randomTime)
        //    {
        //        Debug.Log("moving time");
        //    }
        //    else bot.ChangeState(new IdleState());
        //}
        if (bot.BuildBridge())
        {
            bot.ChangeState(new BuildState());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
