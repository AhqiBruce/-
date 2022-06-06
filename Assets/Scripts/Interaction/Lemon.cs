using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemon : InteractionObjectBase
{
    public WaterBottle waterBottle;
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (waterBottle != null)
        {
            if (waterBottle.eatAgent.waternumber > 0 && waterBottle.isactive == false)
            {
                if (isfirst == false)
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
        waterBottle.state = Liquid.lemon.GetHashCode();
        waterBottle.islemon = true;
        isactivate = false;
        Destroy(this.gameObject, 0.2f);
    }

    public override void SelectEnter()
    {
        base.SelectEnter();
    }
    public override void SelectOver()
    {
        base.SelectOver();
    }
}
