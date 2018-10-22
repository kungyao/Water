using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().BeStone();
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.layer == 8)
        {
            Destroy(this.gameObject);
        }
    }
}
