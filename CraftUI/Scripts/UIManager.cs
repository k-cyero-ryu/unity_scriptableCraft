using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject craftUI; // Reference to the CraftUI GameObject

    public void ToggleCraftUI()
    {
        // Toggle the active state of the CraftUI
        craftUI.SetActive(!craftUI.activeSelf);
    }
}
