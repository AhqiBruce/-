﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock2D : MonoBehaviour
{
    public EatAgent eatAgent;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PrickFood")
        {
            eatAgent = GameObject.Find("EatArea/Prick").GetComponent<EatAgent>();
            eatAgent.iseatrock = true;
        }
        else if (collision.gameObject.name == "ManFood")
        {
            eatAgent = GameObject.Find("EatArea/Man").GetComponent<EatAgent>();
            eatAgent.iseatrock = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PrickFood")
        {
            eatAgent = GameObject.Find("EatArea/Prick").GetComponent<EatAgent>();
            eatAgent.iseatrock = true;
        }
        else if (collision.gameObject.name == "ManFood")
        {
            eatAgent = GameObject.Find("EatArea/Man").GetComponent<EatAgent>();
            eatAgent.iseatrock = true;
        }
    }
}
