using UnityEngine;
using System.Collections;

[System.Serializable]

//abstract 예약어: 추상클래스를 만드는 예약어
//         추상클래스 <--'형태'를 제공하는 클래스
public abstract class Node 
{
    /* Delegate that returns the state of the node.*/
    public delegate NodeStates NodeReturn();

    /* The current state of the node */
    protected NodeStates m_nodeState;

    public NodeStates nodeState 
    {
        get { return m_nodeState; }
    }

    /* The constructor for the node */
    public Node() {}

    /* Implementing classes use this method to valuate the desired set of conditions */
    public abstract NodeStates Evaluate();// 추상함수: 함수가 정의는 되어 있지 않고 형태만 제공한다.
    //<-- 자식클래스는 이 함수를 반드시 정의를 구현해야 한다(강제한다)

}
