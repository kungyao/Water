using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _moveSpeed = 0;
    public float _jumpForce = 0;
    public LayerMask _layer;            /**floor layer*/
    public GameObject _role;

    private Animator _animator;         /**player animator*/
    private Rigidbody2D _rigid2D;       /**player rigidbody*/
    //private Animator animator;
    private bool _isGround = false;
    private bool _faceRight = true;
    public bool FaceRight { get { return _faceRight; } }

    private int _currentRole = 0;
    public int CurrentRole { get { return _currentRole; } }

    private enum Role
    {
        Liquid,
        Solid,
        Vapor
    }

    void Start()
    {
        //init Luquid State
        _role.GetComponent<SpriteRenderer>().color = new Color(0, 0.196f, 1.0f, 0.6274f);
        _moveSpeed = 4;
        _jumpForce = 5.0f;

        _animator = this.GetComponentInChildren<Animator>();
        _rigid2D = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        //Switch();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        FallCheck();
    }

    public void ChangeRole(int role)
    {
        _currentRole = role;
        if(_currentRole == (int)Role.Liquid)
        {
            _role.GetComponent<SpriteRenderer>().color = new Color(0,0.196f,1.0f,0.6274f);
            _moveSpeed = 5;
            _jumpForce = 5.0f;
        }
        else if(_currentRole == (int)Role.Solid)
        {
            _role.GetComponent<SpriteRenderer>().color = new Color(0f, 1.0f, 0f, 0.7843f);
            _moveSpeed = 7;
            _jumpForce = 4.0f;
        }
        else if (_currentRole == (int)Role.Vapor)
        {
            _role.GetComponent<SpriteRenderer>().color = new Color(0.3529f, 0.3529f, 0.3529f, 0.3529f);
            _moveSpeed = 3;
            _jumpForce = 9.5f;
        }
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(transform.right * Time.deltaTime * h * _moveSpeed, Space.World);
        _animator.SetFloat("Speed", Mathf.Abs(h));
        _animator.SetBool("isGround", _isGround);
        if (Input.GetButtonDown("Jump") && _isGround) 
        {
            _animator.SetBool("isJumpUp", true);
            this.GetComponent<Rigidbody2D>().AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        }
        Flip(h);
    }

    /*private void Switch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Switch(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Switch(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Switch(2);
    }*/

    /*private void Switch(int key)
    {
        if (_currentPhase == key)
            return;
        _currentPhase = key;
        _charPhase[key].SetActive(true);
        for (int i = 0; i < 3; i++)
            if (i != key)
                _charPhase[i].SetActive(false);
    }*/

    private void GroundCheck()
    {
        Vector2 position = transform.position;
        Vector2 direction = -transform.up;
        float distance = 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, _layer);
        if (hit.collider)
        {
            _isGround = true;
        }
        else _isGround = false;
    }

    private void FallCheck()
    {
        float vd = _rigid2D.velocity.y;
        //判斷為誤差
        if (Mathf.Abs(vd) < 0.01)
        {
            _animator.SetBool("isFalling", false);
            _animator.SetBool("isJumpUp", false);
        }
        else
        {
            if (vd < 0)
            {
                _animator.SetBool("isJumpUp", false);
                _animator.SetBool("isFalling", true);
            }
            else
                _animator.SetBool("isFalling", false);
        }        
    }

    private void Flip(float x)
    {
        if (_faceRight && x < 0)
        {
            _faceRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
            //transform.Rotate(transform.up, 180);
        }
        else if(!_faceRight && x > 0)
        {
            _faceRight = true;
            transform.localScale = new Vector3(1, 1, 1);
            //transform.Rotate(transform.up, 180);
        }
    }
}
