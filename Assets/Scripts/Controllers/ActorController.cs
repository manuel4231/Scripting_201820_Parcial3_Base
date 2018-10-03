﻿using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public abstract class ActorController : MonoBehaviour
{
    protected NavMeshAgent agent;

    [SerializeField]
    protected Color baseColor = Color.blue;

    protected Color taggedColor = Color.red;

    protected MeshRenderer renderer;

    public delegate void OnActorTagged(bool val);

    public OnActorTagged onActorTagged;

    public bool IsTagged { get; protected set; }

    public int timesTagged = 0;

    public static GameObject TaggedLast;

    public static GameObject TaggedNow;

    // Use this for initialization
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<MeshRenderer>();

        SetTagged(false);

        onActorTagged += SetTagged;

    }

    protected abstract Vector3 GetTargetLocation();

    protected void MoveActor()
    {
        agent.SetDestination(GetTargetLocation());
    }

    protected void OnCollisionEnter(Collision collision)
    {
        ActorController otherActor = collision.gameObject.GetComponent<ActorController>();

        if (otherActor != null)
        {
            if (IsTagged)
            {
                if (otherActor != TaggedLast)
                {
                    print(otherActor != TaggedLast);
                   
                    otherActor.onActorTagged(true);
                    onActorTagged(false);

                    TaggedLast = gameObject;
                }
            }
        }
    }

    protected virtual void OnDestroy()
    {
        agent = null;
        renderer = null;
        onActorTagged -= SetTagged;
    }

    private void SetTagged(bool val)
    {
        IsTagged = val;

        if (IsTagged)
        {
            renderer.material.color = taggedColor;
            TaggedNow = this.gameObject;
            timesTagged++;
        }
        else
            renderer.material.color = baseColor;
    }

    public void Die()
    {
        OnDestroy();
        agent = null;
        
    }
}