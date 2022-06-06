using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Network_Player_Spawn : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    public GameObject ground, objects, eatagent, xrrig;
    public Material skybox;
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        RenderSettings.skybox = skybox;
        ground.SetActive(true);
        objects.SetActive(true);
        eatagent.SetActive(true);
        if (PhotonNetwork.IsMasterClient)
        {
            xrrig.transform.position = new Vector3(1.67f, 1.065f, -0.785f);
            spawnedPlayerPrefab = PhotonNetwork.Instantiate("doughnut", transform.position, transform.rotation);
            xrrig.GetComponent<CharacterController>().enabled = true;
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            spawnedPlayerPrefab = PhotonNetwork.Instantiate("Dad", transform.position, transform.rotation);
            xrrig.GetComponent<CharacterController>().enabled = true;
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
