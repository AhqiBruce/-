using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.UI;

public class JuggleAgent : Agent
{
    public Rigidbody ballrg;
    Rigidbody player;
    public float speed;
    float diff = 0.0f;
    float previousDiff = 0.0f;
    float previousY = 5.0f;
    bool collied = false;
    Text scoretext;
    public override void Initialize()
    {
        base.Initialize();
        player = this.GetComponent<Rigidbody>();
        if (this.GetComponent<BehaviorParameters>().TeamId == 0)
        {
            scoretext = GameObject.Find("Canvas/Blue/ScoreText").GetComponent<Text>();
        }
        else if (this.GetComponent<BehaviorParameters>().TeamId == 1)
        {
            scoretext = GameObject.Find("Canvas/Yellow/ScoreText").GetComponent<Text>();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
        sensor.AddObservation(ballrg.transform.localPosition);
        sensor.AddObservation(ballrg.velocity);
        sensor.AddObservation(ballrg.transform.rotation);
        sensor.AddObservation(ballrg.angularVelocity);

        sensor.AddObservation(player.transform.localPosition);
        sensor.AddObservation(player.velocity);
        sensor.AddObservation(player.transform.rotation);
        sensor.AddObservation(player.angularVelocity);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        base.OnActionReceived(actions);
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actions.ContinuousActions[0];
        controlSignal.z = actions.ContinuousActions[1];
        if (player.transform.localPosition.y == 1)
        {
            controlSignal.y = actions.ContinuousActions[2] * 10.0f;
        }
        player.AddForce(controlSignal * speed);

        diff = ballrg.transform.localPosition.y - previousY;
        if (diff > 0.0f && previousDiff < 0.0f && collied)
        {
            AddReward(0.1f);
            scoretext.text = GetCumulativeReward().ToString();
        }
        collied = false;
        previousDiff = diff;
        previousY = ballrg.transform.localPosition.y;

        if (ballrg.transform.localPosition.y < 1f)
        {
            EndEpisode();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody == ballrg)
        {
            collied = true;
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        base.Heuristic(actionsOut);
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
        continuousActions[2] = Input.GetAxis("Jump");
    }
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        SetReward(0);
        scoretext.text = "0";
        ballrg.transform.localPosition = new Vector3(Random.value * 10 - 5, 8.0f, Random.value * 10 - 5);
        ballrg.velocity = Vector3.zero;
        ballrg.rotation = Quaternion.Euler(Vector3.zero);
        ballrg.angularVelocity = Vector3.zero;

        if (this.GetComponent<BehaviorParameters>().TeamId == 0)
        {
            player.transform.localPosition = new Vector3(-5, 1, 0 );
        }
        else if (this.GetComponent<BehaviorParameters>().TeamId == 1)
        {
            player.transform.localPosition = new Vector3(5, 1, 0);
        }
        
        player.velocity = Vector3.zero;
        player.rotation = Quaternion.Euler(Vector3.zero);
        player.angularVelocity = Vector3.zero;

        diff = 0;
        previousDiff = 0;
        previousY = 5;
        collied = false;
    }
}
