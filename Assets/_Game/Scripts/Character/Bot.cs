using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] public Transform finishBox;
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
        if (IsCharacterReachTarget())
        {
            SetTargetBrickPosition();
        }
    }

    public void SetTarget(Vector3 pos)
    {
        navMeshAgent.destination = pos;
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

    public void CheckStair(Stair stair)
    {
        if (GetCurrentTotalBricks() > 0)
        {
            if (stair.GetColorType() != CurrentCharacterColor())
            {
                NormalStairChecking(stair.Bridge(), stair);
            }
            stair.Bridge().ResetCurrentBarrier(stair.Bridge().GetStairIndex(stair));
            stair.StairMeshRenderer().enabled = true;

            if (!stair.Bridge().IsEnoughStairForBridge() && GetCurrentTotalBricks() == 0)
            {
                ChangeState(new PatrolState());
            }
        }
        else
        {
            if (stair.GetColorType() != CurrentCharacterColor())
            {
                ChangeState(new PatrolState());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.TAG_DOOR))
        {
            //TODO: cache
            Door door = Cache.GetDoor(other);
            if (door.IsNextStageDoor())
            {
                Debug.LogError("process next stage!!!");
                ProcessToNextStage();
                LevelManager.GetInstance.GetCurrentLevel().LoadStage(this, GetCurrentStageIndex());
                SetTargetBrickPosition();
            }
        }
    }

}
