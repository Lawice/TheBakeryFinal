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

    void Start() {
        body = GetComponent<Rigidbody>();
    }

    void Update() {
        grounded = Physics.Raycast(transform.position, Vector3.down, height*0.5f + 0.2f, Ground);
        if (grounded) { body.drag = groundDrag; }
        else { body.drag = 0; }
    }
    private void OnCollision3D(Collision collision) {
        if (collision.gameObject.tag == "Ground") { grounded = true;}
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
}
