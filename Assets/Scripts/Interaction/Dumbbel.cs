using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumbbel : InteractionObjectBase
{
    EatAgent player2;
    bool active;
    public override void Effect()
    {
        base.Effect();
        EatAgent.isaction = true;
        player2.isaction = true;
        EatAgent.isshock = true;
        player2.isshock = true;
    }

    public override void SelectEnter()
    {
        base.SelectEnter();
    }

    public override void SelectOver()
    {
        base.SelectOver();
        Audio.ObjectAudio(ObjectAudio.dumbbel);
    }

    public override void Start()
    {
        base.Start();
        EatAgent = GameObject.Find("EatArea/Prick").GetComponent<EatAgent>();
        player2 = GameObject.Find("EatArea/Man").GetComponent<EatAgent>();
    }

    public override void Update()
    {
        base.Update();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (isfirst == false)
            {
                Effect();
                isfirst = true;
            }
        }
    }
}
