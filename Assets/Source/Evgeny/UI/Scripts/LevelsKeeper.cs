using UnityEngine;

public class LevelsKeeper : MonoBehaviour
{
    [SerializeField] private SaveData _data;

    public SaveData Data => _data;
}