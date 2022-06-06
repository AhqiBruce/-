using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quillpen : InteractionObjectBase
{
    public override void Effect()
    {
        base.Effect();
        EatAgent.isaction = true;
        EatAgent.isquillpen = true;
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PrickFace")
        {
            EatAgent = GameObject.Find("EatArea/Prick").GetComponent<EatAgent>();
        }
        else if (other.tag == "ManFace")
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
