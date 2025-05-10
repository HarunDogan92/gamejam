using UnityEngine;
using TMPro;

public class CashManager : MonoBehaviour
{
    public static CashManager Instance;  // Singleton instance
    public TextMeshProUGUI cashText;  // Reference to the UI Text for cash display
    public int totalCash;

    private void Awake()
    {
        // Singleton pattern to ensure there's only one CashManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // If another instance exists, destroy it
        }
    }

    private void Start()
    {
        totalCash = 0;  // Initialize cash
        UpdateCashUI();
    }

    // Adds cash and updates the UI
    public void AddCash(int amount)
    {
        totalCash += amount;
        UpdateCashUI();
    }

    // Updates the UI to reflect the total cash
    private void UpdateCashUI()
    {
        cashText.text = " $" + totalCash.ToString();
    }

    // Gets the total cash
    public int GetTotalCash()
    {
        return totalCash;
    }
}
