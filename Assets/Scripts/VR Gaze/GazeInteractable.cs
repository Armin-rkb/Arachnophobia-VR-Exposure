using UnityEngine;
using UnityEngine.Events;

public class GazeInteractable : MonoBehaviour
{
    [field: SerializeField]
    public float GazeTimeToActivate
    {
        get;
        private set;
    }

    public bool IsActivated
    {
        get;
        private set;
    }

    [SerializeField]
    private UnityEvent OnGazeStart;
    [SerializeField]
    private UnityEvent<float> OnGazeStay;
    [SerializeField]
    private UnityEvent OnGazeEnd;
    [SerializeField]
    private UnityEvent OnGazeActivated;

    public void GazeStart()
    {
        OnGazeStart?.Invoke();
    }
    
    public void GazeStay(float progress)
    {
        OnGazeStay?.Invoke(progress);
    }

    public void GazeEnd() 
    {
        IsActivated = false;
        OnGazeEnd?.Invoke();
    }

    public void GazeActivated()
    {
        IsActivated = true;
        OnGazeActivated?.Invoke();
    }

    public void GazeInteractableReset()
    {
        IsActivated = false;
    }
}