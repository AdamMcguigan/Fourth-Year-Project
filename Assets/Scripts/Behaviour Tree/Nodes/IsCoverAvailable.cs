using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCoverAvailable : Node
{
    private Cover[] availableCovers;
    private Transform target;
    private EnemyAI enemyAI;

    public IsCoverAvailable(Cover[] availableCovers, Transform target, EnemyAI enemyAI)
    {
        this.availableCovers = availableCovers;
        this.target = target;
        this.enemyAI = enemyAI;
    }

    public override NodeState Evaluate()
    {
        Transform bestPosition = FindBestCoverSpots();
        enemyAI.SetBestCoverSpot(bestPosition);
        return bestPosition != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    private Transform FindBestCoverSpots()
    {
        float minAngle = 90;
        Transform bestSpot = null;
        for(int i = 0; i < availableCovers.Length; i++)
        {
            Transform bestSpotInCover = FindBestSpotInCover(availableCovers[i], ref minAngle);
            if(bestSpotInCover != null)
            {
                bestSpot = bestSpotInCover;
            }
        }
        return bestSpot;
    }

    private Transform FindBestSpotInCover(Cover cover, ref float minAngle)
    {
        Transform[] availableSpots = cover.GetCoverSpots();
        Transform bestSpot = null;
        

        for(int i = 0; i < availableCovers.Length; i++)
        {
            Vector3 direction = target.position - availableSpots[i].position;
            if (checkIfCoverIsValid(availableSpots[i]))
            {
                float angle = Vector3.Angle(availableSpots[i].forward, direction);
                if(angle < minAngle)
                {
                    minAngle = angle;
                    bestSpot = availableSpots[i];
                }
            }
        }
        return bestSpot;
    }

    private bool checkIfCoverIsValid(Transform spot)
    {
        RaycastHit hit;
        Vector3 direction = target.position - spot.position;
        if(Physics.Raycast(spot.position, direction, out hit))
        {
            if(hit.collider.transform != target)
            {
                return true;
            }
        }
        return false;
    }
}
