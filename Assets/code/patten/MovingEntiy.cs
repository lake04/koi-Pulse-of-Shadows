using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class MovingEntiy : MonoBehaviour
{
    private Movement movement2D;
    private Vector3 originPosition; //���� ��ġ
    private Vector3 originDirection; //���� ����

    private void Awake()
    {
        movement2D = GetComponent<Movement>();
        originPosition = transform.position;
        originDirection = movement2D.MoveDirection;
    }

    public void Reset()
    {
        //�̵� ����� ��ġ �ʱ�ȭ
        movement2D.MoveTo(originDirection);
        transform.position = originPosition;
    }
}
