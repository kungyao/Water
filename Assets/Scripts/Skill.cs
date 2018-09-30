using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {

    public GameObject _roleManager;
    public GameObject _role;
    public GameObject _Item;

    private PlayerController _roleController;
    private int _roleId;
    private bool _faceRight;
	// Use this for initialization
	void Start ()
    {
        _roleController = _roleManager.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        _roleId = _roleController.CurrentRole;
        _faceRight = _roleController.FaceRight;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PutCube();
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
            GameObject temp = Instantiate(_Item, _role.transform.position + new Vector3(0.6f, 0, 0), Quaternion.identity) as GameObject;
            Destroy(temp, 10.0f);
        }
        else
        {
            GameObject temp = Instantiate(_Item, _role.transform.position + new Vector3(-0.6f, 0, 0), Quaternion.identity) as GameObject;
            Destroy(temp, 10.0f);
        }
    }
}
