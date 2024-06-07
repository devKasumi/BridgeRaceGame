using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private JoystickManager joystickManager;
    [SerializeField] private Rigidbody rb;

    private float inputX;
    private float inputZ;

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

    public void Move()
    {
        inputX = joystickManager.InputHorizontal();
        inputZ = joystickManager.InputVertical();

        rb.velocity = new Vector3(inputX * GetMoveSpeed(), 0f, inputZ * GetMoveSpeed());
    }
}
