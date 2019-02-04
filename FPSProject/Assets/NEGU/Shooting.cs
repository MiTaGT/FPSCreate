using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    public GameObject bulletprefab;
    public Transform muzzle;
    public float bulletPower = 500f;
    private float cooltime = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(ray.direction);

        RaycastHit hit;

        if (cooltime > 5)
        {
            if (Input.GetMouseButton(0))
            {
                Shot();
            }
            cooltime = 0;
        }
        else
        {
            cooltime += 1;
        }
	}

    void Shot()
    {
        var bulletInstance = GameObject.Instantiate(bulletprefab, muzzle.position, muzzle.rotation) as GameObject;
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletPower);
    }
}
