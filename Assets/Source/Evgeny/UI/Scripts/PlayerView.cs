using UnityEngine;
using TMPro;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _score;

    public void SetView(string rank, string name, string score)
    {
        _name.text = $"{rank}.  {name}";
        _score.text = score;
    }

    public void Clear()
    {
        _score.text = string.Empty;
        _name.text = string.Empty;
    }
}