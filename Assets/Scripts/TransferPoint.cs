using System.Collections;
using UnityEngine;

public class TransferPoint : MonoBehaviour
{
    public string destinationTag = "Destination"; // Set the destination tag in the Unity Inspector
    public Transform[] destinationPoints; // Set this in the Unity Inspector with the possible destination points

    private bool isCollided;
    private GameObject selectedObject; // Track the selected object

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the transfer point is the player and is not a child
        if (other.CompareTag("Selectable") && !IsChildObject(other.gameObject) && selectedObject == null)
        {
            selectedObject = other.gameObject; // Select the object
            SnapToNearestDestination(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reset the selected object if it exits the trigger
        if (other.gameObject == selectedObject)
        {
            selectedObject = null;
        }
    }

    bool IsChildObject(GameObject obj)
    {
        // Check if the object has a parent
        return obj.transform.parent != null;
    }

    void SnapToNearestDestination(Transform objectToSnap)
    {
        if (destinationPoints.Length == 0)
        {
            Debug.LogWarning("No destination points assigned to the TransferPoint.");
            return;
        }

        // Find the nearest destination point
        Transform nearestDestination = FindNearestDestination(objectToSnap.position);

        if (nearestDestination != null)
        {
            // Snap the object to the nearest destination
            objectToSnap.position = nearestDestination.position;
            objectToSnap.rotation = nearestDestination.rotation;
        }
    }

    Transform FindNearestDestination(Vector3 currentPosition)
    {
        GameObject[] destinations = GameObject.FindGameObjectsWithTag(destinationTag);

        if (destinations.Length == 0)
        {
            return null;
        }

        Transform nearestDestination = destinations[0].transform;
        float shortestDistance = Vector3.Distance(currentPosition, nearestDestination.position);

        foreach (GameObject destination in destinations)
        {
            float distance = Vector3.Distance(currentPosition, destination.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestDestination = destination.transform;
            }
        }

        return nearestDestination;
    }
}
