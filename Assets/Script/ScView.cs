using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScView : MonoBehaviour{
    [SerializeField] private Transform camView;
    [SerializeField] private float xSensy;
    [SerializeField] private float ySensy;
    [SerializeField] float zRota;

    float _xRota;
    float _yRota;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LookAround(Vector2 mouseMove) {
        _yRota += mouseMove.x * Time.deltaTime * xSensy;
        _xRota -= mouseMove.y * Time.deltaTime * ySensy;
        _xRota = Mathf.Clamp(_xRota, -90, 90);

        camView.rotation = Quaternion.Euler(_xRota, _yRota, zRota);
    }

}
