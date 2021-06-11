using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveWall : MonoBehaviour
{
    public Transform pointStart;
    public Transform pointEnd;

    void Update()
    {
        transform.position = Vector3.Lerp(pointStart.position, pointEnd.position, Mathf.PingPong(Time.time / 3, 1));
    }
}
