using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootNode : Node
{
    private NavMeshAgent agent;
    private EnemyAI enemyAI;

    public ShootNode(NavMeshAgent agent, EnemyAI enemyAI)
    {
        this.agent = agent;
        this.enemyAI = enemyAI;
    }

    public override NodeState Evaluate()
    {
        agent.isStopped = true;
        enemyAI.SetColor(Color.green);
        return NodeState.RUNNING;
    }
}
