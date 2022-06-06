using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioButton : MonoBehaviour
{
    bool isfirst;
    EatAgent player1, player2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lhand" || other.tag == "Rhand")
        {
            if (isfirst == false)
            {
                Effect();
                isfirst = true;
            }
        }
    }
    void Effect()
    {
        player1 = GameObject.Find("EatArea/Prick").GetComponent<EatAgent>();
        player2 = GameObject.Find("EatArea/Man").GetComponent<EatAgent>();
        player1.ismosquito = true;
        player2.ismosquito = true;
    }
}
