using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TextMeshProUGUI playerNameText;

    public Camera camera;

    private Shooting shooting;

    void Awake()
    {
         
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerNameText.gameObject.SetActive(!photonView.IsMine);

        //DeathRaceManager.instance.playersLeft.Add(photonView.Owner.NickName);
        shooting = this.GetComponent<Shooting>();

        this.camera = transform.Find("Camera").GetComponent<Camera>();
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("rc"))
        {
            GetComponent<VehicleMovement>().enabled = photonView.IsMine;
            GetComponent<LapController>().enabled = photonView.IsMine;
            camera.enabled = photonView.IsMine;
            //this.GetComponent<Shooting>().enabled = false;
        }
        else if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("dr"))
        {
            GetComponent<VehicleMovement>().enabled = photonView.IsMine;
            GetComponent<LapController>().enabled = photonView.IsMine;
            //shooting.enabled = true;
            GetComponent<Shooting>().enabled = photonView.IsMine;
            //GetComponent<Shooting>().enabled = photonView.IsMine;
            GetComponent<LastManStanding>().enabled = photonView.IsMine;
            camera.enabled = photonView.IsMine;
        }

        playerNameText.text = photonView.Owner.NickName;
    }
}
