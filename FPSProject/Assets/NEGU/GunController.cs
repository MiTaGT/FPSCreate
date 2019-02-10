using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public GameObject Camera;
    public GameObject bulletprefab;
    public Transform muzzle;
    public float bulletPower; //弾を飛ばす強さ
    private float cooltime; //次に打つまでの時間
    public float nextbullet; //次に玉を打てる時間
    public int bulletnum; //弾の数
    bool fring = false; //銃を撃ったかの判定

	// Use this for initialization
	void Start () {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (fring == true)
        {
            this.transform.Rotate(10, 0, 0);//反動の角度を戻す
            Camera.transform.Rotate(1.5f, 0, 0);
            fring = false;
        }

        if (Input.GetMouseButton(1))//エイムの切り替え
        {
            this.transform.localPosition = new Vector3(0.0f, -0.18f, 0.55f);
        }
        else
        {
            this.transform.localPosition = new Vector3(0.5f, -0.3f, 0.8f);
        }

        //RaycastHit hit;

        //銃を撃つ処理
        if (cooltime > nextbullet)
        {
            if (Input.GetMouseButton(0))
            {
                if (bulletnum > 0)
                {
                    Shot();//弾を打つ
                    bulletnum -= 1;//残段数を減らす
                    cooltime = 0; //次に玉を打てる時間の更新
                    this.transform.Rotate(-10, 0, 0);//銃の反動
                    Camera.transform.Rotate(-1.5f, 0, 0);
                    fring = true;
                }
            }
        }
        else
        {
            cooltime += 1;//時間の更新
        }

        //リロード
        if (Input.GetKeyDown("r"))
        {
            bulletnum = 7;
        }
	}

    void Shot()//銃を撃つ処理
    {
        var bulletInstance = GameObject.Instantiate(bulletprefab, muzzle.position, muzzle.rotation) as GameObject;
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletPower);
    }
}
