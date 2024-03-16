using UnityEngine;
using System.Collections.Generic;

public class ObjectSelector : MonoBehaviour
{
    public List<Selectable> selectedObjects = new List<Selectable>();

    // Update is called once per frame
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits any object
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has a selectable component
                Selectable selectable = hit.collider.GetComponent<Selectable>();

                if (selectable != null)
                {
                    // Toggle selection state
                    ToggleSelection(selectable);
                }
            }
        }
    }

    // Toggle the selection state of a selectable object
    void ToggleSelection(Selectable selectable)
    {
        if (selectedObjects.Contains(selectable))
        {
            // If already selected, deselect it
            selectedObjects.Remove(selectable);
            selectable.Deselect();
        }
        else
        {
            // If not selected, select it
            selectedObjects.Add(selectable);
            selectable.Select();
        }
    }
}
