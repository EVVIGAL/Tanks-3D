using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject _missions;

    private const string _animation = "ToTank";

    private Animator _animator;
    private Coroutine _coroutine;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Activate()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(Move());
        }
        else
        {
            _coroutine = StartCoroutine(Move());
        }
    }

    public IEnumerator Move()
    {
        _animator.Play(_animation);
        AnimationClip anim = _animator.runtimeAnimatorController.animationClips[1];
        yield return new WaitForSeconds(anim.length);
        _missions.SetActive(true);
        yield break;
    }
}
