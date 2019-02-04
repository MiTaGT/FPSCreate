using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
/// <summary>
/// ログインのときの告知を表示する
/// </summary>
public class LoginText : NetworkBehaviour {
    //履歴のコンポーネント
    Text m_ChatHistory;

    private void Start() {
        //履歴表示用Textを取得
        m_ChatHistory = GameObject.Find("HistoryText").GetComponent<Text>();
    }
    public override void OnStartLocalPlayer() {
        CmdPost(this.transform.name);
    }

    //文字列を鯖へ送信
    [Command]
    void CmdPost(string text) {
        RpcPost(text);
    }
    //履歴にチャットを追加
    [ClientRpc]
    void RpcPost(string text) {
        m_ChatHistory.text += text + "がゲームに参加しました。" + System.Environment.NewLine;
    }
}
