using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GraphNode : MonoBehaviour
{
    [HideInInspector] public List<GraphEdge> _edges = new List<GraphEdge>();

    public GameObject _graphNameObject;

    GameObject _graphName;

    private void Awake()
    {
        _graphName = Instantiate(_graphNameObject);
        _graphName.GetComponent<TMP_Text>().SetText(gameObject.name);

        // Adding all the edges connected to the node
        foreach(GraphEdge edge in FindObjectsOfType(typeof(GraphEdge)))
        {
            if(edge._fromNode == this || edge._toNode == this)
            {
                _edges.Add(edge);
                Debug.Log("Added " + gameObject.name);
            }
        }
    }

    private void Update()
    {
        _graphName.transform.position = transform.position + Vector3.up * 0.2f;
        
    }
}
