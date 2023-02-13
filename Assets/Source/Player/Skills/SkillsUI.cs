using UnityEngine;

public class SkillsUI : MonoBehaviour
{
    [SerializeField] private Skill _repairKitSkill;
    [SerializeField] private Skill _artBlowSkill;

    public void OnRepairKitButtonClick()
    {
        _repairKitSkill.Use();
    }

    public void OnArtBlowButtonClick()
    {
        _artBlowSkill.Use();
    }
}