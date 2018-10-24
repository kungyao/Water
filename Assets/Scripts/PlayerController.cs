using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float _moveSpeed = 0;
    public float _jumpForce = 0;
    public LayerMask _layer;            /**floor layer*/

    private Animator _animator;         /**player animator*/
    private Rigidbody2D _rigid2D;       /**player rigidbody*/
    //private CapsuleCollider2D _collider;

    private bool _isGround = false;
    private bool _faceRight = true;
    public bool FaceRight { get { return _faceRight; } }

    private int _currentRole = 0;
    public int CurrentRole { get { return _currentRole; } }

    public Canvas _playerUI;
    private List<int> _items = new List<int>();

    // Skill Textures
    private int _skill_ID = 0;
    private bool _isCD = false;
    public Texture[] _skill = new Texture[6];

    // Item prefab
    public GameObject _fireball;
    public GameObject _stoneMagic;
    public GameObject _poison;

    //PlayerState
    public bool _isPoisonous = false;
    public bool _isStone = false;

    //UiManager
    //public int _userId; // 0 1 2 3
    public GameObject _playerInterface;
    private UIManager _playerInterfaceManager;

    //Manipulate keycode
    public KeyCode jump;
    public KeyCode props1;
    public KeyCode props2;
    public KeyCode skill;
    public string moveAxis;

    public enum Item
    {
        FireBall,
        BigAid,
        MidAid,
        SmallAid,
        Poison,
        FireFloor,
        BigBomb,
        Stone,
        RestoreMagic,
        Accelerate,
        MineTrap
    }

    public void Init(int userId , player_Info player)
    {
        //init color and movespeend and jumpspeed
        this.GetComponent<SpriteRenderer>().color = player.Color;
        _skill_ID = player.Skill;
        this.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<RawImage>().texture = _skill[_skill_ID];
        _moveSpeed = 4;
        _jumpForce = 5.0f;
        //_userId = userId;

        //Initial playerInterface Info
        _playerInterface.GetComponent<RectTransform>().localPosition = new Vector2(-645 + userId * 485, -513);
        _playerInterfaceManager._title.text = "P" + (userId + 1).ToString();
        SetKeycode(userId);
        
        //_collider = this.GetComponent<CapsuleCollider2D>();

        // _animator = this.GetComponent<Animator>();
        // _rigid2D = this.GetComponent<Rigidbody2D>();
    }
    void Awake()
    {
        _playerInterfaceManager = _playerInterface.GetComponent<UIManager>();
        _playerInterfaceManager.Init();
        _playerInterfaceManager.UpdatePros();
        _animator = this.GetComponent<Animator>();
        _rigid2D = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(props1))
            UseItem(0);
        else if (Input.GetKeyDown(props2))
            UseItem(1);
        else if (Input.GetKeyDown(skill) && !_isCD)
            UseSkill(this._skill_ID);
        //Switch();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        FallCheck();
    }

    private void Move()
    {
        if (!_isStone)
        {
            float h = Input.GetAxis(moveAxis);
            //float v = Input.GetAxis("Vertical");
            transform.Translate(transform.right * Time.deltaTime * h * _moveSpeed, Space.World);
            _animator.SetFloat("Speed", Mathf.Abs(h));
            _animator.SetBool("isGround", _isGround);
            if (Input.GetKeyDown(jump) && _isGround)
            {
                _animator.SetBool("isJumpUp", true);
                this.GetComponent<Rigidbody2D>().AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            }
            Flip(h);
        }
    }

    private void GroundCheck()
    {
        Vector2 position = transform.position;
        Vector2 direction = -transform.up;
        float distance = 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, _layer);
        //RaycastHit2D hit = Physics2D.CapsuleCast(position, _collider.size, CapsuleDirection2D.Vertical, 0, direction, distance, _layer);
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
            _playerUI.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            //transform.Rotate(transform.up, 180);
        }
        else if(!_faceRight && x > 0)
        {
            _faceRight = true;
            transform.localScale = new Vector3(1, 1, 1);
            _playerUI.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //transform.Rotate(transform.up, 180);
        }
    }

    // Skill Part
    public void UseSkill(int ID) {
        if (ID == 0) {
            this.GetComponent<HP>().Recover(50);
            StartCoroutine(WaitCD(40));
        }
        else if (ID == 1) {
            if (_faceRight)
            {
                GameObject temp = Instantiate(_fireball, this.transform.position + new Vector3(1f, 0, 0), Quaternion.identity) as GameObject;
                temp.transform.localScale = new Vector3(-0.4f, 0.4f, 1);
                temp.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            }
            else
            {
                GameObject temp = Instantiate(_fireball, this.transform.position + new Vector3(-1f, 0, 0), Quaternion.identity) as GameObject;
                temp.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10, ForceMode2D.Impulse);
            }
            StartCoroutine(WaitCD(10));
        }
        else if (ID == 2) {
            int i = Random.Range(0, 9);
            if (i >= 5) i += 2;
            AddItem(i);
            StartCoroutine(WaitCD(15));
        }
        else if (ID == 3) {
            StartCoroutine(SheldMagic());
            StartCoroutine(WaitCD(30));
        }
        else {
            StartCoroutine(AccelerateMagic(2));
            StartCoroutine(WaitCD(20));
        }
    }

    //Item Part
    public List<int> getProps()
    {
        return _items;
    }
    public bool CheckItemDestroy()
    {
        if (_items.Count >= 2)
            return false;
        return true;
    }
    public void AddItem(int _itemId)
    {
        _items.Add(_itemId);
        _playerInterfaceManager.UpdatePros();
        
    }
    public void UseItem(int _useId)
    {
        if (_useId > _items.Count - 1)
        {
            return;
        }
        //Item Use
        int itemId = _items[_useId];

        if (itemId == (int)Item.FireBall)
        {
            if (_faceRight)
            {
                GameObject temp = Instantiate(_fireball, this.transform.position + new Vector3(1f, 0, 0), Quaternion.identity) as GameObject;
                temp.transform.localScale = new Vector3(-0.4f, 0.4f, 1);
                temp.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            }
            else
            {
                GameObject temp = Instantiate(_fireball, this.transform.position + new Vector3(-1f, 0, 0), Quaternion.identity) as GameObject;
                temp.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10, ForceMode2D.Impulse);
            }
        }
        else if (itemId == (int)Item.BigAid)
        {
            this.GetComponent<HP>().Recover(70);
        }
        else if (itemId == (int)Item.MidAid)
        {
            this.GetComponent<HP>().Recover(40);
        }
        else if (itemId == (int)Item.SmallAid)
        {
            this.GetComponent<HP>().Recover(10);
        }
        else if (itemId == (int)Item.Poison)
        {
            if (_faceRight)
            {
                GameObject temp = Instantiate(_poison, this.transform.position + new Vector3(1f, 0, 0), Quaternion.identity) as GameObject;
                temp.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            }
            else
            {
                GameObject temp = Instantiate(_poison, this.transform.position + new Vector3(-1f, 0, 0), Quaternion.identity) as GameObject;
                temp.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10, ForceMode2D.Impulse);
            }
        }
        else if (itemId == (int)Item.FireFloor)
        {
            

        }
        else if(itemId == (int)Item.BigBomb)
        {

        }
        else if(itemId == (int)Item.Stone)
        {
            if (_faceRight)
            {
                GameObject temp = Instantiate(_stoneMagic, this.transform.position + new Vector3(1f, 0, 0), Quaternion.identity) as GameObject;
                temp.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            }
            else
            {
                GameObject temp = Instantiate(_stoneMagic, this.transform.position + new Vector3(-1f, 0, 0), Quaternion.identity) as GameObject;
                temp.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10, ForceMode2D.Impulse);
            }
        }
        else if (itemId == (int)Item.RestoreMagic)
        {
            StartCoroutine(RestoreMagic());
        }
        else if (itemId == (int)Item.Accelerate)
        {
            StartCoroutine(AccelerateMagic(1.5f));
        }
        else if (itemId == (int)Item.MineTrap)
        {

        }

        _items.RemoveAt(_useId);
        _playerInterfaceManager.UpdatePros();
    }

    //Set Keycode
    private void SetKeycode(int userId)
    {
        if (userId == 0)
        {
            moveAxis = "Horizontal";
            jump = KeyCode.W;
            props1 = KeyCode.K;
            props2 = KeyCode.L;
            skill = KeyCode.J;
        }
        else if (userId == 1)
        {
            moveAxis = "2p";
            jump = KeyCode.UpArrow;
            props1 = KeyCode.Keypad2;
            props2 = KeyCode.Keypad3;
            skill = KeyCode.Keypad1;
        }
    }



    //PlayerState
    public void BeStone()
    {
        StartCoroutine(IsStone());
    }

    public void BePoisonous()
    {
        StartCoroutine(IsPoisonous());
    }

    IEnumerator RestoreMagic()
    {
        for(int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            this.GetComponent<HP>().Recover(10);
        }
    }

    IEnumerator AccelerateMagic(float s)
    {
        this._moveSpeed *= s;
        yield return new WaitForSeconds(5f);
        this._moveSpeed /= s;
    }

    IEnumerator WaitCD(int t) {
        _isCD = true;
        this.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<RawImage>().texture = _skill[5];
        yield return new WaitForSeconds(t);
        _isCD = false;
        this.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<RawImage>().texture = _skill[_skill_ID];
    }

    IEnumerator SheldMagic() {
        this.tag = "Untagged";
        yield return new WaitForSeconds(1.0f);
        this.tag = "Player";
    }

    IEnumerator IsStone()
    {
        _isStone = true;
        yield return new WaitForSeconds(3f);
        _isStone = false;
    }

    IEnumerator IsPoisonous()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            this.GetComponent<HP>().Damage(5);
        }
    }
}
