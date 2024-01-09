using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScMovement : MonoBehaviour {
    public Transform orientation;
    Rigidbody body;

    [Header("~~~~Movement~~~~")]
    [SerializeField] private float speed;
    Vector3 moveDir;

    [Header("~~~~GroundCheck~~~~")]
    public float height;
    [SerializeField] bool grounded;
    public LayerMask Ground;
    [SerializeField] float groundDrag;

    [Header("~~~~Jumping~~~~")]
    [SerializeField] private float jumpForce;
    [SerializeField] private bool readyToJump;
    [SerializeField] private int jumps=3;
    

    void Start() {
        body = GetComponent<Rigidbody>();
    }

    void Update() {
        grounded = Physics.Raycast(transform.position, Vector3.down, height*0.5f + 0.2f, Ground);
        if (grounded) { body.drag = groundDrag; jumps = 3; readyToJump = true; }
        else { body.drag = 0; }
        SpeedControl();
        if (jumps <= 1) { readyToJump = false;}
    }

    public void Move(Vector2 MoveInput) {
        moveDir = orientation.forward * MoveInput.y + orientation.right * MoveInput.x;
        body.AddForce(moveDir.normalized * speed * 10, ForceMode.Force);
    }

    private void SpeedControl() { 
        Vector3 flatVelocity = new Vector3(body.velocity.x, 0f, body.velocity.z);
        if(flatVelocity.magnitude > speed) {
            Vector3 limitedVelocity = flatVelocity.normalized * speed;
            body.velocity = new Vector3(limitedVelocity.x, body.velocity.y, limitedVelocity.z);
        }
    }

    public void Jump() {
        print("jump");
        if (readyToJump) {
            jumps -= 1;
            body.velocity = new Vector3(body.velocity.x, 0f, body.velocity.z);
            body.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
}
