using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sequence : Node 
{
    /** Chiildren nodes that belong to this sequence */
    private List<Node> m_nodes = new List<Node>();//N개의 하위 노드들

    /** Must provide an initial set of children nodes to work */
    public Sequence(List<Node> nodes) 
    {
        m_nodes = nodes;
    }

    /* If any child node returns a failure, the entire node fails. Whence all 
     * nodes return a success, the node reports a success. */
    public override NodeStates Evaluate() 
    {
        bool anyChildRunning = false;
        
        foreach(Node node in m_nodes)//N개의 하위노드들을 평가
        {
            switch (node.Evaluate()) 
            {
                case NodeStates.FAILURE:
                    m_nodeState = NodeStates.FAILURE;
                    return m_nodeState;                    
                    //<--하위 노드들 중 하나라도 false를 리턴라면 Sequence도 false를 리턴하며 그 즉시 종료한다.

                case NodeStates.SUCCESS:
                    continue;
                case NodeStates.RUNNING:
                    anyChildRunning = true;
                    continue;
                default:
                    m_nodeState = NodeStates.SUCCESS;
                    return m_nodeState;
            }
        }
        m_nodeState = anyChildRunning ? NodeStates.RUNNING : NodeStates.SUCCESS;
        return m_nodeState;
    }
}
