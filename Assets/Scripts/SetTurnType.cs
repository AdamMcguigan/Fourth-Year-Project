using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetTurnType : MonoBehaviour
{
    public ActionBasedSnapTurnProvider snapTurn;
    public ActionBasedContinuousTurnProvider continuousTurn;

    public ActionBasedContinuousMoveProvider move;
    public TeleportationProvider teleportation;

    public ActivateTeleportationRay teleportationRay;

    public void setTypeFromIndex(int index)
    {
        if(index == 0)
        {
            snapTurn.enabled = false;
            continuousTurn.enabled = true;
        }
        else if(index == 1)
        {
            snapTurn.enabled = true;
            continuousTurn.enabled = false; 
        }
    }

    public void setMovementFromIndex(int ind)
    {
        if (ind == 0)
        {
            move.enabled = false;
            teleportation.enabled = true;
            teleportationRay.enabled = true;
        }
        else if (ind == 1)
        {
            move.enabled = true;
            teleportation.enabled = false;
            teleportationRay.enabled = false;
        }
    }
}
