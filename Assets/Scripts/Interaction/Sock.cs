using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sock : InteractionObjectBase
{
    public override void Effect()
    {
        base.Effect();
        if (PhotonNetwork.IsMasterClient == true)
        {
            PhotonNetwork.Instantiate("sock", new Vector3(Xposition(this.transform.position.x), YPosition(this.transform.position.y), -116.95f), Quaternion.Euler(40, 0, 50));
        }

    }

    public override void SelectEnter()
    {
        base.SelectEnter();
    }

    public override void SelectOver()
    {
        base.SelectOver();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (GameObject.Find("sock(Clone)"))
        {
            EatAgent.issock = true;
            Destroy(this.gameObject);
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
        if (EatAgent != null)
        {
            if (isfirst == false)
            {
                Effect();
                isfirst = true;
            }
        }
    }
}
