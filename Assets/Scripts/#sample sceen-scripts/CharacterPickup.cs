using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPickup : MonoBehaviour
{
    public Transform holdPoint;  // The point where the character holds the object
    private GameObject heldObject;  // The object currently held by the character
    private bool isCollided;
    public Material highlightMaterial; // Reference to the highlight material
    private Material originalMaterial; // To store the original material

    void Update()
    {
        // Check for user input to pick up or drop an object
        if (Input.GetMouseButtonDown(0))
        {
            // Check if there is a collision with an obstacle and the character is not holding an object
            if (isCollided != true && heldObject == null)
            {
                // If not holding an object, try to pick up an object
                TryPickupObject();                
            }
        }

        // Check for user input to pick up or drop an object
        if (Input.GetMouseButtonDown(1))
        {
            if (heldObject != null)  // Check if the character is holding an object
            {
                // If holding an object, try to drop it
                DropObject();
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        // Check if there is a collision with an obstacle and the character is not holding an object
        if (collision.gameObject.CompareTag("Obstacle") && heldObject == null)
        {
            // If not holding an object, try to pick up an object
            TryPickupObject();            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collision involves an obstacle
        if (other.CompareTag("HoldPoint"))
        {
            isCollided = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the character is no longer colliding with an obstacle
        if (other.CompareTag("HoldPoint"))
        {
            isCollided = false;
        }
    }

    void TryPickupObject()
{
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    // Cast a ray to check for objects to pick up
    if (Physics.Raycast(ray, out hit))
    {
        // Check if the hit object is pickable
        PickableObject pickableObject = hit.collider.GetComponent<PickableObject>();

        if (pickableObject != null)
        {
            // Check if the holdPoint is colliding with the pickableObject
            if (IsObjectCollidedWithHoldPoint(pickableObject.gameObject))
            {
                // Pick up the object
                heldObject = pickableObject.gameObject;
                heldObject.transform.SetParent(holdPoint);
                heldObject.transform.localPosition = Vector3.zero;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Optional: Disable physics while holding
            }
        }
    }
}


   bool IsObjectCollidedWithHoldPoint(GameObject obj)
   {
    Collider holdPointCollider = holdPoint.GetComponent<Collider>();
    if (holdPointCollider != null)
    {
        // Check if the object is colliding with the holdPoint
        return holdPointCollider.bounds.Intersects(obj.GetComponent<Collider>().bounds);
    }
    return false;
   }


    void DropObject()
    {
        // Drop the held object
        heldObject.transform.SetParent(null);
        heldObject.GetComponent<Rigidbody>().isKinematic = false; // Optional: Enable physics after dropping
        heldObject = null;
    }
}
