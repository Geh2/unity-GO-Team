using UnityEngine;
using UnityEngine.AI;

public class RotaController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject currentTarget;
    private GameObject currentSlot;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindAndSetClosestTarget();
    }

    void Update()
    {
        // Check if Rota has reached the target
        if (currentTarget != null && IsTargetReached())
        {
            // If target reached, pick up the box
            PickUpBox(currentTarget);
        }

        // Check if Rota is carrying a box and has reached the slot
        if (currentSlot != null && IsTargetReached())
        {
            // If slot reached, drop the box
            DropBox(currentSlot);
        }

        // If Rota is not carrying a box, find and move towards the closest target
        if (currentTarget != null && currentSlot == null)
        {
            agent.SetDestination(currentTarget.transform.position);
        }
    }

    bool IsTargetReached()
    {
        // Check if the Rota has reached the target by comparing the remaining distance to the stopping distance
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    void PickUpBox(GameObject box)
    {
        // Implement picking up logic
        currentTarget = null; // Reset the current target
        // Move towards the box and pick it up
        Debug.Log("Picking up box: " + box.name);
        // Perform additional actions as needed
    }

    void DropBox(GameObject slot)
    {
        // Implement dropping logic
        currentSlot = null; // Reset the current slot
        // Move towards the slot and drop the box
        Debug.Log("Dropping box at slot: " + slot.name);
        // Perform additional actions as needed
    }

    void FindAndSetClosestTarget()
    {
        // Find all GameObjects with the "Target" tag
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        
        // Initialize variables to store the closest target and its distance
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
                currentTarget = target;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if Rota collides with a box
        if (other.CompareTag("Target"))
        {
            // Assign the collided box as the current target
            currentTarget = other.gameObject;
        }

        // Check if Rota collides with a slot
        if (other.CompareTag("Destination"))
        {
            // Assign the collided slot as the current slot
            currentSlot = other.gameObject;
        }
    }
}

