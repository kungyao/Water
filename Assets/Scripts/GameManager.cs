using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private Vector2[] ori_pos = new Vector2[4] { new Vector2(-6.0f, -3.5f),
        new Vector2(7.0f, -3.5f), new Vector2(-1.0f, -3.5f), new Vector2(3.0f, -3.5f) };

    public GameObject people;
    public GameObject[] props = new GameObject[8];
    private GameObject[] player = new GameObject[Info._people];
	// Use this for initialization
	void Start () {
        for (int i = 0; i < Info._people; i++)
        {
            player[i] = Instantiate(people, ori_pos[i], Quaternion.identity);
            player[i].GetComponent<PlayerController>().Init(i, Info._player_Infos[i].Color);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
