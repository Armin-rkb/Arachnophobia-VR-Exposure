using UnityEngine;

public class GazePointer : MonoBehaviour
{
    private GazeInteractable gazeInteractable;
    private Collider LastFrameObjectHit;
    private Collider ObjectHit;
    private float gazeStartTime;

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
        Debug.DrawRay(transform.position, forward, Color.red);

        RaycastHit gazeInteractableHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out gazeInteractableHit))
        {
            ObjectHit = gazeInteractableHit.collider;

            // Check if we hit a new or same object.
            if (LastFrameObjectHit != ObjectHit)
            {
                gazeStartTime = Time.time;

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
                    if (gazeInteractable)
                    { 
                        gazeInteractable.GazeEnd();
                        gazeInteractable = null;
                    }
                    // Object isn't an interactable.
                    return;
                }
            }

            if (!gazeInteractable)
            {
                ObjectHit = null;
                LastFrameObjectHit = null;
                return;
            }

            // Same object has been hit, progress gaze time.
            if (gazeInteractable && !gazeInteractable.IsActivated)
            {
                float activateTime = (gazeStartTime + gazeInteractable.GazeTimeToActivate) - Time.time;
                float progress = 1 - (activateTime / gazeInteractable.GazeTimeToActivate);
                progress = Mathf.Clamp(progress, 0, 1);

                gazeInteractable.GazeStay(progress);

                // Progress is from 0-1. 0 being the start time and 1 the end time to activate.
                if (progress == 1)
                {
                    gazeInteractable.GazeActivated();
                }
            }

            // Reference what we hit for the next frame.
            LastFrameObjectHit = gazeInteractableHit.collider;
        }
    }
}
