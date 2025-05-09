using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrateManager : MonoBehaviour
{

    public int crateCount;
    [SerializeField] TextMeshProUGUI crateText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        crateText.text = crateCount.ToString();
    }

}
