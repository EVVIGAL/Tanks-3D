using System.Collections;
using TMPro;
using UnityEngine;

public class DamageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private float _liveTime;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_liveTime);
        Destroy(gameObject);
    }

    public void Show(uint damage)
    {
        _valueText.SetText(damage.ToString());
    }
}