using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private NavMeshAgent navMeshAgent;

    private IState currentState;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }

        if (GetTargetBrickPosition() == Vector3.zero)
        {
            SetTargetBrickPosition();
        }
        else
        {
            Debug.Log("target not zero: " + Vector3.Distance(transform.position, GetTargetBrickPosition()));
            if (IsCharacterReachTarget())
            {
                Debug.Log("set new target");
                SetTargetBrickPosition();
            }
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        ChangeState(new PatrolState());
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    public void MoveToBrick(Vector3 pos)
    {
        Debug.LogError("bot move!!!!" + pos);
        navMeshAgent.destination = pos;
        //isMoving = true;
    }
}
