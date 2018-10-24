using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private Vector2[] ori_pos = new Vector2[4] { new Vector2(-6.0f, -3.5f),
        new Vector2(7.0f, -3.5f), new Vector2(-1.0f, -3.5f), new Vector2(3.0f, -3.5f) };
    private int less_prople;
    private bool[] last;
    private int last_ID;
    public GameObject end;
    public GameObject people;
    public GameObject[] props = new GameObject[8];
    private GameObject[] player = new GameObject[Info._people];
    // Use this for initialization
    void Start()
    {
        last = new bool[Info._people];
        less_prople = Info._people;
        for (int i = 0; i < Info._people; i++)
        {
            last[i] = true;
            player[i] = Instantiate(people, ori_pos[i], Quaternion.identity);
            player[i].GetComponent<PlayerController>().Init(i, Info._player_Infos[i]);
        }
    }


    // Update is called once per frame
    void Update () {
        for (int i = 0; i < Info._people; i++)
        {
            if (player[i].GetComponent<HP>().IsDied)
            {
                less_prople--;
                last[i] = false;
            }
            if (less_prople == 1) {
                end.SetActive(true);
                this.GetComponent<RandomProps>().enabled = false;
                for (int j = 0; j < Info._people; j++)
                {
                    if (last[j])
                    {
                        last_ID = j + 1;
                        player[j].GetComponent<HP>().enabled = false;
                        player[j].GetComponent<PlayerController>().enabled = false;
                        break;
                    }
                }
                end.transform.GetChild(0).GetComponent<Text>().text = "P" + last_ID.ToString() + " Win";
            }
        }
    }
    public void Btn_Exit() {
        SceneManager.LoadScene(0);
    }
}
