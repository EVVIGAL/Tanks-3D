public class ToolsAmount : SkillAmount
{
    public override void Save()
    {
        _saveData.Data.ToolsAmount = _amount;
        _saveData.Save();
        _text.text = _amount.ToString();
    }

    public override void Set()
    {
        _amount = _saveData.Data.ToolsAmount;
        _text.text = _amount.ToString();
    }
}
