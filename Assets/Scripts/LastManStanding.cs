using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;

public class LastManStanding : MonoBehaviourPunCallbacks
{
    private Shooting shooting;

    public GameObject playerUiPrefab;

    public enum RaiseEventsCode
    {
        WhoDiedEventCode = 0
    }

    private int deathOrder = 0;

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    void OnEvent(EventData photonEvent)
    {
        //checking if the event that has been sent was for the whodied event 
        if (photonEvent.Code == (byte)RaiseEventsCode.WhoDiedEventCode)
        {
            //now we retrieve data that we passed for our event
            object[] data = (object[])photonEvent.CustomData;

            string deadPlayerName = (string)data[0];
            deathOrder = (int)data[1];
            int viewId = (int)data[2];

            Debug.Log(deadPlayerName + " " + deathOrder);

            GameObject orderUiText = DeathRaceGameManager.instance.finisherTextUi[deathOrder - 1];
            orderUiText.SetActive(true);
           
        }
    }
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDied()
    {
        deathOrder++;

        string nickName = photonView.Owner.NickName;
        int viewId = photonView.ViewID;

        //event data 
        object[] data = new object[] { nickName, deathOrder, viewId };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache
        };

        SendOptions sendOption = new SendOptions
        {
            Reliability = false
        };

        PhotonNetwork.RaiseEvent((byte)RaiseEventsCode.WhoDiedEventCode, data, raiseEventOptions, sendOption);
    }
}
