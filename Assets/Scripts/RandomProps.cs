using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomProps : MonoBehaviour
{

    public GameObject[] Plats;//所有地板
    public GameObject[] Props;//所有道具
    public float High = 0.5f;//道具出現離地板高度
    public float TimeGap = 5;//多久一次

    // Update is called once per frame
    void Update()
    {
        if (Time.time > TimeGap)//多久生一次
        {
            int index = Random.Range(0, Plats.Length);
            Transform[] transforms = Plats[index].GetComponentsInChildren<Transform>();
            do
            {
                index = Random.Range(0, transforms.Length);
            } while (transforms[index].gameObject.name[0] == 'p');//只要子物件座標

            int index2 = Random.Range(0, Props.Length);//取得子物件
            Vector3 tmpPos = transforms[index].position;
            tmpPos.y += High;//道具位置微調
            GameObject tmp = GameObject.Instantiate(Props[index2], tmpPos, Quaternion.identity);
            TimeGap = Time.time + 5;
        }


    }
}
