using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private InputHandler _input;

    [SerializeField]
    private float movSpeed = 10f;
    private Rigidbody rb;
    private Vector2 moveAmount;
    private Animator anim;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private bool rotateTowardsMouse;

    [HideInInspector]
    public Vector2 position;

    public float health;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        var movementVector = MoveTowardsTarget(moveInput);
        if (!rotateTowardsMouse)
        {
            RotatePlayer(movementVector);
        }
        else
        {
            RotateTowardsMouseVector();
        }
    }

    private void RotateTowardsMouseVector()
    {
        Ray ray = camera.ScreenPointToRay(_input.MousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private void RotatePlayer(Vector3 movementVector)
    {
        if (movementVector.magnitude == 0)
            return;
        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }

    private Vector3 MoveTowardsTarget(Vector3 moveInput)
    {
        var speed = movSpeed * Time.deltaTime;
        moveInput = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * moveInput;
        var targetPosition = transform.position + moveInput * speed;
        transform.position = targetPosition;
        return moveInput;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
