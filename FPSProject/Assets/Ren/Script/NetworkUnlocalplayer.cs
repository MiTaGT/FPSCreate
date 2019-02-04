using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// ローカルではないオブジェクトのコンポーネントをfalse
/// プレイヤーにつけようね
/// </summary>

public class NetworkUnlocalplayer : NetworkBehaviour {

    [SerializeField]
    Behaviour[] behaviours;

    private void Start() {
        if (!isLocalPlayer) {
            foreach (var behavior in behaviours) {
                behavior.enabled = false;
            }
        }
    }
    private void OnApplicationFocus(bool focus) {
        if (isLocalPlayer) {
            foreach (var behavior in behaviours) {
                behavior.enabled = focus;
            }
        }
    }
}
