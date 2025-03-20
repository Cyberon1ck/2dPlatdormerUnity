    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currectWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    void Update()
    {
        if (Vector2.Distance(waypoints[currectWaypointIndex].transform.position, transform.position) < .1f)
        {
            currectWaypointIndex++;
            if (currectWaypointIndex >= waypoints.Length)
            {
                currectWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currectWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
