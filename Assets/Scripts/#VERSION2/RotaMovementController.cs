using UnityEngine;
using UnityEngine.AI;

public class RotaMovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject closestTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true; // Ensure that the NavMeshAgent is enabled
        FindAndSetClosestTarget(); // Find and set the closest target immediately on start
    }

    void Update()
    {
        if (closestTarget != null && IsTargetReached())
        {
            // If target reached, find and set the next closest target
            FindAndSetClosestTarget();
        }

        if (closestTarget != null)
        {
            // Move towards the closest target
            agent.SetDestination(closestTarget.transform.position);
        }
    }

    bool IsTargetReached()
    {
        // Check if the Rota has reached the target by comparing the remaining distance to the stopping distance
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    void FindAndSetClosestTarget()
    {
        // Find all GameObjects with the "Target" tag
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        
        // Initialize variables to store the closest target and its distance
        closestTarget = null;
        float closestDistance = Mathf.Infinity;

        // Iterate through each target to find the closest one
        foreach (GameObject target in targets)
        {
            // Calculate the distance between the Rota and the target
            float distance = Vector3.Distance(transform.position, target.transform.position);

            // Check if this target is closer than the current closest one
            if (distance < closestDistance)
            {
                // Update the closest target and its distance
                closestDistance = distance;
                closestTarget = target;
            }
        }
    }
}
