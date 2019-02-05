using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController_Recoile_mitagt : MonoBehaviour {

    public GameObject bulletprefab;
    public Transform muzzle;
    public float bulletPower; //弾を飛ばす強さ
    private float cooltime; //次に打つまでの時間
    public float nextbullet; //次に玉を打てる時間
    public int bulletnum; //弾の数
    bool fring = false; //銃を撃ったかの判定

    //リコイル関係の変数
    public float shotpoint;//撃った地点、および自動リコイル制御で戻す場所
    public float shockup;//累積でどれだけ跳ね上がったか
    public float maxshockup;
    public float backspeed = 1;
    public float gunshockup;

    AudioSource A_source;
    public AudioClip A_clip;

	// Use this for initialization
	void Start () {

        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;

        A_source = GetComponent<AudioSource>();//自分自身の音声コンポーネントを取る
    }
	
	// Update is called once per frame
	void Update () {

        if (fring == true)
        {
            shotShockReturn();
        }

        if (Input.GetMouseButton(1))//エイムの切り替え
        {
            this.transform.localPosition = new Vector3(0.0f, -0.3f, 0.8f);
        }
        else
        {
            this.transform.localPosition = new Vector3(0.5f, -0.3f, 0.8f);
        }

        //RaycastHit hit;
        Debug.Log(this.transform.parent.transform.localEulerAngles);
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
                    shotShock();
                    fring = true;
                    A_source.PlayOneShot(A_clip);//音を流す処理
                    
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

    void shotShock()//銃を撃った時のリコイル処理
    {

        if (maxshockup > 0)
        {

        }
        else
        {

            shotpoint = transform.parent.transform.localEulerAngles.x;//発射角度を記憶しておく
            maxshockup += 5;
        }
        this.transform.parent.transform.Rotate(-5, 0, 0);//銃の反動
        shockup += 5;

        int n = Random.Range(-5, 5);//横ブレ用
        this.transform.localEulerAngles = new Vector3(transform.localRotation.x, 0, 0);//変なぶれ方をしないようにいったん横ブレリセット
        this.transform.Rotate(-5, n, 0);//銃の反動
        gunshockup += 5;//数値分跳ね上がらせる

    }

    void shotShockReturn()//リコイルで跳ね上がった分が自動的に戻ってくる処理
    {
        //プレイヤーの視点を戻す
        if (shockup > 0)
        {
            this.transform.parent.transform.Rotate(4 * Time.deltaTime * backspeed, 0, 0);//反動の角度を戻す
            shockup -= 4 * Time.deltaTime * backspeed;
            backspeed += 50 * Time.deltaTime;//戻すスピードは加速させる、リコイルが強い銃だと戻ってくるのが遅くなってしまうため
        }
        else
        {
            this.transform.parent.transform.localEulerAngles = new Vector3(shotpoint, transform.parent.transform.localRotation.y, transform.parent.transform.localRotation.z);
            maxshockup = 0;
            shockup = 0;
            backspeed = 1;
            fring = false;

        }

        //跳ね上がった銃を戻す
        if (gunshockup > 0)
        {
            this.transform.Rotate(10 * Time.deltaTime * backspeed, 0, 0);//反動の角度を戻す
            gunshockup -= 10 * Time.deltaTime * backspeed;
        }
        else
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
            gunshockup = 0;

        }
    }
}
