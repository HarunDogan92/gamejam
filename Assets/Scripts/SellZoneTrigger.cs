using UnityEngine;

public class SellZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Check if the object entering is the player
        {
            // Use Object.FindObjectOfType to find the CrateManager
            CrateManager crateManager = Object.FindObjectOfType<CrateManager>();
            if (crateManager != null)
            {
                crateManager.SellCrates();  // Sell crates when player enters the zone
            }
        }
    }
}
