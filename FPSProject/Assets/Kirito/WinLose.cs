using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLose : MonoBehaviour {


    public int End_Kill;//試合が終了するキル数
    public int Kill_A;//Aチームのキル数
     public int Kill_B;//Bチームのキル数
    
    // Use this for initialization
	void Start () {
        int Kill_A = 0;
        int Kill_B = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if(Kill_A == End_Kill && Kill_A > Kill_B)//設定した試合が終了するキル数がATeamのキル数に達するかつ、BTeaｍのキル数より多い場合
        {
            Debug.Log("TeamA Win!!");
        }
        if (Kill_B == End_Kill && Kill_B > Kill_A)//設定した試合が終了するキル数がATeamのキル数に達するかつ、BTeaｍのキル数より多い場合
        {
            Debug.Log("TeamB Win!!");
        }
    }
}
