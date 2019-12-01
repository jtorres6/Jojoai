using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState {
    Idle = 0,
    Left = 1,
    Right = 2,
    GonnaFall = 3,
}

public class Player : MonoBehaviour
{
    // Physics Constants
    public const float SPEED = 6.0f;
    public const float JUMP_FORCE = 700f;

    // Components
    private HFTInput _hftInput;
    private HFTGamepad _gamepad;
    private Animator _animator;
    private Rigidbody _rigidbody;

    // State helpers
    private float _distToGround;
    private bool _grounded = true;
    private bool _falling = false;

    private bool Grounded() {
        return Physics.Raycast(transform.position, -Vector3.up, _distToGround + 0.1f);
    }

    public bool Dead() {
        print("Ayyyy me ha dao");

        return false;
    }

    private IEnumerator Fall() {
        _falling = true;
        _animator.SetInteger("moveState", (int) MoveState.GonnaFall);
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        yield return new WaitForSeconds(0.75f);
        _animator.SetInteger("moveState", (int) MoveState.Idle);
        _rigidbody.constraints = ~RigidbodyConstraints.FreezePositionY;
        _rigidbody.AddForce(new Vector3(0, -2 * JUMP_FORCE, 0));
        _falling = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        _hftInput = GetComponent<HFTInput>();
        _gamepad = GetComponent<HFTGamepad>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        Renderer renderer = transform.GetChild(0).gameObject.GetComponent<Renderer>();
        renderer.material.SetColor("_BaseColor", _gamepad.color);
        _distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        /*** Jump ***/
        if (Grounded() && _hftInput.GetButtonDown("fire1")) {
            _rigidbody.AddForce(new Vector3(0, JUMP_FORCE, 0));
        }

        /*** Go down heavily ***/
        if (!Grounded() && _hftInput.GetButtonDown("fire2")) {
            StartCoroutine(Fall());
        }

        /*** X axis movement ***/
        if (!_falling) {
            float dx = SPEED * _hftInput.GetAxis("Horizontal") * Time.deltaTime;
            transform.position = transform.position + new Vector3(dx, 0.0f, 0.0f);

            if (dx == 0) {
                _animator.SetInteger("moveState", (int) MoveState.Idle);
            } else if (dx < 0) {
                _animator.SetInteger("moveState", (int) MoveState.Left);
            } else {
                _animator.SetInteger("moveState", (int) MoveState.Right);
            }
        }
    }
}
