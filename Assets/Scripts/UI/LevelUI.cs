using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField]
    [Range(0, 5)]
    private int inactiveOnLevel; 
    [SerializeField]
    private GameObject cross;
    [SerializeField]
    private BoxCollider levelGazeInteractable;

    private void Start()
    {
        CheckToCrossLevel(0);
    }

    public void CheckToCrossLevel(int level)
    {
        levelGazeInteractable.enabled = true;
        cross.SetActive(false);

        if (inactiveOnLevel == level)
        {
            levelGazeInteractable.enabled = false;
            cross.SetActive(true);
        }
    }
}
