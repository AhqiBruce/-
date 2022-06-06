using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class AnimControl : MonoBehaviour
{
    Animator anim;
    EatAgent eatAgent;
    public WaterBottle waterBottle;
    public GameObject interactionrange;
    bool issweat, eatrock, issock;
    public AudioManager Audio;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponent<Animator>();
        eatAgent = transform.GetComponent<EatAgent>();
        Audio = this.GetComponent<AudioManager>();
    }
    /// <summary>
    /// 吃飯動畫開始
    /// </summary>
    public void EatStart()
    {
        waterBottle.bottle.enabled = true;
        if (eatAgent.issweat == true)
        {
            issweat = true;
        }
        if (eatAgent.iseatrock == true)
        {
            eatrock = true;
            PhotonNetwork.Destroy(GameObject.Find("rock(Clone)").gameObject);
        }
        if (eatAgent.issock == true)
        {
            issock = true;
            eatAgent.issock = false;
        }
        if (eatAgent.isjoke == true)
        {
            eatAgent.Anim("Look_Joke");
            eatAgent.isjoke = false;
        }
        else if (eatAgent.islollipop == true)
        {
            eatAgent.Anim("Lollipop");
            eatAgent.isspoon = true;
            eatAgent.islollipop = false;
        }
        else if (eatAgent.issnack == true)
        {
            eatAgent.Anim("snackEat");
            eatAgent.issnack = false;
        }
    }
    /// <summary>
    /// 吃飯動畫結束
    /// </summary>
    public void EatOver()
    {
        float eat = 0;
        if (eatAgent.Thirstynumber > 80)
        {
            eat = 0.5f;
            eatAgent.AddReward(0.01f);
        }
        else if (eatAgent.Thirstynumber > 50)
        {
            eat = 1;
            eatAgent.AddReward(0.02f);
        }
        else if (eatAgent.Thirstynumber > 20)
        {
            eat = 1.5f;
            eatAgent.AddReward(0.03f);
        }
        else if (eatAgent.Thirstynumber >= 0)
        {
            eat = 2;
            eatAgent.AddReward(0.05f);
        }
        eatAgent.foodnumber -= eat;
        eatAgent.Thirstynumber += 10;
        if (issweat == true)
        {
            eatAgent.Anim("Sweat");
            issweat = false;
        }
        else if (eatrock == true)
        {
            eatAgent.Anim("eatrock");
            eatrock = false;
        }
        else if (issock == true)
        {
            eatAgent.Anim("Smell_socks");
            issock = false;
        }
        else
        {
            anim.SetBool("Eat", false);
            eatAgent.isaction = false;
        }
    }
    /// <summary>
    /// 吃飯吃很快動畫結束
    /// </summary>
    public void QuickEatOver()
    {
        float eat = 0;
        if (eatAgent.Thirstynumber > 80)
        {
            eat = 0.5f;
            eatAgent.AddReward(0.01f);
        }
        else if (eatAgent.Thirstynumber > 50)
        {
            eat = 1;
            eatAgent.AddReward(0.02f);
        }
        else if (eatAgent.Thirstynumber > 20)
        {
            eat = 1.5f;
            eatAgent.AddReward(0.03f);
        }
        else if (eatAgent.Thirstynumber >= 0)
        {
            eat = 2;
            eatAgent.AddReward(0.05f);
        }
        eatAgent.foodnumber -= eat;
        eatAgent.Thirstynumber += 10;
        if (issweat == true)
        {
            eatAgent.Anim("Sweat");
            issweat = false;
        }
        else if (eatrock == true)
        {
            eatAgent.Anim("eatrock");
            eatrock = false;
        }
        else if (issock == true)
        {
            eatAgent.Anim("Smell_socks");
            issock = false;
        }
    }
    public void QuickEatOver3()
    {
        anim.SetBool("quickEat", false);
        eatAgent.isaction = false;
    }
    /// <summary>
    /// 用大湯匙吃飯動畫結束
    /// </summary>
    public void EatSpoonOver()
    {
        float eat = 0;
        if (eatAgent.Thirstynumber > 80)
        {
            eat = 0.6f;
            eatAgent.AddReward(0.01f);
        }
        else if (eatAgent.Thirstynumber > 50)
        {
            eat = 1.2f;
            eatAgent.AddReward(0.02f);
        }
        else if (eatAgent.Thirstynumber > 20)
        {
            eat = 1.8f;
            eatAgent.AddReward(0.03f);
        }
        else if (eatAgent.Thirstynumber >= 0)
        {
            eat = 2.4f;
            eatAgent.AddReward(0.05f);
        }
        eatAgent.foodnumber -= eat;
        eatAgent.Thirstynumber += 12;
        if (issweat == true)
        {
            eatAgent.Anim("Sweat");
            issweat = false;
        }
        else if (eatrock == true)
        {
            eatAgent.Anim("eatrock");
            eatrock = false;
        }
        else if (issock == true)
        {
            eatAgent.Anim("Smell_socks");
            issock = false;
        }
        else
        {
            anim.SetBool("spoon", false);
            eatAgent.isaction = false;
        }
    }
    /// <summary>
    /// 喝水動畫開始
    /// </summary>
    public void DrinkStart()
    {
        interactionrange.SetActive(false);
        waterBottle.bottle.enabled = false;
        waterBottle.Drink();
    }
    /// <summary>
    /// 喝水動畫結束
    /// </summary>
    /// <param name="liquid"></param>
    public void DrinkOver(string liquid)
    {
        interactionrange.SetActive(true);
        waterBottle.bottle.enabled  = true;
        eatAgent.Thirstynumber -= 9;
        eatAgent.waternumber -= 10;
        eatAgent.peenumber += 5;
        anim.SetBool(liquid, false);
        if (liquid == "Drink_Soda")
        {
            if (eatAgent.waternumber <= 0)
            {
                eatAgent.Anim("Hiccup");
                return;
            }
        }
        else if (liquid == "Drink_Wine")
        {
            if (eatAgent.waternumber <= 0)
            {
                eatAgent.Anim("Get_drunk");
                return;
            }
        }
        else if (liquid == "Drink_Lemon")
        {
            if (eatAgent.waternumber <= 0)
            {
                waterBottle.Resetbottle();
            }
        }
        else if (liquid == "Drink_vitamin")
        {
            if (eatAgent.waternumber <= 0)
            {
                waterBottle.Resetbottle();
            }
        }
        eatAgent.isaction = false;
    }
    /// <summary>
    /// 盛水動畫結束
    /// </summary>
    public void FillWaterOver()
    {
        interactionrange.SetActive(true);
        waterBottle.bottle.enabled = true;
        waterBottle.FillWater();
        eatAgent.waternumber = 100;
        anim.SetBool("FillWater", false);
        eatAgent.isaction = false;
    }
    /// <summary>
    /// 胡椒鹽飯
    /// </summary>
    public void PeeOver()
    {
        anim.SetBool("Pee", false);
        eatAgent.peenumber = 0;
        eatAgent.isaction = false;
    }
    /// <summary>
    /// 吃到辣動畫結束
    /// </summary>
    public void sweartOver()
    {
        anim.SetBool("Sweat", false);
        eatAgent.issweat = false;
        eatAgent.isaction = false;
        eatAgent.issweat = false;
        eatAgent.Foods = Food.none.GetHashCode();
    }
    /// <summary>
    /// 吃到石頭動畫結束
    /// </summary>
    public void eatrockOver()
    {
        anim.SetBool("eatrock", false);
        eatAgent.isaction = false;
        eatAgent.iseatrock = false;
    }
    public void Drop_socks()
    {
        Destroy(GameObject.Find("sock(Clone)").gameObject);
    }
    public void Smell_socksOver()
    {
        anim.SetBool("Smell_socks", false);
        eatAgent.isaction = false;
        eatAgent.issock = false;
    }
    /// <summary>
    /// 刪除笑話本
    /// </summary>
    public void Drop_JokeBook()
    {
        Destroy(GameObject.Find("JokeBook(Clone)").gameObject);
    }
    /// <summary>
    /// 吃胡椒鹽飯結束
    /// </summary>
    public void EatpepperOver()
    {
        float eat = 0;
        if (eatAgent.Thirstynumber > 80)
        {
            eat = 0.5f;
            eatAgent.AddReward(0.01f);
        }
        else if (eatAgent.Thirstynumber > 50)
        {
            eat = 1;
            eatAgent.AddReward(0.02f);
        }
        else if (eatAgent.Thirstynumber > 20)
        {
            eat = 1.5f;
            eatAgent.AddReward(0.03f);
        }
        else if (eatAgent.Thirstynumber >= 0)
        {
            eat = 2;
            eatAgent.AddReward(0.05f);
        }
        eatAgent.foodnumber -= eat;
        eatAgent.Thirstynumber += 10;
        eatAgent.ispepper = false;
        eatAgent.isaction = false;
        eatAgent.Foods = Food.none.GetHashCode();
    }
    /// <summary>
    /// 喝到維他命水動畫結束
    /// </summary>
    public void EatIaxativeOver()
    {
        waterBottle.islaxative = false;
        waterBottle.state = Liquid.water.GetHashCode();
    }
    /// <summary>
    /// 羽毛筆動畫結束
    /// </summary>
    public void QuillpenOver()
    {
        eatAgent.isquillpen = false;
        eatAgent.anim.SetBool("quill pen", false);
        ActionOver();
    }
    /// <summary>
    /// 驚嚇動畫結束
    /// </summary>
    public void ShockOver()
    {
        eatAgent.isshock = false;
        eatAgent.anim.SetBool("shock", false);
        ActionOver();
    }
    /// <summary>
    /// 棒棒糖動畫結束
    /// </summary>
    public void LollipopOver()
    {
        eatAgent.anim.SetBool("Lollipop", false);
        ActionOver();
    }
    /// <summary>
    /// 蚊子動畫結束
    /// </summary>
    public void MosquitoOver()
    {
        eatAgent.ismosquito = false;
        eatAgent.anim.SetBool("mosquito", false);
        ActionOver();
    }
    /// <summary>
    /// 吃零食動畫結束
    /// </summary>
    public void snackEatOver()
    {
        eatAgent.issnack = false;
        eatAgent.anim.SetBool("snackEat", false);
        ActionOver();
    }
    /// <summary>
    /// 刪除零食
    /// </summary>
    public void Drop_Snack()
    {
        Destroy(GameObject.Find("snack(Clone)").gameObject);
    }
    /// <summary>
    ///播放獲勝的音效
    /// </summary>
    public void WinStart()
    {
        eatAgent.ReactAudios(ReactAudio.win);
    }
    /// <summary>
    /// 水杯與玩家動作結束
    /// </summary>
    public void ActionOver()
    {
        waterBottle.bottle.enabled = true;
        eatAgent.isaction = false;
    }
}
