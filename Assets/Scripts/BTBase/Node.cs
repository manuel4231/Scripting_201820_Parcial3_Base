using UnityEngine;

public abstract class Node : MonoBehaviour
{
    protected AIController ControlledAI { get; set; }

    public abstract bool Execute();

    public virtual void SetControlledAI(AIController newControlledAI)
    {
        ControlledAI = newControlledAI;
    }
}