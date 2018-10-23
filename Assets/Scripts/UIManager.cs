using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject _player;
    private HP _playerHP;
    private PlayerController _playerController;

    public RawImage _uiBlood;
    public List<Texture> _propsImages = new List<Texture>();

    public Text _title;
    //public RawImage _skillImage;
    public List<RawImage> _props = new List<RawImage>();

	// Use this for initialization
	public void Init() {
        _playerHP = _player.GetComponent<HP>();
        _playerController = _player.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        _uiBlood.rectTransform.localPosition = new Vector2(-400 + ((_playerHP._currentHP / 100f) * 400), 0);
	}

    public void UpdatePros()//更新playercontroller _items後呼叫
    {
        for(int i = 0; i < 2; i++)//初始化為空技能
        {
            _props[i].texture = _propsImages[11];
        }
        for(int i = 0; i < _playerController.getProps().Count; i++)
        {
            _props[i].texture = _propsImages[_playerController.getProps()[i]];
        }
    }
}
