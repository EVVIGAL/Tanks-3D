using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject _missions;
    [SerializeField] private TankChoser _tankRefresher;
    [SerializeField] private GameObject[] _objectsToUnhide;

    private const string _animationToTank = "ToTank";
    private const string _animationToTable = "ToTable";

    private Animator _animator;
    private Coroutine _coroutine;
    private string _animation;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Activate(string animation)
    {
        _animation = animation;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(Move(_animation));
        }
        else
        {
            _coroutine = StartCoroutine(Move(_animation));
        }
    }

    public IEnumerator Move(string animation)
    {
        _animator.Play(animation);
        AnimationClip anim = _animator.runtimeAnimatorController.animationClips[1];
        yield return new WaitForSeconds(anim.length);

        if (_animation == _animationToTank)
            _missions.SetActive(true);

        if (_animation == _animationToTable)
        {
            Unhide();
            _tankRefresher.Refresh();
        }

        yield break;
    }

    private void Unhide()
    {
        foreach (GameObject obj in _objectsToUnhide)
            obj.SetActive(true);
    }
}