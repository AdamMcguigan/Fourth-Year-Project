using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCoveredNode : Node
{
    private Transform target;
    private Transform origin;

    public IsCoveredNode(Transform target, Transform origin)
    {
        this.target = target;
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        //ask if the hit object is our target, if not then that then theres something separating the ai
        //from the player which returns Success, otherwise return failure
        RaycastHit hit;

        if(Physics.Raycast(origin.position, target.position - origin.position, out hit))
        {
            if(hit.collider.transform != target)
            {
                return NodeState.SUCCESS;
            }
        }

        return NodeState.FAILURE;
    }
}
