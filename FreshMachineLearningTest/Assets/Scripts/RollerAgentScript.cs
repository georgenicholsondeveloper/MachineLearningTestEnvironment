using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class RollerAgentScript : Agent
{
    public Transform Target;
    Rigidbody rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    public override void AgentReset()
    {
        if(this.transform.position.y <0)
        {
            this.rbody.angularVelocity = Vector3.zero;
            this.rbody.velocity = Vector3.zero;
            this.transform.position = new Vector3(0, 0.5f, 0);       
        }

        Target.position = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);

        FindObjectOfType<CounterScript>().countedRun += 1;
    }

    public override void CollectObservations()
    {
        AddVectorObs(Target.position);
        AddVectorObs(this.transform.position);

        AddVectorObs(rbody.velocity.x);
        AddVectorObs(rbody.velocity.y);
    }

    public float speed = 10f;

    public override void AgentAction(float[] vectorAction)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        rbody.AddForce(controlSignal * speed);

        float distanceToTarget = Vector3.Distance(this.transform.position, Target.position);


        if(distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            Done();
        }

        if(this.transform.position.y < 0)
        {
            Done();
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");

        return action;
    }


}
