using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Task that instructs ControlledAI to move away from 'tagged' player
/// </summary>
public class FleeFromTagged : Task
{
    protected NavMeshAgent agent;
    public override bool Execute()
    {
        Vector3 position = (ActorController.TaggedNow.transform.position - transform.position);       
        agent.SetDestination(transform.position - position);
        return base.Execute();
    }
}