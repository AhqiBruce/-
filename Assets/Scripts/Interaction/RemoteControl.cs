using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteControl : InteractionObjectBase
{
    public override void Effect()
    {
        base.Effect();
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
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Air_Condition")
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red);
            }
        }
    }
}
