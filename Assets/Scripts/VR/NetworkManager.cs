using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public EatAgent prick;
    public EatAgent man;
    public bool first;
    public VideoPlayer videoPlayer;
    double time;
    double currentTime;
    bool video = false;
    public VideoClip beforeVideo, prickvideo, manvideo;
    private GameObject spawnedPlayerPrefab;
    public GameObject ground, objects, eatagent, xrrig;
    public Material beforevideo;
    public GameObject waitText;
    public AudioSource BGM;
    // Start is called before the first frame update
    private void Awake()
    {
        time = videoPlayer.clip.length;
    }
    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Server...;");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Server.");
        base.OnConnectedToMaster();
        JoinRoom();
    }
    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && video == false)//如果房間有兩個人，影片開始播放
            {
                waitText.SetActive(false);
                videoPlayer.Play();
                video = true;
            }
        }
        if (video == true)
        {
            currentTime += Time.deltaTime;
        }
        if (currentTime >= time && first ==  false)//影片播完，遊戲前的初始化
        {
            RenderSettings.skybox = beforevideo;
            eatagent.SetActive(true);
            ground.SetActive(true);
            objects.SetActive(true);
            if (PhotonNetwork.IsMasterClient)
            {
                xrrig.transform.position = new Vector3(-0.197f, 1.065f, -0.813f);
                spawnedPlayerPrefab = PhotonNetwork.Instantiate("doughnut", transform.position, transform.rotation);
                xrrig.GetComponent<CharacterController>().enabled = true;
            }
            else if (!PhotonNetwork.IsMasterClient)
            {
                xrrig.transform.position = new Vector3(2.534f, 1.065f, -0.813f);
                spawnedPlayerPrefab = PhotonNetwork.Instantiate("Dad", transform.position, transform.rotation);
                xrrig.GetComponent<CharacterController>().enabled = true;
            }
            prick.isaction = false;
            man.isaction = false;
            first = true;
            BGM.Play(0);
        }
        if (prick.gameover == true)
        {
            GameOver("Prick");
        }
        else if (man.gameover == true)
        {
            GameOver("Man");
        }
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
    void JoinRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom("Room", roomOptions, TypedLobby.Default);
    }
    /// <summary>
    /// 遊戲結束
    /// </summary>
    /// <param name="winner">勝者</param>
    public void GameOver(string winner)
    {
        xrrig.GetComponent<CharacterController>().enabled = false;
        Destroy(GameObject.Find("doughnut(Clone)").gameObject);
        Destroy(GameObject.Find("Dad(Clone)").gameObject);
        ground.SetActive(false);
        objects.SetActive(false);
        eatagent.SetActive(false);
        if (winner == "Prick")
        {
            videoPlayer.clip = prickvideo;
            RenderSettings.skybox = beforevideo;
            videoPlayer.Play();
            Invoke("CometoStart", 20);
        }
        else if (winner == "Man")
        {
            videoPlayer.clip = manvideo;
            RenderSettings.skybox = beforevideo;
            videoPlayer.Play();
            Invoke("CometoStart", 20);
        }
    }
    /// <summary>
    /// 遊戲結束回到初始畫面
    /// </summary>
    void CometoStart()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Start");
    }
}
