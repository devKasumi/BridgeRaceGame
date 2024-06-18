using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform finishBox;
    [SerializeField] private int minCollectedBricks;
    [SerializeField] private int maxCollectedBricks;

    private int collectedBrick;
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

        if (GameManager.GetInstance.CurrentState(GameState.GamePlay))
        {
            Moving();
        }
        else
        {
            ChangeState(new IdleState());
            return;
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        collectedBrick = Random.Range(GetMinCollectedBrick(), GetMaxCollectedBrick());

        ChangeState(new IdleState());
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

    public void Moving()
    {
        if (GetTargetBrickPosition() == Vector3.zero)
        {
            SetTargetBrickPosition();
        }
        else
        {
            if (IsCharacterReachTarget())
            {
                SetTargetBrickPosition();
            }
        }
    }

    public void StopMoving()
    {
        navMeshAgent.destination = transform.position;
    }

    public void MoveToBrick(Vector3 pos)
    {
        navMeshAgent.destination = pos;
    }

    public void SetFinalTarget()
    {
        navMeshAgent.destination = finishBox.position;
    }

    public int GetMinCollectedBrick()
    {
        return minCollectedBricks;
    }

    public int GetMaxCollectedBrick()
    {
        return maxCollectedBricks;
    }
    
    public bool BuildBridge()
    {
        return GetCurrentTotalBricks() >= collectedBrick;
    }

    public void SetRandomResetPoint()
    {
        navMeshAgent.destination = GetRandomResetPointPos();
    }

    public bool IsReachTarget()
    {
        return Vector3.Distance(transform.position, navMeshAgent.destination) < 1.3f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.TAG_DOOR))
        {
            //TODO: cache
            if (other.GetComponent<Door>().IsNextStageDoor())
            {
                Debug.LogError("process next stage!!!");
                ProcessToNextStage();
                LevelManager.GetInstance.GetCurrentLevel().LoadStage(this, GetCurrentStageIndex());
                SetTargetBrickPosition();
            }
        }
    }

}
