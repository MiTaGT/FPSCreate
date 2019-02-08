using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MYNetworkDiscovery : NetworkDiscovery {

    public override void OnReceivedBroadcast(string fromAddress, string data) {
        //見つけたアドレスを鯖アドレスに入れる
        NetworkManager.singleton.networkAddress = fromAddress;
        //既に接続済みなら何もしない。
        if (NetworkManager.singleton.IsClientConnected() == true) {
            return;
        }
        Debug.LogFormat("OnReceivedBroadcast;fromAddress:{0},data{1}",fromAddress,data);
        NetworkManager.singleton.StartClient();
    }
}
