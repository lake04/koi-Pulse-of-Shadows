using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class MovingEntiy : MonoBehaviour
{
    private Movement movement2D;
    private Vector3 originPosition; //최초 위치
    private Vector3 originDirection; //최초 방향

    private void Awake()
    {
        movement2D = GetComponent<Movement>();
        originPosition = transform.position;
        originDirection = movement2D.MoveDirection;
    }

    public void Reset()
    {
        //이동 방향과 위치 초기화
        movement2D.MoveTo(originDirection);
        transform.position = originPosition;
    }
}
