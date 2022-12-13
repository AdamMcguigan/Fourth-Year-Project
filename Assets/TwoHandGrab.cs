using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHandGrab : MonoBehaviour
{
    // The hands that are grabbing the object
    public Transform hand1;
    public Transform hand2;

    // The position and rotation offsets for each hand
    public Vector3 hand1Offset;
    public Vector3 hand2Offset;
    public Quaternion hand1RotationOffset;
    public Quaternion hand2RotationOffset;

    void Update()
    {
        // Calculate the average position and rotation of the two hands
        Vector3 avgPos = (hand1.position + hand2.position) / 2;
        Quaternion avgRot = Quaternion.Lerp(hand1.rotation, hand2.rotation, 0.5f);

        // Apply the position and rotation offsets for each hand
        transform.position = avgPos + hand1Offset + hand2Offset;
        transform.rotation = avgRot * hand1RotationOffset * hand2RotationOffset;
    }
}
