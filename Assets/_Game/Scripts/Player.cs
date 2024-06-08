using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private JoystickManager joystickManager;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float playerHeight;
    [SerializeField] private float maxSlopeAngle;

    private float inputX;
    private float inputZ;

    private RaycastHit slopeHit;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        

        Move();
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
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
        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * GetMoveSpeed(), ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        rb.useGravity = !OnSlope();

        inputX = joystickManager.InputHorizontal();
        inputZ = joystickManager.InputVertical();

        if (inputX > 0f) moveDirection = Vector3.forward;
        else moveDirection = Vector3.back;

        if (inputZ > 0f) moveDirection = Vector3.right;
        else moveDirection = Vector3.left;

        if (OnSlope())
        {
            if (rb.velocity.magnitude > GetMoveSpeed())
            {
                rb.velocity = rb.velocity.normalized * GetMoveSpeed();
            }
        }

        rb.velocity = new Vector3(inputX * GetMoveSpeed(), 0f, inputZ * GetMoveSpeed());
    }

    public void StopClimbSatir()
    {
        //Debug.Log("pokemon xDDDDDD");
        inputZ = 0f;
    }
}
