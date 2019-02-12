using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
/// <summary>
/// マッチメーカー(ドラえもん風)
/// </summary>
public class MatchMaker : MonoBehaviour {
    //マッチリスト格納用List
    List<MatchInfoSnapshot> m_Matchs = null;

    /// <summary>
    /// スタート
    /// </summary>
    private void Start() {
        //マッチメーカーを使う準備
        NetworkManager.singleton.StartMatchMaker();
    }

    /// <summary>
    /// アップデート
    /// </summary>
    private void Update() {
        NetworkManager.singleton.matchMaker.ListMatches(0,10,"",true,0,0,OnListMatches);
    }
    /// <summary>
    /// マッチ製作終了後呼ばれるもの
    /// </summary>
    void OnCreateMatch(bool success, string extendedInfo, MatchInfo matchInfo) {
        if (success) {
            NetworkManager.singleton.StartHost(matchInfo);
        } else {
            Debug.Log("失敗");
        }
    }
    /// <summary>
    /// マッチリスト取得後呼ばれるもの
    /// </summary>
    private void OnListMatches(bool success,string extendedInfo,List<MatchInfoSnapshot> matches) {
        if (success) {
            //成功
            m_Matchs  = matches;
        } else {
            //失敗…
            Debug.Log("失敗");
        }
    }
    /// <summary>
    /// マッチ入場時に呼ばれるもの
    /// </summary>
    private void OnJoinMatch(bool success, string extendedInfo, MatchInfo matchInfo) {
        if (success) {
            //成功
            NetworkManager.singleton.StartClient(matchInfo);
        } else {
            //失敗
            Debug.Log("失敗");
        }
    }
    /// <summary>
    /// マッチ制作ボタン用
    /// </summary>
    public void MatchCreateButton() {
        InputField input = GameObject.Find("RoomNameInputField").GetComponent<InputField>();
        NetworkManager.singleton.matchMaker.CreateMatch(input.text, 7 , true, "" , "" , "" , 0 , 0 , OnCreateMatch);
    }

    /// <summary>
    /// マッチ接続ボタン用
    /// </summary>
    public void MatchJoin() {
        Debug.Log(m_Matchs[0].name);
        NetworkManager.singleton.matchMaker.JoinMatch(m_Matchs[0].networkId,"","","",0,0,OnJoinMatch);
    }
}
