using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class RepairKit : MonoBehaviour
{
    [field: SerializeField] public uint Health;
}