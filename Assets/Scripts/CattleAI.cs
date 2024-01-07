using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CattleAI : MonoBehaviour
{
    public Transform player; // Player's transform
    public NavMeshAgent agent;
    private Animator anim;

    [Header("Health Settings")]
    public AIHealth aiHealth; // Reference to AIHealth script
    public bool dead;

    [Header("Attack Settings")]
    public float damage = 5f;
    public float attackCooldownTime = 10.0f; // Time between attacks

    [Header("Movement")]
    public float wanderWaitTime = 10f;
    public float walkSpeed = 2f;
    public float runSpeed = 3.5f;
    public float wanderingRange = 20f; // The range for wandering
    public float minWalkDistance = 10f; // Minimum distance to walk before setting a new destination
    public float fleeingDistance = 10f; // Distance to flee from the player

    [Header("Drop Settings")]
    public GameObject gatherableItemPrefab;

    private bool isAttacking;
    private bool isWandering = true;
    private Vector3 currentDestination;

    private SphereCollider detectionCollider; // SphereCollider for detecting the player
    private float maxChaseDistance; // Maximum chase distance based on collider radius

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        detectionCollider = GetComponent<SphereCollider>(); // Get the SphereCollider component
        aiHealth = GetComponent<AIHealth>(); // Get the AIHealth component

        // Use the collider's radius to determine the maximum chase distance
        maxChaseDistance = detectionCollider.radius;

        // Set the initial destination within the wandering range
        SetRandomDestinationInWanderingRange();
    }

    private void Update()
    {
        // Update the dead boolean based on AIHealth script
        dead = aiHealth.IsDead();

        if (dead)
        {
            Die();
            return; // Exit the method if the animal is dead
        }

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

        // Check if the player is within the chase distance
        if (distanceToPlayer <= maxChaseDistance)
        {
            isWandering = false;
            agent.speed = runSpeed;
            anim.SetBool("Walk", false);
            anim.SetBool("Run", true);
            SetChaseDestination();
        }
        else if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // The AI has reached its destination, so set a new random destination within the wandering range
            StartCoroutine(WaitAndSetNewDestination());
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
            SetRandomDestinationInWanderingRange();
            return; // Exit the method, no further actions needed
        }

        if (distanceToPlayer <= agent.stoppingDistance)
        {
            // The player is within a certain distance, make the cattle run away
            FleeFromPlayer();
        }
        else if (distanceToPlayer > maxChaseDistance)
        {
            // Player is outside the chase distance, go back to wandering
            isWandering = true;
            agent.speed = walkSpeed;
            anim.SetBool("Run", false);
            anim.SetBool("Walk", true);
            SetRandomDestinationInWanderingRange();
        }
        else
        {
            // Continue chasing the player
            agent.SetDestination(player.position);
        }
    }

    private void FleeFromPlayer()
    {
        // Calculate the direction away from the player
        Vector3 fleeDirection = transform.position - player.position;

        // Calculate the target position by adding the flee direction to the current position
        Vector3 fleePosition = transform.position + fleeDirection.normalized * fleeingDistance;

        // Set the destination for fleeing
        agent.SetDestination(fleePosition);

        // Play the run animation
        isWandering = false;
        agent.speed = runSpeed;
        anim.SetBool("Walk", false);
        anim.SetBool("Run", true);
    }

    private void SetChaseDestination()
    {
        // Set the destination to the player's position for chasing
        agent.SetDestination(player.position);
    }

    private void UpdateAnimations()
    {
        anim.SetBool("Walk", isWandering);
        anim.SetBool("Run", !isWandering);
        anim.SetBool("Eat", false); // Reset eat animation
    }

    private void Die()
    {
        DropGatherableItem();
        // You can add more death-related logic here
        Destroy(gameObject); // Destroy the animal GameObject
    }

    private void DropGatherableItem()
    {
        if (gatherableItemPrefab != null && aiHealth != null)
        {
            // Get the position where the animal has died
            Vector3 deathPosition = transform.position;

            // Add a small offset in the y-axis (3 cm up)
            deathPosition.y += 1.00f;

            // Instantiate the gatherable item at the modified position
            GameObject droppedItem = Instantiate(gatherableItemPrefab, deathPosition, Quaternion.identity);

            // Assuming your GatherableItem script is attached to the prefab and has a DropToGround method
            GatherableItem gatherableItem = droppedItem.GetComponent<GatherableItem>();
            if (gatherableItem != null)
            {
                gatherableItem.DropToGround(deathPosition);
            }
            else
            {
                Debug.LogError("GatherableItem script not found on the prefab.");
            }
        }
        else
        {
            Debug.LogError("Gatherable item prefab or AIHealth reference not found.");
        }
    }

    private void SetRandomDestinationInWanderingRange()
    {
        // Use physics to find a random point within the specified wandering range
        NavMeshHit hit;
        Vector3 randomDirection = Random.onUnitSphere * wanderingRange;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y; // Keep the y-coordinate the same as the current position

        if (NavMesh.SamplePosition(randomDirection, out hit, wanderingRange, NavMesh.AllAreas))
        {
            currentDestination = hit.position;

            agent.autoTraverseOffMeshLink = true;
            agent.SetDestination(currentDestination);

            float distanceToDestination = Vector3.Distance(transform.position, currentDestination);
            if (distanceToDestination > minWalkDistance)
            {
                agent.speed = walkSpeed;
            }
            else
            {
                StartCoroutine(WaitAndSetNewDestination());
            }
        }
    }

    private IEnumerator WaitAndSetNewDestination()
    {
        if (!anim.GetBool("Eat") && Random.value < 0.1f) // 10% chance to start eating
        {
            anim.SetBool("Eat", true);
            yield return new WaitForSeconds(3f); // Assuming the eat animation takes 3 seconds
            anim.SetBool("Eat", false);
        }
        else
        {
            yield return new WaitForSeconds(wanderWaitTime);
        }

        SetRandomDestinationInWanderingRange();
    }
}
