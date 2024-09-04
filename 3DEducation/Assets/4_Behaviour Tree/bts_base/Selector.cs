using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector : Node 
{
    /** The child nodes for this selector */
    protected List<Node> m_nodes = new List<Node>();//N개의 하위 노드


    /** The constructor requires a lsit of child nodes to be 
     * passed in*/
    public Selector(List<Node> nodes) 
    {
        m_nodes = nodes;
    }

    /* If any of the children reports a success, the selector will
     * immediately report a success upwards. If all children fail,
     * it will report a failure instead.*/
    public override NodeStates Evaluate() 
    {
        foreach (Node node in m_nodes)//N개의 하위 노드 평가
        {
            switch (node.Evaluate()) 
            {
                case NodeStates.FAILURE:
                    continue;
                case NodeStates.SUCCESS:
                    m_nodeState = NodeStates.SUCCESS;
                    return m_nodeState;
                //<-- 하위 노드들 중 하나라도 true를 리턴하면 Selector도 true를 리턴하며 그 즉시 종료한다.
                case NodeStates.RUNNING:
                    m_nodeState = NodeStates.RUNNING;
                    return m_nodeState;
                default:
                    continue;
            }
        }
        m_nodeState = NodeStates.FAILURE;
        return m_nodeState;
    }
}
