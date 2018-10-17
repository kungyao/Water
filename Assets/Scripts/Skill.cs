using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {

    public GameObject _role;
    public GameObject _item;
    public GameObject _weapon;

    private PlayerController _roleController;
    private int _roleId;
    private bool _faceRight;
	// Use this for initialization
	void Start ()
    {
        _roleController = _role.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        _roleId = _roleController.CurrentRole;
        _faceRight = _roleController.FaceRight;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PutCube();
            ShootSaw();
        }
    }
    //Liquid Skill
    void ShootSaw()
    {
        if (_roleId != 0) return;
        if (_faceRight)
        {
            GameObject temp = Instantiate(_weapon, _role.transform.position + new Vector3(0.6f, 0, 0), Quaternion.identity) as GameObject;
            temp.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            Destroy(temp, 5.0f);
        }
        else
        {
            GameObject temp = Instantiate(_weapon, _role.transform.position + new Vector3(-0.6f, 0, 0), Quaternion.identity) as GameObject;
            temp.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10, ForceMode2D.Impulse);
            Destroy(temp, 5.0f);
        }
    }

    //Solid Skill
    void PutCube()
    {
        if(_roleId != 1)
        {
            return ;
        }

        if (_faceRight)
        {
            GameObject temp = Instantiate(_item, _role.transform.position + new Vector3(0.6f, 0, 0), Quaternion.identity) as GameObject;
            Destroy(temp, 10.0f);
        }
        else
        {
            GameObject temp = Instantiate(_item, _role.transform.position + new Vector3(-0.6f, 0, 0), Quaternion.identity) as GameObject;
            Destroy(temp, 10.0f);
        }
    }
}
