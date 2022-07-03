using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class GazePointer : MonoBehaviour
{
    // Todo: Add layermask
    private RaycastHit previousGazeInteractableHit;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Debug Ray
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
        Debug.DrawRay(transform.position, forward, Color.red);
        //

        RaycastHit gazeInteractableHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out gazeInteractableHit)) // Layermask)
        {
            Debug.Log(gazeInteractableHit.collider.gameObject.name);
        }
            
    }
}
