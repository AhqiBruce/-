using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 飯的狀態
/// </summary>
public enum Food
{
    none,
    chili_sauce,
    pepper_can,
    rock,
    sock,
}
public class EatAgent : Agent
{
    public int Foods;
    public int charID;
    public int Thirstynumber, peenumber , waternumber, foodindex;
    public float foodnumber;
    Slider thirsty,pee;
    Image water,food;
    public Sprite[] foods = new Sprite[11];
    public Sprite[] hotfoods = new Sprite[10];
    public Sprite[] pepperfoods = new Sprite[10];
    Text score;
    public Animator anim;
    public bool isaction, issweat, ispepper, iseatrock, issock, isjoke, isshock, isquillpen, islollipop, isspoon, ismosquito, issnack, gameover;
    public WaterBottle waterBottle;
    public AudioManager Audio;
    public EatAgent enemy;
    /// <summary>
    /// 輸入設定
    /// </summary>
    /// <param name="actionsOut"></param>
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        base.Heuristic(actionsOut);
        ActionSegment<float> actionSegment = actionsOut.ContinuousActions;

        actionSegment[0] = Input.GetKeyDown(KeyCode.E).GetHashCode(); //吃東西
        actionSegment[1] = Input.GetKeyDown(KeyCode.D).GetHashCode(); //喝水
        actionSegment[2] = Input.GetKeyDown(KeyCode.Space).GetHashCode();//裝水
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
        isaction = true;
        Thirstynumber = 0;
        peenumber = 0;
        waternumber = 100;
        foodnumber = 100;
        foodindex = 0;
        Foods = Food.none.GetHashCode();
        if (this.gameObject.name == "Prick")
        {
            charID = 1;
            thirsty = transform.parent.Find("PrickCanvas/Water/Slider").GetComponent<Slider>();
            pee = transform.parent.Find("PrickCanvas/Pee/Slider").GetComponent<Slider>();
            water = transform.parent.Find("PrickCanvas/Water/Water").GetComponent<Image>();
            food = transform.parent.Find("PrickCanvas/Food").GetComponent<Image>();
            food.sprite = foods[foodindex];
            score = transform.parent.Find("PrickCanvas/Prick/ScoreText").GetComponent<Text>();
            waterBottle = transform.parent.Find("Prick_waterbottle").GetComponent<WaterBottle>();
        }
        else if (this.gameObject.name == "Man")
        {
            charID = 2;
            thirsty = transform.parent.Find("ManCanvas/Water/Slider").GetComponent<Slider>();
            pee = transform.parent.Find("ManCanvas/Pee/Slider").GetComponent<Slider>();
            water = transform.parent.Find("ManCanvas/Water/Water").GetComponent<Image>();
            food = transform.parent.Find("ManCanvas/Food").GetComponent<Image>();
            food.sprite = foods[foodindex];
            score = transform.parent.Find("ManCanvas/Man/ScoreText").GetComponent<Text>();
            waterBottle = transform.parent.Find("Man_waterbottle").GetComponent<WaterBottle>();
        }
        anim = this.transform.GetComponent<Animator>();
        Audio = this.GetComponent<AudioManager>();
    }
    /// <summary>
    /// 收到指令處理
    /// </summary>
    /// <param name="actions"></param>
    public override void OnActionReceived(ActionBuffers actions)
    {
        base.OnActionReceived(actions);
        float iseat = 0;
        float isdrink = 0;
        float isfillwater = 0;
        if (gameover == true)
        {
            return;
        }
        if (isaction == false && PhotonNetwork.IsMasterClient == true)
        {
            if (peenumber >= 100)//上廁所
            {
                isaction = true;
                Anim("Pee");
                Debug.Log("Pee");
                return;
            }
            if (Thirstynumber < 100)//如果還沒口渴，可以吃飯
            {
                iseat = actions.ContinuousActions[0];
            }
            if (waternumber > 0 && Thirstynumber > 0)//水杯還有水且口渴
            {
                isdrink = actions.ContinuousActions[1];
            }
            if (waternumber <= 0)//如果沒有水，可以盛水
            {
                isfillwater = actions.ContinuousActions[2];
            }
            
            if (iseat == 1 && Thirstynumber < 100)//如果執行吃飯且還沒口渴
            {
                if (ispepper == true)
                {
                    isaction = true;
                    Anim("eatpepper");
                }
                else if (isspoon == true)
                {
                    isaction = true;
                    Anim("spoon");
                }
                else if (waterBottle.isvitamin == true)
                {
                    isaction = true;
                    Anim("quickEat");
                }
                else
                {
                    isaction = true;
                    Anim("Eat");
                }
                Debug.Log("Eat");
            }
            if (isdrink == 1)//執行喝水
            {
                if (waterBottle.iscola == true)
                {
                    isaction = true;
                    Anim("Drink_Soda");
                }
                else if (waterBottle.iswine == true)
                {
                    isaction = true;
                    Anim("Drink_Wine");
                }
                else if (waterBottle.islemon == true)
                {
                    isaction = true;
                    Anim("Drink_Lemon");
                }
                else if (waterBottle.isvitamin == true)
                {
                    isaction = true;
                    Anim("Drink_vitamin");
                }
                else if (waterBottle.islaxative == true)
                {
                    isaction = true;
                    Anim("eatlaxative");
                }            
                else
                {
                    isaction = true;
                    Anim("Drink");
                }
                Debug.Log("Drink");
            }
            if (isfillwater == 1)//執行盛水
            {
                AddReward(0.01f);
                isaction = true;
                Anim("FillWater");
                Debug.Log("Fill Water");
            }
        }     
        
        Refrash();//更新狀態
    }
    /// <summary>
    /// 迴圈開始的初始化
    /// </summary>
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        SetReward(0);
        score.text = "0";
        Thirstynumber = 0;
        peenumber = 0;
        waternumber = 100;
        foodnumber = 100;
        water.fillAmount = 1;
        foodindex = 0;
        food.sprite = foods[foodindex];
        food.fillAmount = 1;
    }
    /// <summary>
    /// 收集觀察數值
    /// </summary>
    /// <param name="sensor"></param>
    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
        sensor.AddObservation(food.fillAmount);
        sensor.AddObservation(thirsty.value);
        sensor.AddObservation(pee.value);
        sensor.AddObservation(water.fillAmount);
    }
    /// <summary>
    /// 更換飯的圖片
    /// </summary>
    /// <param name="foodindex"></param>
    void FoodChange(int foodindex)
    {
        if (issweat == true)
        {
            food.sprite = hotfoods[foodindex];
        }
        else if (ispepper == true)
        {
            food.sprite = pepperfoods[foodindex];
        }
        else
        {
            food.sprite = foods[foodindex];
        }
    }
    /// <summary>
    /// 更新每個狀態
    /// </summary>
    void Refrash()
    {
        if (Thirstynumber > 100)
        {
            Thirstynumber = 100;
        }
        if (Thirstynumber < 0)
        {
            Thirstynumber = 0;
        }
        if (peenumber > 100)
        {
            peenumber = 100;
        }
        if (peenumber < 0)
        {
            peenumber = 0;
        }
        if (waternumber < 0)
        {
            waternumber = 0;
        }
        score.text = (foodnumber).ToString();
        #region 吃
        if (foodnumber > 90)
        {
            FoodChange(0);
        }
        else if (foodnumber > 80)
        {
            FoodChange(1);
        }
        else if (foodnumber > 70)
        {
            FoodChange(2);
        }
        else if (foodnumber > 60)
        {
            FoodChange(3);
        }
        else if (foodnumber > 50)
        {
            FoodChange(4);
        }
        else if (foodnumber > 40)
        {
            FoodChange(5);
        }
        else if (foodnumber > 30)
        {
            FoodChange(6);
        }
        else if (foodnumber > 20)
        {
            FoodChange(7);
        }
        else if (foodnumber > 10)
        {
            FoodChange(8);
        }
        else if (foodnumber > 5)
        {
            FoodChange(9);
        }
        if (foodnumber <= 0)
        {
            food.sprite = foods[10];
            AddReward(0.1f);
            isaction = true;
            Anim("win");
            enemy.isaction = true;
            enemy.Anim("lose");
            this.enabled = false;
            enemy.enabled = false;
            EndEpisode();
            enemy.EndEpisode();
        }
        thirsty.value = (float)Thirstynumber / 100;
        pee.value = (float)peenumber / 100;
        water.fillAmount = (float)waternumber / 100;
        #endregion
    }
    /// <summary>
    /// 播放動畫
    /// </summary>
    /// <param name="animname"></param>
    public void Anim(string animname)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("shock"))
        {
            if (isshock == true)
            {
                return;
            }
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("quill pen"))
        {
            if (isquillpen == true)
            {
                return;
            }
        }
        string[] anim_name = { "Eat", "Drink", "FillWater", "Pee", "Sweat", "Drink_Soda", "Drink_Wine",
            "eatrock", "Hiccup", "Get_drunk", "Smell_socks", "Look_Joke", "Drink_Lemon", "eatlaxative",
            "eatpepper", "Drink_vitamin", "spoon", "mosquito", "quill pen", "Lollipop", "shock", "win", "quickEat", "snackEat", "lose" };
        for (int i = 0; i < anim_name.Length; i++)
        {
            if (animname == anim_name[i])
            {
                anim.SetBool(anim_name[i], true);
            }
            else
            {
                anim.SetBool(anim_name[i], false);
            }        
        }
    }
    private void Update()
    {
        HandleAction();
        if (isquillpen == true)//羽毛筆
        {
            isaction = true;
            Anim("quill pen");
        }
        if (isshock == true)//驚嚇
        {
            isaction = true;
            Anim("shock");
        }
        if (ismosquito == true)//蚊子
        {
            isaction = true;
            Anim("mosquito");
        }
    }
    /// <summary>
    /// 一般音效播放
    /// </summary>
    /// <param name="audio"></param>
    public void PlayerAudios(PlayerAudio audio)
    {
        Audio.PlayerAudio(audio);
    }

    /// <summary>
    /// 反應音效播放
    /// </summary>
    /// <param name="audio"></param>
    public void ReactAudios(ReactAudio audio)
    {
        Audio.ReactAudio(audio);
    }
    /// <summary>
    /// 遊戲結束
    /// </summary>
    public void GameOver()
    {
        gameover = true;
    }
    /// <summary>
    /// 處理飯的狀態
    /// </summary>
    public void HandleAction()
    {
        switch (Foods)
        {
            case 0:
                Debug.Log("正常飯");
                break;
            case 1:
                issweat = true;
                Debug.Log("辣椒罐");
                break;
            case 2:
                ispepper = true;
                Debug.Log("胡椒罐");
                break;
            default:
                break;
        }
    }
}
