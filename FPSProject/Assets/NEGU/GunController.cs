using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public GameObject bulletprefab;
    public Transform muzzle;
    public float bulletPower;
    private float cooltime = 1;
    public int bulletnum = 7;

	// Use this for initialization
	void Start () {

        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            this.transform.position = new Vector3(0.0f, -0.3f, 0.8f);
        }
        else
        {
            this.transform.position = new Vector3(0.5f, -0.3f, 0.8f);
        }


        RaycastHit hit;

        //銃を撃つ処理
        if (cooltime > 7)
        {
            if (Input.GetMouseButton(0))
            {
                if (bulletnum > 0)
                {
                    Shot();
                    bulletnum -= 1;
                    cooltime = 0;
                }
            }
        }
        else
        {
            cooltime += 1;
        }

        if (Input.GetKeyDown("r"))
        {
            bulletnum = 7;
        }
	}

    void Shot()
    {
        var bulletInstance = GameObject.Instantiate(bulletprefab, muzzle.position, muzzle.rotation) as GameObject;
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletPower);
    }
}
