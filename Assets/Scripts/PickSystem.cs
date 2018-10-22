using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSystem : MonoBehaviour {

    public int _itemId;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.GetComponent<PlayerController>().CheckItemDestroy())
            {
                collision.GetComponent<PlayerController>().AddItem(_itemId);
                Destroy(this.gameObject);
            }
        }
    }
}
