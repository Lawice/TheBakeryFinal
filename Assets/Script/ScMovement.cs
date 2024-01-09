using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScMovement : MonoBehaviour {
    [Header("Movement")]
   
    [SerializeField] private float speed;

    Vector3 moveDir;
    public Transform orientation;

    Rigidbody body;


    void Start() {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
    }

    public void Move(Vector2 MoveInput) {
        moveDir = orientation.forward * MoveInput.y + orientation.right * MoveInput.x;
        body.AddForce(moveDir.normalized * speed*5, ForceMode.Force);
    }
}
