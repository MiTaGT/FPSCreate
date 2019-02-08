using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
/// <summary>
/// プレイヤーのネットワーク上での当たり判定計算
/// レイキャスト使用
/// </summary>
public class Player_Shoot : NetworkBehaviour {
    //残り体力、同期対象
    [SyncVar(hook = "OnLifeChanged")]
    float m_life = 100;

    //ローカルプレイヤー体力表示テキスト
    Text m_MyLifeText;
    Image MyLifeFill;

    Text m_EnemyLifeText;

    private void Start() {
        //頭上体力表示テキストを取ってくる
        //ローカルプレイヤー
        if (isLocalPlayer) { 
            //見た目を無効化
            //transform.Find("Avater").gameObject.SetActive(false);
            //ローカルプレイヤーの残り体力表示テキストを取得
            m_MyLifeText = GameObject.Find("MyLifeText").GetComponent<Text>();
            MyLifeFill = GameObject.Find("MyLifeFill").GetComponent<Image>();
        } else {
            //他人の時
            m_EnemyLifeText = GameObject.Find("EnemyLifeText").GetComponent<Text>();
        }
        //途中参加用の表示を合わせる
        OnLifeChanged(m_life);
    }
    private void Update() {
        //他人だったら何もしない
        if (!isLocalPlayer) {
            return;
        }
        if (Input.GetMouseButton(0)) {
            //画面中央に向かってレイキャスト
            Ray ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);

            Debug.DrawRay(ray.origin,ray.direction * 100,Color.red,100,false);

            RaycastHit hit;
            if (Physics.Raycast(ray, out  hit)) {
                //当たったやつがPlayerなら
                if (hit.collider.CompareTag ("Player")) {
                    //撃ったことを鯖に通知
                    CmdShotPlayer(hit.collider.gameObject);
                }
            }
        }
    }
    /// <summary>
    ///ライフを減らす処理。これは鯖で作業
    /// </summary>
    [Command]
    void CmdShotPlayer(GameObject target) {
            target.GetComponent<Player_Shoot>().m_life -= 1;
        if (target.GetComponent<Player_Shoot>().m_life <= 0) {
           target.GetComponent<Player_Shoot>().Respawn();
        }
    }
    /// <summary>
    ///変化したときの処理
    ///体力表示テキストを最新のものに更新
    /// </summary>
    void OnLifeChanged(float newValue) {
        m_life = newValue;
        if (isLocalPlayer) {
            m_MyLifeText.text = m_life.ToString();
            MyLifeFill.fillAmount = m_life / 100;
        } else {
            m_EnemyLifeText.text = m_life.ToString();
        }
    }
    [Server]
    void Respawn() {
        //ID取得
        short controllerID = GetComponent<NetworkIdentity>().playerControllerId;
        //プレハブから新しいやつを生成
        GameObject newPlayerObj = Instantiate(NetworkManager.singleton.playerPrefab);
        //方向と向き指定
        newPlayerObj.transform.SetPositionAndRotation(Vector3.zero,Quaternion.identity);
        //プレイヤー権限移動
        NetworkServer.ReplacePlayerForConnection(connectionToClient,newPlayerObj,controllerID);
        //古いやつを消す
        NetworkServer.Destroy(gameObject);
    }
}
