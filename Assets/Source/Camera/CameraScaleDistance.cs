using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraScaleDistance : MonoBehaviour
{
    [SerializeField] private AnimationCurve _followOffsetX;

    private CinemachineVirtualCamera _camera;
    private CinemachineTransposer _transposer;
    private float _aspectRatio;

    private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        CinemachineComponentBase componentBase = _camera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        _transposer = componentBase as CinemachineTransposer;
    }

    private void Update()
    {
        float newAspectRatio = (float)Screen.width / Screen.height;

        if (Mathf.Approximately(newAspectRatio, _aspectRatio) == false)
        {
            _aspectRatio = newAspectRatio;
            float distance = _followOffsetX.Evaluate(_aspectRatio);
            _transposer.m_FollowOffset.x = distance;
        }
    }
}