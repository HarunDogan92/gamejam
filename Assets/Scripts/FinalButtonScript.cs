using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalButtonScript : MonoBehaviour
{

    public SkillScript skillScriptLife;
    public SkillScript skillScriptAir;
    public SkillScript skillScriptSight;

    public TextMeshProUGUI unlockText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CountTrue() == 3){
            unlockText.text = "Final ability unlockable";
        }
        if(CountTrue() == 2){
            unlockText.text = "Max out one more skills to unlock final ability";
        }
    }

    int CountTrue(){
        return skillScriptLife.isFinalButtonActive + skillScriptAir.isFinalButtonActive + skillScriptSight.isFinalButtonActive;
    }
}
