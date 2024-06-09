using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private float randomTime;
    private float timer;

    public void OnEnter(Bot bot)
    {
        //timer = 0f;
        //randomTime = Random.Range(4f, 7f);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;

        if (bot.GetTargetBrickPosition() != null)
        {
            bot.MoveToBrick(bot.GetTargetBrickPosition());
        }
        //else
        //{
        //    if (timer < randomTime)
        //    {
        //        Debug.Log("moving time");
        //    }
        //    else bot.ChangeState(new IdleState());
        //}
    }

    public void OnExit(Bot bot)
    {

    }
}
