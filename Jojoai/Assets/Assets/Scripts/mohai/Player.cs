using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState {
    Idle = 0,
    Left = 1,
    Right = 2,
}

public class Player : MonoBehaviour
{
    public const float SPEED = 4.0f;
    public const float JUMP_FORCE = 700f;

    private HFTInput _hftInput;
    private HFTGamepad _gamepad;
    private Animator _animator;
    private Rigidbody _rigidbody;

    private bool _grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        _hftInput = GetComponent<HFTInput>();
        _gamepad = GetComponent<HFTGamepad>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = _gamepad.color;
    }

    // TODO: Add collision here with ground, and set grounded to true

    // Update is called once per frame
    void Update()
    {
        float dx = SPEED * (_hftInput.GetAxis("Horizontal") + Input.GetAxis("Horizontal")) * Time.deltaTime;
        transform.position = transform.position + new Vector3(dx, 0.0f, 0.0f);

        /*** X axis movement ***/
        if (dx == 0) {
            _animator.SetInteger("moveState", (int) MoveState.Idle);
        } else if (dx < 0) {
            _animator.SetInteger("moveState", (int) MoveState.Left);
        } else {
            _animator.SetInteger("moveState", (int) MoveState.Right);
        }

        /*** Jump ***/
        if (_grounded && _hftInput.GetButtonDown("fire1")) {
            print("HOLAAA");
            _grounded = false;
            _rigidbody.AddForce(new Vector3(0, JUMP_FORCE, 0));
        }
    }
}
