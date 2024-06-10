using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    //[SerializeField] private JoystickManager joystickManager;
    [SerializeField] private FixedJoystick joystick;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();

        rb.useGravity = !OnSlope();

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
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 1f, bridgeLayer))
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down * playerHeight, Color.yellow);
            //Debug.Log("raycast hit bridge!!!!!");
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        //Debug.Log(Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized);
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    //public void Move()
    //{
    //    inputX = joystickManager.InputHorizontal();
    //    inputZ = joystickManager.InputVertical();

    //    moveDirection = new Vector3(inputX * GetMoveSpeed(), 0f, inputZ * GetMoveSpeed());

    //    //Vector3 slopeMovement = GetSlopeMoveDirection();

    //    if (OnSlope())
    //    {
    //        //Vector3 slopeDirection = Vector3.up - slopeHit.normal * Vector3.Dot(Vector3.up, slopeHit.normal);
    //        //moveDirection = new Vector3(inputX * GetMoveSpeed(), -slopeHit.point.y, inputZ * GetMoveSpeed());
    //        //Debug.Log("on slope!!!");
    //        transform.rotation = originRotation;
    //        rb.velocity = GetSlopeMoveDirection() * 5f;
    //        //rb.velocity = slopeMovement * 5f;
    //        //Debug.Log(slopeDirection);
    //        //rb.velocity = slopeDirection * 5f;
    //    }
    //    else
    //    {
    //        transform.rotation = Quaternion.LookRotation(rb.velocity);

    //        rb.velocity = moveDirection;
    //    }



    //    //Debug.Log(moveDirection);
    //}

    public void Move()
    {
        moveDirection = new Vector3(joystick.Horizontal * GetMoveSpeed(), rb.velocity.y, joystick.Vertical * GetMoveSpeed());

        if (OnSlope())
        {
            Debug.Log(slopeHit.normal);
            moveDirection = GetSlopeMoveDirection() * 5f;
            //if (moveDirection.y > 0f)
            //{
            //    moveDirection -= Vector3.up * moveDirection.y;
            //}
            rb.velocity = moveDirection;
        }

        rb.velocity = moveDirection;


        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }

        Debug.Log(moveDirection);
    }

}
