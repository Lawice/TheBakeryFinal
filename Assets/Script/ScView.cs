using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScView : MonoBehaviour{
    [SerializeField] private Transform camView;
    [SerializeField] private Transform orientation;
    [SerializeField] private float xSensy;
    [SerializeField] private float ySensy;

    private float yRota = 0;
    private float xRota = 0;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LookAround(Vector2 mouseMove) {
        yRota += mouseMove.x * Time.deltaTime * xSensy;
        xRota -= mouseMove.y * Time.deltaTime * ySensy;
        xRota = Mathf.Clamp(xRota, -90, 90);

        camView.rotation = Quaternion.Euler(xRota, yRota, 0);
        orientation.rotation = Quaternion.Euler(0,yRota, 0);
    }

}
