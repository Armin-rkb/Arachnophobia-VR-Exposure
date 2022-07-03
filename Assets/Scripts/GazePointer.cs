using UnityEngine;

public class GazePointer : MonoBehaviour
{
    // Todo: Add layermask
    private GazeInteractable gazeInteractable;
    private Collider LastFrameObjectHit;
    private Collider ObjectHit;

    //
    private float currentGazeTime;

    void FixedUpdate()
    {
        // Debug Ray
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
        Debug.DrawRay(transform.position, forward, Color.red);
        //

        RaycastHit gazeInteractableHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out gazeInteractableHit)) // Layermask)
        {
            ObjectHit = gazeInteractableHit.collider;

            // Check if our interactable is valid.
            if (!ObjectHit)
            {
                return;
            }
            
            // Check if we hit a new or same object.
            if (LastFrameObjectHit != ObjectHit)
            {
                currentGazeTime = 0;

                GazeInteractable newGazeInteractable = gazeInteractableHit.collider.GetComponent<GazeInteractable>();
                if (newGazeInteractable != null)
                {
                    if (gazeInteractable != null)
                    {
                        // Stop previous interactable.
                        gazeInteractable.GazeEnd();
                    }

                    // New object has been hit.
                    gazeInteractable = newGazeInteractable;
                    gazeInteractable.GazeStart();
                }
                else
                {
                    // Object isn't an interactable.
                    return;
                }
            }

            // Same object has been hit, progress gaze time.
            if (!gazeInteractable.IsActivated)
            {
                currentGazeTime += Time.deltaTime;
                Debug.Log(gazeInteractableHit.collider.gameObject.name + ": " + currentGazeTime);

                if (currentGazeTime >= gazeInteractable.GazeTimeToActivate)
                {
                    gazeInteractable.GazeActivated();
                }
            }

            // Reference what we hit for the next frame.
            LastFrameObjectHit = gazeInteractableHit.collider;
        }
    }
}
