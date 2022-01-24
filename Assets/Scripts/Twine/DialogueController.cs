using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueObject;

public class DialogueController : MonoBehaviour {
    
    private Dialogue curDialogue;
    private Node curNode;

    public delegate void NodeEnteredHandler( Node node );
    public event NodeEnteredHandler onEnteredNode;

    public Node GetCurrentNode() {
        return curNode;
    }

    public void InitializeDialogue(string twineText) {
        curDialogue = new Dialogue( twineText );
        curNode = curDialogue.GetStartNode();
        onEnteredNode( curNode );
    }

    public List<Response> GetCurrentResponses() {
        return curNode.responses;
    }

    public void ChooseResponse( int responseIndex ) {
        if (responseIndex > curNode.responses.Count - 1)
        {
            Debug.Log("Index of response is not correct");
            return;
        }
        string nextNodeID = curNode.responses[responseIndex].destinationNode;
        Node nextNode = curDialogue.GetNode(nextNodeID);
        Debug.Log( "Chose: " + nextNode.title );
        curNode = nextNode;
        onEnteredNode( nextNode );
    }
}