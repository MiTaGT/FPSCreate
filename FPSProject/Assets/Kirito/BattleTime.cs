using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTime : MonoBehaviour {

    public float Sectime = 0; //秒数
    public int Mintime;//分




    void Start()
    {




        GetComponent<Text>().text = Mintime.ToString() + ":" + ((int)Sectime).ToString();//float型からint型へCastしてString型に変換して表示する

    }



    void Update()
    {

       


        if (Sectime > 0 && Mintime >= 0)
        {
            Sectime -= Time.deltaTime; //1秒に1ずつ減らしていく
        }

        else
        {
            Debug.Log("次回城之内死す");
        }


        //マイナスは表示しない
        if (Sectime < 0)
        {
            Sectime = 60;
            Mintime--;
        }
       

        GetComponent<Text>().text = Mintime.ToString() + ":" + ((int)Sectime).ToString();

    }
}
