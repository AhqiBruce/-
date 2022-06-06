using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepper_can : InteractionObjectBase
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (EatAgent != null)
        {
            if (isfirst == false)
            {
                Effect();
                isfirst = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PrickFood")
        {
            EatAgent = GameObject.Find("EatArea/Prick").GetComponent<EatAgent>();
        }
        else if (other.tag == "ManFood")
        {
            EatAgent = GameObject.Find("EatArea/Man").GetComponent<EatAgent>();
        }
   
    }
    public override void Effect()
    {
        EatAgent.Foods = Food.pepper_can.GetHashCode();
        photonView.RPC("SetState", RpcTarget.Others, EatAgent.charID, EatAgent.Foods, EatAgent.Thirstynumber, EatAgent.peenumber, EatAgent.waternumber, EatAgent.foodnumber);
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
        EatAgent.Foods = State;
        EatAgent.Thirstynumber = Thirstynumber;
        EatAgent.peenumber = Peenumber;
        EatAgent.waternumber = Waternumber;
        EatAgent.foodnumber = Foodnumber;
    }
}
