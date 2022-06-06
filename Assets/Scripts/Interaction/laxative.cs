using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laxative : InteractionObjectBase
{
    public WaterBottle waterBottle;
    public override void Start()
    {
        base.Start();
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
            if (waterBottle.eatAgent.waternumber > 0 && waterBottle.isactive == false)
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
        waterBottle.state = Liquid.laxative.GetHashCode();
        photonView.RPC("SetState", RpcTarget.Others, waterBottle.eatAgent.charID, waterBottle.state, waterBottle.eatAgent.Thirstynumber, waterBottle.eatAgent.peenumber, waterBottle.eatAgent.waternumber, waterBottle.eatAgent.foodnumber);
        Audio.ObjectAudio(ObjectAudio.laxative);
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
