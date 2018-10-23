using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPeople : MonoBehaviour {

    public GameObject people;
    public GameObject select;
    private int player;

    

    void CallSelect() {
        select.SetActive(true);
        people.SetActive(false);
    }

    void CallPeople() {
        select.SetActive(false);
        people.SetActive(true);
    }

    public void Btn_Back() {

        select.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
        select.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
        select.transform.GetChild(2).GetChild(3).gameObject.SetActive(false);

        select.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
        select.transform.GetChild(3).GetChild(2).gameObject.SetActive(true);
        select.transform.GetChild(3).GetChild(3).gameObject.SetActive(false);

        CallPeople();
    }
    public void Btn_Go() {
        Info._player_Infos = new player_Info[player];
        for (int i=0; i < player; i++)
            Info._player_Infos[i] = select.transform.GetChild(i).GetComponent<PlayerSelect>().Data;
        Info._people = player;
        SceneManager.LoadScene(1);
    }

    public void Btn2_Click() {
        player = 2;
        CallSelect();
        select.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
        select.transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
        select.transform.GetChild(2).GetChild(3).gameObject.SetActive(true);

        select.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
        select.transform.GetChild(3).GetChild(2).gameObject.SetActive(false);
        select.transform.GetChild(3).GetChild(3).gameObject.SetActive(true);
    }
    public void Btn3_Click()
    {
        player = 3;
        CallSelect();
        select.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
        select.transform.GetChild(3).GetChild(2).gameObject.SetActive(false);
        select.transform.GetChild(3).GetChild(3).gameObject.SetActive(true);
    }
    public void Btn4_Click()
    {
        player = 4;
        CallSelect();
    }

}
