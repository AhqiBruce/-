using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Liquid
{
    water,
    cola,
    wine,
    vitmain,
    laxative,
    lemon,
}
public class WaterBottle : MonoBehaviour
{
    public EatAgent eatAgent;
    public Sprite[] water;
    public Sprite[] cola;
    public Sprite[] wine;
    public Sprite[] vitmain;
    public Sprite[] laxative;
    public Sprite[] lemon;
    public SpriteRenderer bottle;
    public GameObject add,add_bottle;
    public bool isactive;
    public bool iscola;
    public bool iswine;
    public bool isvitamin;
    public bool islaxative;
    public bool islemon;
    public int waterbottlenumber;
    public bool iscolafirst;
    public bool iswinefirst;
    public bool isvitaminfirst;
    public bool islaxativefirst;
    public bool islemonfirst;
    public int state;
    void Start()
    {
        waterbottlenumber = 0;
        bottle = this.GetComponent<SpriteRenderer>();
        isactive = false;
        iscola = false;
        iswine = false;
        isvitamin = false;
        islaxative = false;
        islemon = false;
        iscolafirst = false;
        iswinefirst = false;
        isvitaminfirst = false;
        islaxativefirst = false;
        islemonfirst = false;
        state = Liquid.water.GetHashCode();
    }
    /// <summary>
    /// 重置水杯狀態
    /// </summary>
    public void Resetbottle()
    {
        iscola = false;
        iswine = false;
        isvitamin = false;
        islaxative = false;
        islemon = false;
        isactive = false;
        state = Liquid.water.GetHashCode();
    }
    // Update is called once per frame
    void Update()
    {
        HandleWater();
        if (eatAgent.waternumber <= 0)
        {
            Resetbottle();
        }
    }
    /// <summary>
    /// 喝水事件
    /// </summary>
    public void Drink()
    {
        waterbottlenumber = 0;
        if (eatAgent.waternumber >= 100)
        {
            waterbottlenumber = 6;
        }
        else if (eatAgent.waternumber > 80)
        {
            waterbottlenumber = 5;
        }
        else if (eatAgent.waternumber > 60)
        {
            waterbottlenumber = 4;
        }
        else if (eatAgent.waternumber > 40)
        {
            waterbottlenumber = 3;
        }
        else if (eatAgent.waternumber > 20)
        {
            waterbottlenumber = 2;
        }
        else if (eatAgent.waternumber > 10)
        {
            waterbottlenumber = 1;
        }
        else
        {
            waterbottlenumber = 0;
        }
        if (iscola == true)
        {
            bottle.sprite = cola[waterbottlenumber];
        }
        else if (iswine == true)
        {
            bottle.sprite = wine[waterbottlenumber];
        }
        else if (isvitamin == true)
        {
            bottle.sprite = vitmain[waterbottlenumber];
        }
        else if (islaxative == true)
        {
            bottle.sprite = laxative[waterbottlenumber];
        }
        else if (islemon == true)
        {
            bottle.sprite = lemon[waterbottlenumber];
        }
        else
        {
            bottle.sprite = water[waterbottlenumber];
        }
    }
    /// <summary>
    /// 裝水
    /// </summary>
    public void FillWater()
    {
        bottle.sprite = water[6];
        Resetbottle();
    }
    /// <summary>
    /// 裝可樂的動畫
    /// </summary>
    /// <returns></returns>
    public IEnumerator FillCola()
    {
        for (int i = waterbottlenumber; i < 7; i++)
        {
            bottle.sprite = cola[i];
            yield return new WaitForSeconds(0.3f);
        }
    }
    /// <summary>
    /// 裝酒的動畫
    /// </summary>
    /// <returns></returns>
    public IEnumerator FillWine()
    {
        for (int i = waterbottlenumber; i < 7; i++)
        {
            bottle.sprite = wine[i];
            yield return new WaitForSeconds(0.3f);
        }
    }
    /// <summary>
    /// 處理水杯裝什麼
    /// </summary>
    public void HandleWater()
    {
        switch (state)
        {
            case 0:
                Debug.Log("正常水");
                isactive = false;
                break;
            case 1:
                iscola = true;
                if (iscolafirst == false)
                {
                    eatAgent.isaction = true;
                    eatAgent.waternumber = 100;
                    add_bottle = PhotonNetwork.Instantiate(add.name, add.transform.position, add.transform.rotation);
                    add_bottle.SetActive(true);
                    add_bottle.GetComponent<Animator>().SetBool("cola", true);
                    Invoke("Destroyadd", 0.8f);
                    StartCoroutine(FillCola());
                    isactive = true;
                    iscolafirst = true;
                }
                Debug.Log("可樂");
                break;
            case 2:
                iswine = true;
                if (iswinefirst == false)
                {
                    eatAgent.isaction = true;
                    eatAgent.waternumber = 100;
                    add_bottle = PhotonNetwork.Instantiate(add.name, add.transform.position, add.transform.rotation);
                    add_bottle.SetActive(true);
                    add_bottle.GetComponent<Animator>().SetBool("wine", true);
                    Invoke("Destroyadd", 0.8f);
                    StartCoroutine(FillWine());
                    isactive = true;
                    iswinefirst = true;
                }
                Debug.Log("酒");
                break;
            case 3:
                isvitamin = true;
                if (isvitaminfirst == false)
                {
                    eatAgent.isaction = true;
                    bottle.sprite = vitmain[waterbottlenumber];
                    isactive = true;
                    isvitaminfirst = true;
                }
                Debug.Log("維他命");
                break;
            case 4:
                islaxative = true;
                if (islaxativefirst == false)
                {
                    eatAgent.isaction = true;
                    bottle.sprite = laxative[waterbottlenumber];
                    isactive = true;
                    islaxativefirst = true;
                }
                Debug.Log("瀉藥");
                break;
            case 5:
                islemon = true;
                if (islemonfirst == false)
                {
                    eatAgent.isaction = true;
                    bottle.sprite = lemon[waterbottlenumber];
                    isactive = true;
                    islemonfirst = true;
                }
                Debug.Log("檸檬");
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 刪除加水杯的動畫
    /// </summary>
    void Destroyadd()
    {
        if (add_bottle != null)
        {
            PhotonNetwork.Destroy(add_bottle);
        }
        eatAgent.isaction = false;
    }
}
