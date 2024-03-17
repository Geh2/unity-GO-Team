//This script handles picking up and dropping boxes, 
//as well as finding the nearest empty slot.
using UnityEngine;

public class RotaInteractionController : MonoBehaviour
{
    private GameObject currentTarget;
    private GameObject currentSlot;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            PickUpBox(other.gameObject);
        }
        else if (other.CompareTag("Destination"))
        {
            DropBox(other.gameObject);
        }
    }

    void PickUpBox(GameObject box)
    {
        // Implement picking up logic
        currentTarget = box;
        // Move towards the box and pick it up
    }

    void DropBox(GameObject slot)
    {
        // Implement dropping logic
        currentSlot = slot;
        // Move towards the slot and drop the box
    }

    GameObject FindNearestEmptySlot()
    {
        GameObject[] destinations = GameObject.FindGameObjectsWithTag("Destination");
        GameObject nearestDestination = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject destination in destinations)
        {
            if (!destination.GetComponentInChildren<Transform>().Find("Target")) // Check if there's no child object named "Target"
            {
                float distance = Vector3.Distance(transform.position, destination.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestDestination = destination;
                }
            }
        }

        return nearestDestination;
    }
}
