using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private Vector2[] ori_pos = new Vector2[4] { new Vector2(-6.0f, -3.5f),
        new Vector2(7.0f, -3.5f), new Vector2(-1.0f, -3.5f), new Vector2(3.0f, -3.5f) };

    private int[] record = new int[Info._people]; // 以後排名用
    private int less_prople = 1;// 剩餘人數

    // Props Instantiate
    private float High = 0.5f;  // 道具出現離地板高度
    private float TimeGap = 5;  // 多久一次
    private bool _isInstance = true;    // 是否生成 

    // Plat obj
    public GameObject[] plats;  // 所有地板(大範圍)

    public GameObject end;      // 結算UI
    public GameObject people;   // 人物物件
    public GameObject[] props = new GameObject[8];  // 道具
    private GameObject[] player = new GameObject[Info._people]; // 玩家物件

    void Start()
    {
        less_prople = Info._people;
        for (int i = 0; i < Info._people; i++)
        {
            record[i] = 1;
            player[i] = Instantiate(people, ori_pos[i], Quaternion.identity);
            player[i].GetComponent<PlayerController>().Init(i, Info._player_Infos[i]);
        }
    }

    void Update () {

        for (int i = 0; i < Info._people; i++)
        {
            if (player[i].GetComponent<HP>().IsDied)
            {
                record[i] = less_prople;
                less_prople--;
            }
            if (less_prople == 1) {
                end.SetActive(true);
                _isInstance = false; 
                for (int j = 0; j < Info._people; j++)
                {
                    if (record[j] == 1)
                    {
                        player[j].GetComponent<HP>().enabled = false;
                        player[j].GetComponent<PlayerController>().enabled = false;
                        end.transform.GetChild(0).GetComponent<Text>().text = "P" + (j+1).ToString() + " Win";
                        break;
                    }
                }
                

            }
        }

        if (Time.time > TimeGap && _isInstance) {  // 多久生一次
            PropInstance();
            TimeGap = Time.time + 5 / less_prople;
        }
    }

    public void Btn_Exit() {
        SceneManager.LoadScene(0);
    }
    private void PropInstance() {
        int index1 = Random.Range(0, plats.Length); // 第一層(平台集合)
        int index2 = Random.Range(0, plats[index1].transform.childCount);   // 第二層(各平台)
        int index3 = Random.Range(0, plats[index1].transform.GetChild(index2).childCount);  //第三層(各格子)
        int rand_prop = Random.Range(0, props.Length); // 取得道具
        Vector3 tmpPos = plats[index1].transform.GetChild(index2).GetChild(index3).position;// 生成位置
        tmpPos.y += High;   // 道具位置微調
        GameObject tmp = GameObject.Instantiate(props[rand_prop], tmpPos, Quaternion.identity);
    }

    IEnumerator PropWait(float CD)
    {
        yield return new WaitForSeconds(CD);       
    }
}
