using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float playerHeight;
    [SerializeField] private float maxSlopeAngle;
    [SerializeField] private LayerMask bridgeLayer;

    private Quaternion originRotation;

    private float inputX;
    private float inputZ;

    private RaycastHit slopeHit;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        originRotation = transform.rotation;
        OnInit();
    }

    private void Update()
    {
        Move();

        rb.useGravity = !OnSlope();

        AdvanceToNextStage();
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeAnimation(Constants.ANIMATION_IDLE);
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 1f, bridgeLayer))
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down * playerHeight, Color.yellow);
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    public void Move()
    {
        inputX = floatingJoystick.Horizontal;
        inputZ = floatingJoystick.Vertical;

        moveDirection = new Vector3(inputX * GetMoveSpeed(), 0f, inputZ * GetMoveSpeed());

        if (OnSlope())
        {
            rb.velocity = new Vector3(GetSlopeMoveDirection().x * 5f, GetSlopeMoveDirection().y*5f-1f, GetSlopeMoveDirection().z*5f);   
        }
        else
        {
            rb.velocity = moveDirection;
        }

        if (!floatingJoystick.IsResetJoystick())
        {
            rb.isKinematic = false;
            ChangeAnimation(Constants.ANIMATION_RUN);
        }
        else
        {
            rb.isKinematic = true;
            ChangeAnimation(Constants.ANIMATION_IDLE);
        }

        transform.rotation = Quaternion.LookRotation(new Vector3(rb.velocity.x, originRotation.y, rb.velocity.z));
    }


    public void ResetPlayerRotation()
    {
        transform.rotation = Quaternion.identity;
    }

    public void AdvanceToNextStage()
    {
        // TODO fix:
        List<Transform> nextStageLines = LevelManager.GetInstance.GetCurrentLevel().GetCurrentStagePlatform(GetCurrentStageIndex()).GetNextStageLine();
        List<Transform> finalStageLines = LevelManager.GetInstance.GetCurrentLevel().GetFinalLines();
        if (IsClearStage(nextStageLines) && nextStageLines.Count > 0)
        {
            for (int i = 0; i < nextStageLines.Count; i++)
            {
                nextStageLines[i].gameObject.SetActive(false);
            }
            LevelManager.GetInstance.GetCurrentLevel().GetCurrentStagePlatform(GetCurrentStageIndex()).EnableGate();
            ProcessToNextStage();
            LevelManager.GetInstance.GetCurrentLevel().LoadStage(this, GetCurrentStageIndex());
        }
        else if (IsClearStage(finalStageLines))
        {
            for (int i = 0; i < nextStageLines.Count; i++)
            {
                finalStageLines[i].gameObject.SetActive(false);
            }
            LevelManager.GetInstance.GetCurrentLevel().GetCurrentStagePlatform(GetCurrentStageIndex()).EnableGate();
        }
    }

    public bool IsClearStage(List<Transform> nextStageLines)
    {
        for (int i = 0; i < nextStageLines.Count; i++)
        {
            if (Vector3.Distance(transform.position, nextStageLines[i].position) < 0.8f)
            {
                return true;
            }
        }
        return false;
    }
}
