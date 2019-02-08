using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


/// <summary>
/// プレイヤーIDを設定する
/// </summary>
public class Set_Player_ID : NetworkBehaviour {

    [SyncVar] private string PlayerUniqueIdentity;

    private NetworkInstanceId playerNetID;
    private Transform mypos;

    public override void OnStartLocalPlayer() {
            GetNetIdentity();
            SetIdentity();
    }
    private void Awake() {
        mypos = transform;
    }
    private void Update() {
        if (mypos.name == "" || mypos.name == "Player(Clone)") {
            SetIdentity();
        }
    }
    [Client]
    void GetNetIdentity() {
        //NetID取得
        playerNetID = GetComponent<NetworkIdentity>().netId;
        //名前をつけます
        CmdTellServerMyIdentity(MakeUniqueIdentity());
        Debug.Log("プレイヤーID：" + playerNetID);
    }
    void SetIdentity() {
        //自分以外のプレイヤーの時
        if (!isLocalPlayer) {
            //名前を変えない
            Debug.Log("名前を変えません…");
            mypos.name = PlayerUniqueIdentity;
        } else {
            //自分の時
            Debug.Log("名前を変更します…");
            mypos.name = MakeUniqueIdentity();
        }
    }
    string MakeUniqueIdentity() {
        //Player + NetIDで名前設定
        string UniqueName = "Player" + playerNetID.ToString();
        Debug.Log("生成した名前：" + UniqueName);
        return UniqueName;
    }
    /// <summary>
    /// Sync変数変更後、結果を全クライアントへ
    /// </summary>
    [Command]
    void CmdTellServerMyIdentity(string name) {
        Debug.Log("この名前を設定しました" + name);
        PlayerUniqueIdentity = name;
    }
}
