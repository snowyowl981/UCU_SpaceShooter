using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtrl : MonoBehaviour
{
    // 열거형 변수 정의
    public enum State
    {
        IDLE, TRACE, ATTACK, DIE
    }
    // 몬스터의 상태
    public State state = State.IDLE;
    
    // 추적 사정거리
    [Range(10.0f, 50.0f)]
    public float traceDist = 10.0f;

    // 공격 사정거리
    public float attackDist = 2.0f;

    [System.NonSerialized] //C#
    public Transform monsterTr;
    [HideInInspector]  //UnityEngine
    public Transform playerTr;

    public bool isDie = false;

    void Start()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();

        StartCoroutine(CheckState());
    }

    IEnumerator CheckState()
    {
        while (isDie == false)
        {
            // 두 3차원 좌표간의 거리를 측정
            float distance = Vector3.Distance(monsterTr.position, playerTr.position);

            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }
            else if (distance <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }
}