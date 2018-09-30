using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public int id;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponent<PlayerController>().ChangeRole(id);
            //collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Force);
    }
}
