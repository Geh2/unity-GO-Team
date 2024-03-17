using UnityEngine;

public class Selectable : MonoBehaviour
{
    public Material highlightMaterial; // Reference to the highlight material

    private Material originalMaterial; // To store the original material

    // Method to be called when the object is selected
    public void Select()
    {
        // Store the original material if it's not stored already
        if (originalMaterial == null)
        {
            originalMaterial = GetComponent<Renderer>().material;
        }

        // Apply the highlight material
        GetComponent<Renderer>().material = highlightMaterial;

        Debug.Log("Object Selected: " + gameObject.name);

        // You can perform additional actions when an object is selected
        // For example, change its color, show a menu, etc.
    }

    // Method to be called when deselecting the object (optional)
    public void Deselect()
    {
        // Restore the original material
        if (originalMaterial != null)
        {
            GetComponent<Renderer>().material = originalMaterial;
        }
    }
}
