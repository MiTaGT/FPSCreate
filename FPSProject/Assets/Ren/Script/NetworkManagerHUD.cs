using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
/// <summary>
/// ネットワーク接続の自作用スクリプト
/// </summary>
public class NetworkManagerHUD : MonoBehaviour {
    //主なUI
    GameObject m_MainUIs;
    //接続状態を表示するテキスト
    GameObject m_ConnectingText;
    //接続状態種別
    enum ConnctionState {
        //サーバー
        Sever,
        //ホスト
        Host,
        //リモートクライアント接続
        RemoteClientConnected,
        //リモートクライアント接続試行中
        RemoteClientConnecting,
        //接続なし
        Nothing,
    }

    /// <summary>
    /// ネットワーク接続の状態を取得
    /// </summary>
    ConnctionState GetConnectionState() {
        //サーバーが起動してる？
        if (NetworkServer.active) {
            //クライアントか
            if (NetworkManager.singleton.IsClientConnected()) {
                //ホストとして起動中！
                return ConnctionState.Host;
            } else {
                //サーバーとして起動中！
                return ConnctionState.Sever;
            }
        } else if (NetworkManager.singleton.IsClientConnected()) {
            //リモートクライアント接続！
            return ConnctionState.RemoteClientConnected;
        } else {
            NetworkClient client = NetworkManager.singleton.client;
            //Connectionが存在するか？
            if (client != null && client.connection != null && client.connection.connectionId != -1) {
                //接続試行中
                return ConnctionState.RemoteClientConnecting;
            } else {
                //接続なし
                return ConnctionState.Nothing;
            }
        }
    }

    /// <summary>
    /// スタート
    /// </summary>
    private void Start() {
        m_MainUIs = GameObject.Find("MainUIs");
        m_ConnectingText = GameObject.Find("ConnectingText");
    }

    /// <summary>
    /// アップデート
    /// </summary>
    private void Update() {
        ConnctionState state = GetConnectionState();

        //接続試行中
        if (state == ConnctionState.RemoteClientConnecting) {
            m_MainUIs.SetActive(false);
            m_ConnectingText.SetActive(true);
            m_ConnectingText.GetComponent<Text>().text = "接続中…";

            //ESCで中止
            if (Input.GetKeyDown(KeyCode.Escape)) {
                NetworkManager.singleton.StopHost();
            }
        } else {
            m_MainUIs.SetActive(true);
            m_ConnectingText.SetActive(false);
        }
    }

    /// <summary>
    /// サーバー起動ボタン用
    /// </summary>
    public void OnServerButton() {
        NetworkManager.singleton.StartServer();
    }

    /// <summary>
    /// ホスト起動用
    /// </summary>
    public void OnHostButton() {
        NetworkManager.singleton.StartHost();
    }

    /// <summary>
    /// サーバーへ接続用
    /// </summary>
    public void OnClientButton() {
        InputField input = GameObject.Find("ServerAddressInputField").GetComponent<InputField>();
        NetworkManager.singleton.networkAddress = input.text;
        NetworkManager.singleton.StartClient();
    }
}
