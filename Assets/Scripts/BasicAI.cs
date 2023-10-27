using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    public NavMeshAgent agent;
    private Animator anim;
    private SphereCollider wanderArea; // Reference to the SphereCollider

    [Header("Movement")]
    private float currentWanderTime;
    public float wanderWaitTime = 20f;
    [Space]
    public float walkSpeed = 1f;
    public float runSpeed = 4f;
    public bool walk;
    public bool run;

    private Vector3 currentDestination; // Store the current destination point

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        wanderArea = GetComponent<SphereCollider>(); // Get the SphereCollider component.

        currentWanderTime = wanderWaitTime;

        // Set the initial destination within the wander area
        SetRandomDestinationInSphere();
    }

    private void Update()
    {
        UpdateAnimations();

        // Check if the AI is close to the current destination
        if (Vector3.Distance(transform.position, currentDestination) < 10.0f)
        {
            SetRandomDestinationInSphere();
        }
    }

    public void UpdateAnimations()
    {
        anim.SetBool("Walk", walk);
        anim.SetBool("Run", run);

        // Set the animation speed for walking to 2
        if (walk)
        {
            anim.SetFloat("WalkSpeedMultiplier", 1.0f); // Adjust the parameter name as per your Animator setup.
        }
        else
        {
            anim.SetFloat("WalkSpeedMultiplier", 1.0f); // Set back to 1.0 for other states.
        }
    }

    // Set a random destination point within the sphere collider's bounds
    private void SetRandomDestinationInSphere()
    {
        // Generate a random point within the specified sphere collider's bounds
        Vector3 randomPointInSphere = RandomPointInSphere(wanderArea.transform.position, wanderArea.radius);

        // Ensure the point is within the sphere collider
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPointInSphere, out hit, wanderArea.radius, NavMesh.AllAreas))
        {
            currentDestination = hit.position;

            // Enable pathfinding obstacles avoidance
            agent.autoTraverseOffMeshLink = true;

            agent.SetDestination(currentDestination);

            // Adjust speed based on distance
            float distanceToDestination = Vector3.Distance(transform.position, currentDestination);
            if (distanceToDestination > 5.0f)
            {
                agent.speed = runSpeed;
                run = false;
                walk = true;
            }
            else
            {
                agent.speed = walkSpeed;
                run = false;
                walk = true;
            }
        }
    }

    // Helper function to get a random point within a sphere's bounds
    private Vector3 RandomPointInSphere(Vector3 center, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += center;
        return randomDirection;
    }
}
