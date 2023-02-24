public class ArtilleryAmount : SkillAmount
{
    public override void Save()
    {
        _saveData.Data.ArtilleryAmount = _amount;
        _saveData.Save();
    }

    public override void Set()
    {
        _amount = _saveData.Data.ArtilleryAmount;
        _text.text = _amount.ToString();
    }
}