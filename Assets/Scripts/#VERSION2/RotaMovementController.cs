using UnityEngine;
using UnityEngine.AI;

public class RotaMovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject closestTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (closestTarget == null || IsTargetReached())
        {
            closestTarget = FindClosestTarget();
            if (closestTarget != null)
            {
                agent.SetDestination(closestTarget.transform.position);
            }
        }
    }

    bool IsTargetReached()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    GameObject FindClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = target;
            }
        }

        return closest;
    }
}
