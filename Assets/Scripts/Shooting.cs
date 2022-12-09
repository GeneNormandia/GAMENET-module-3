using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;


public class Shooting : MonoBehaviourPunCallbacks
{
    
    public GameObject hitEffectPrefab;
    [Header("HP Related Stuff")]
    public float startHealth = 100;
    [SerializeField]
    private float health;
    public Image healthBar;



    [Header("Turret Stuff")]
    public Transform turretNozzle;
    public float fireRate = 0.1f;
    protected float fireTimer = 0;

    public bool isControlEnabled;

    public Camera camera;

    private LastManStanding lastManStanding;

    public Text timeText;

    public int kills;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        isControlEnabled = false;
        health = startHealth;

        lastManStanding = this.GetComponent<LastManStanding>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        RaycastHit hit;
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if (Physics.Raycast(ray, out hit, 200))
        {
            Debug.Log(hit.collider.gameObject.name);

            GameObject hitEffectGameObject = Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);
            Destroy(hitEffectGameObject, 0.2f);

            //GameObject hitEffectPrefab = Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);

            //photonView.RPC("CreateHitEffects", RpcTarget.All, hit.point);

            // AllBuffered means current and future players in room will get this broadcast function
            if (hit.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
            {
                hit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10);
                Debug.Log("TEST DAMAGE " + health);

                if (hit.collider.gameObject.GetComponent<Shooting>().health <= 0)
                {
                    this.gameObject.GetComponent<PhotonView>().RPC("Die", RpcTarget.AllBuffered);
                    //this.gameObject.GetComponent<PhotonView>().RPC("KillCounter", RpcTarget.AllBuffered);
                }
            }

        }
    }

    public void FixedUpdate()
    {

        if (isControlEnabled)
        {
            if (fireTimer < fireRate)
            {
                fireTimer += Time.deltaTime;
            }

            if (Input.GetButton("Fire1") && fireTimer > fireRate)
            {
                Debug.Log("Firing");
                Fire();
                fireTimer = 0;

            }
        }
        

    }

    [PunRPC]
    public void TakeDamage(int damage, PhotonMessageInfo info)
    {
        if (this.health <= 0) return;
        this.health -= damage;
        Debug.Log("taking damage" + damage + " " + this.health);
        if (health <= 0)
        {
            Debug.Log(info.Sender.NickName + " killed " + info.photonView.Owner.NickName);
            Die();
        }
    }

    
    public void Die()
    {
        if (photonView.IsMine)
        {
            lastManStanding.GetComponent<LastManStanding>().PlayerDied();
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("LobbyScene");
            
        }
    }
}
