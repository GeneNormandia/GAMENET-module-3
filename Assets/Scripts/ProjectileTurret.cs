using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ProjectileTurret : MonoBehaviour
{
    public GameObject bulletPreFab;

    // Start is called before the first frame update
    void Start()
    {
        //base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*protected override void FixedUpdate()
    {
        //base.FixedUpdate();

        if (isControlEnabled)
        {
            if (fireTimer < fireRate)
            {
                fireTimer += Time.deltaTime;

            }

            if (Input.GetButton("Fire1") && fireTimer > fireRate)
            {
                Fire();
                fireTimer = 0;
            }
        }
    }

    public override void Fire()
    {
        //base.Fire();
        if (photonView.IsMine)
        {
            photonView.RPC("DisplayBullets", RpcTarget.All, gunNozzle.position, photonView.IsMine);
        }


    }

    [PunRPC]
    public void DisplayBullets(Vector3 gunNozzle, bool playerOrigin)
    {
        GameObject bulletGameObject = Instantiate(bulletPreFab, gunNozzle, Quaternion.identity);
        bulletGameObject.GetComponent<Projectile>().bulletOwner = playerOrigin;
        bulletGameObject.GetComponent<Projectile>().gameObjectBulletOwner = this.gameObject;
    }*/
}
