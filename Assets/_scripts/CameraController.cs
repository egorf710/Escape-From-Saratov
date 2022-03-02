using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform _target;
    Vector3 _targetInVector3; // ����� ����� ������� � ����������� Z �� �������� � ������

    [Header("�������� � ����� ������ ����� ����������� ������(_target)")]
    [SerializeField, Range(1,10)] private float followSpeed = 3;

    private void Start()
    {
        _target = FindObjectOfType<PlayerController>().transform;
    }

    private void FixedUpdate()
    {
        _targetInVector3 = new Vector3(_target.position.x, _target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, _targetInVector3, followSpeed * Time.deltaTime);
    }
}
