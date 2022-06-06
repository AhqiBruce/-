using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class InteractionObjectBase : MonoBehaviour
{
    public EatAgent EatAgent;
    public bool isactivate;
    public bool ishold;
    public bool isfirst;
    protected ObjectAudioManager Audio;
    public PhotonView photonView;
    [SerializeField]
    public int charid, state, thirstynumber, peenumber, waternumber;
    public float foodnumber;
    public virtual void Start()
    {
        isactivate = false;
        ishold = false;
        isfirst = false;
        Audio = GameObject.Find("ObjectAudioManager").GetComponent<ObjectAudioManager>();
        photonView = this.GetComponent<PhotonView>();
    }
    public virtual void Update()
    {
    }
    /// <summary>
    /// 物品效果
    /// </summary>
    public virtual void Effect()
    {

    }
    /// <summary>
    /// 拿在手上
    /// </summary>
    public virtual void SelectEnter()
    {
        this.GetComponent<Collider>().isTrigger = true;
        this.GetComponent<Rigidbody>().useGravity = false;
    }/// <summary>
    /// 放掉
    /// </summary>
    public virtual void SelectOver()
    {
        this.GetComponent<Collider>().isTrigger = false;
        this.GetComponent<Rigidbody>().useGravity = true;
    }
    /// <summary>
    /// 轉換角度
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float CheckAngle(float value)
    {
        float angle = value - 180;
        if (angle > 0)
        {
            return angle - 180;
        }
        return angle + 180;
    }
    public float Xposition(float orginx)
    {
        float inputminx = 0;
        float inputmaxx = 0;
        float minx = 0;
        float maxx = 0;
        if (EatAgent.gameObject.name == "Prick")
        {
            inputminx = 1.6f;
            inputmaxx = 2.25f;
            minx = 14f;
            maxx = 18.57f;

            if (orginx < inputminx)
            {
                orginx = inputminx;
            }
            else if (orginx > inputmaxx)
            {
                orginx = inputmaxx;
            }
            return (maxx - minx) / (inputmaxx - inputminx) * (orginx - inputminx) + minx;
        }
        else if (EatAgent.gameObject.name == "Man")
        {
            inputminx = 0.1f;
            inputmaxx = 0.74f;
            minx = 5.09f;
            maxx = 9.7f;
            if (orginx < inputminx)
            {
                orginx = inputminx;
            }
            else if (orginx > inputmaxx)
            {
                orginx = inputmaxx;
            }
            return (maxx - minx) / (inputmaxx - inputminx) * (orginx - inputminx) + minx;
        }
        return 0;
    }
    public float YPosition(float orginy)
    {
        float inputminy = 2.4f;
        float inputmaxy = 2.55f;
        float miny = -88.7f;
        float maxy = -88f;
        if (orginy < inputminy)
        {
            orginy = inputminy;
        }
        else if (orginy > inputmaxy)
        {
            orginy = inputmaxy;
        }
        return (maxy - miny) / (inputmaxy - inputminy) * (orginy - inputminy) + miny;
    }
}
