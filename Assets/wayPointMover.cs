using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayPointMover : MonoBehaviour
{
    [SerializeField] private waypoints waypoints;

    [SerializeField] private float moveSpeed = 5f;

    
    [Range(0f,15f)]
    [SerializeField] private float rotateSpeed = 4f;

    [SerializeField] private float distanceTresshold = 0.1f;
    private Transform currentWaypoint;

    private Quaternion rotationGoal;

    private Vector3 directionToWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceTresshold)
        {
            currentWaypoint = waypoints.GetNextWayPoint(currentWaypoint);
            //transform.LookAt(currentWaypoint);
        }
        RotateTowardWaypoint();
    }

    private void RotateTowardWaypoint()
    {
        directionToWaypoint =  (currentWaypoint.position - transform.position).normalized;
        rotationGoal = Quaternion.LookRotation(directionToWaypoint);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, rotateSpeed * Time.deltaTime);
    }
}
