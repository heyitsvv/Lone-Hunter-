using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    public Transform player; // Player's transform
    public NavMeshAgent agent;
    private Animator anim;

    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Attack Settings")]
    public float damage = 5f;
    public float attackCooldownTime = 10.0f; // Time between attacks

    [Header("Movement")]
    public float wanderWaitTime = 10f;
    public float walkSpeed = 2f;
    public float runSpeed = 3.5f;
    public float stoppingDistance = 8.0f; // Adjust this stopping distance

    private bool isAttacking;
    private bool isWandering = true;
    private Vector3 currentDestination;

    private float currentWanderTime;

    private SphereCollider detectionCollider; // SphereCollider for detecting the player
    private float maxChaseDistance; // Maximum chase distance based on collider radius

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance; // Set the stopping distance
        anim = GetComponent<Animator>();
        detectionCollider = GetComponent<SphereCollider>(); // Get the SphereCollider component

        // Use the collider's radius to determine the maximum chase distance
        maxChaseDistance = detectionCollider.radius;

        currentWanderTime = wanderWaitTime;

        // Set the initial destination within the wander area
        SetRandomDestinationInSphere();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isWandering)
        {
            Wander(distanceToPlayer);
        }
        else
        {
            ChaseAndAttack(distanceToPlayer);
        }
    }

    private void Wander(float distanceToPlayer)
    {
        UpdateAnimations();

        // Check if the AI has reached its destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // The AI has reached its destination, so set a new random destination
            SetRandomDestinationInSphere();
        }
    }

    private void ChaseAndAttack(float distanceToPlayer)
    {
        if (player.GetComponent<PlayerHealth>().currentHealth <= 0)
        {
            // Player's health is zero or below, don't chase or attack
            isWandering = true;
            agent.speed = walkSpeed;
            anim.SetBool("Run", false);
            anim.SetBool("Walk", true);
            return; // Exit the method, no further actions needed
        }

        if (distanceToPlayer <= maxChaseDistance)
        {
            // Chase the player
            agent.SetDestination(player.position);
            agent.speed = runSpeed;
            anim.SetBool("Walk", false);
            anim.SetBool("Run", true);

            if (distanceToPlayer <= agent.stoppingDistance)
            {
                // The player is within attack range, so transition to the attack animation
                anim.SetTrigger("Attack");
            }
        }
        else
        {
            // Stop chasing and go back to wandering
            isWandering = true;
            agent.speed = walkSpeed;
            anim.SetBool("Run", false);
            anim.SetBool("Walk", true);
        }
    }

    public void UpdateAnimations()
    {
        anim.SetBool("Walk", isWandering);
        anim.SetBool("Run", !isWandering);
    }

    public void AttackPlayer()
    {
        // Damage the player
        player.GetComponent<PlayerHealth>().TakeDamage((int)damage);
        // You can add more attack-related logic here
    }

    public void StartAttack()
    {
        if (!isAttacking)
        {
            // Implement the attack cooldown timer
            isAttacking = true;
            StartCoroutine(ResetAttackCooldown());
        }
    }

    private IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownTime);
        isAttacking = false;
    }

    private void SetRandomDestinationInSphere()
    {
        // Generate a random point within the specified sphere collider's bounds
        Vector3 randomPointInSphere = RandomPointInSphere(detectionCollider.transform.position, detectionCollider.radius);

        // Ensure the point is within the sphere collider
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPointInSphere, out hit, detectionCollider.radius, NavMesh.AllAreas))
        {
            currentDestination = hit.position;

            agent.autoTraverseOffMeshLink = true;

            agent.SetDestination(currentDestination);

            float distanceToDestination = Vector3.Distance(transform.position, currentDestination);
            if (distanceToDestination > 5.0f)
            {
                agent.speed = runSpeed;
                isWandering = false;
            }
            else
            {
                agent.speed = walkSpeed;
                isWandering = true;
            }
        }
    }

    private Vector3 RandomPointInSphere(Vector3 center, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += center;
        return randomDirection;
    }
}
