using UnityEngine;
using TMPro;

public class CrateManager : MonoBehaviour
{
    public int crateCount;
    [SerializeField] TextMeshProUGUI crateText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crateCount = 0; // Initialize crate count (optional)
    }

    // Update is called once per frame
    void Update()
    {
        crateText.text = crateCount.ToString();  // Update crate count display
    }

    // This method will be called to sell crates and give cash
    public void SellCrates()
    {
        int cashToAdd = crateCount * 1000;  // Each crate is worth 1000
        CashManager.Instance.AddCash(cashToAdd);  // Add cash to the global CashManager
        crateCount = 0;  // Reset crate count after selling
    }
}
