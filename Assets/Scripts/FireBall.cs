using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<HP>().Damage(30f);
            Destroy(this.gameObject);
        }else if(collision.gameObject.layer == 8)
        {
            Destroy(this.gameObject);
        }
    }
}
