using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coca_cola : InteractionObjectBase
{
    public WaterBottle waterBottle;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        photonView = this.GetComponent<PhotonView>();
    }

    public override void Update()
    {
        base.Update();
        if (this.transform.eulerAngles.x > 300)
        {
            isactivate = true;
        }
        else
        {
            isactivate = false;
        }
        if (waterBottle != null)
        {
            if (waterBottle.eatAgent.waternumber < 90 && waterBottle.eatAgent.waternumber > 0 && waterBottle.isactive == false)
            {
                if (isfirst == false && isactivate == true)
                {
                    Effect();
                    isfirst = true;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prickwaterbottle")
        {
            waterBottle = GameObject.Find("EatArea/Prick_waterbottle").GetComponent<WaterBottle>();
        }
        else if (other.tag == "Manwaterbottle")
        {
            waterBottle = GameObject.Find("EatArea/Man_waterbottle").GetComponent<WaterBottle>();
        }
    }
    public override void Effect()
    {
        waterBottle.state = Liquid.cola.GetHashCode();
        photonView.RPC("SetState", RpcTarget.Others, waterBottle.eatAgent.charID, waterBottle.state, waterBottle.eatAgent.Thirstynumber, waterBottle.eatAgent.peenumber, waterBottle.eatAgent.waternumber, waterBottle.eatAgent.foodnumber);
        Audio.ObjectAudio(ObjectAudio.cola);
        isactivate = false;
    }

    public override void SelectEnter()
    {
        base.SelectEnter();
    }
    public override void SelectOver()
    {
        base.SelectOver();
    }
    /// <summary>
    /// 傳送狀態
    /// </summary>
    /// <param name="CharID"></param>
    /// <param name="State"></param>
    /// <param name="Thirstynumber"></param>
    /// <param name="Peenumber"></param>
    /// <param name="Waternumber"></param>
    /// <param name="Foodnumber"></param>
    [PunRPC]
    void SetState(int CharID, int State, int Thirstynumber, int Peenumber, int Waternumber, float Foodnumber)
    {
        waterBottle.state = State;
        waterBottle.eatAgent.Thirstynumber = Thirstynumber;
        waterBottle.eatAgent.peenumber = Peenumber;
        waterBottle.eatAgent.waternumber = Waternumber;
        waterBottle.eatAgent.foodnumber = Foodnumber;
    }
}
