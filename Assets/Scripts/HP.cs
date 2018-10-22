using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour {

    public RectTransform _blood;
    public float _totalHP = 100;
    public float _currentHP = 100;
    private bool _isDied = false;

    // Use this for initialization
    void Start () {
        StartCoroutine(Drain());// Drain blood
    }
	
	// Update is called once per frame
	void Update () {
        _blood.transform.localPosition = new Vector2(-1 + (_currentHP / _totalHP) * 1, 0);
    }

    public void Damage(float _damage)
    {
        _currentHP -= _damage;
        if (_currentHP <= 0)
        {
            _isDied = true;
            //Play died animation
        }
    }

    public void Recover(float _recover)
    {
        if (_isDied)
            return;
        _currentHP += _recover;
        if (_currentHP > 100)
        {
            _currentHP = 100;
        }
    }

    private IEnumerator Drain()
    {
        while (true)
        {
            if (_isDied)
                break;
            yield return new WaitForSeconds(1f);
            this.Damage(1);
        }
    }
}
