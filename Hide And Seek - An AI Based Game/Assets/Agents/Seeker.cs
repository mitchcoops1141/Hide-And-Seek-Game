using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Seeker : Agent
{
    public float speed = 5f;

    float cacheSpeed;

    private void Start()
    {
        cacheSpeed = speed;
        speed = 0;
    }

    public void BeginSeeking()
    {
        speed = cacheSpeed;
    }

    public void StopSeeking()
    {
        cacheSpeed = speed;
        speed = 0;
    }

    public override void OnEpisodeBegin()
    {
        //transform.localPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-6f, 6f));
        //target.localPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-6f, 6f));

        //transform.localPosition = Vector3.zero;
        //target.position = new Vector3(8, 0, 6);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        if (LevelManager.instance.player)
            sensor.AddObservation(LevelManager.instance.player.transform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * speed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            //AddReward(+1f);
        }

        if (other.tag == "Wall")
        {
            //AddReward(-1f);
        }
    }
}
