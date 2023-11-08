using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(LineRenderer))]
public class GraphEdge : MonoBehaviour
{
    public GraphNode _fromNode, _toNode;
    public float _cost;
    public GameObject _costTextObject;

    private GameObject _costText;
    private LineRenderer _line;

    private void Start()
    {
        _costText = Instantiate(_costTextObject);
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 2;
    }

    private void Update()
    {
        _cost = Vector3.Distance(_fromNode.transform.position, _toNode.transform.position);
        DrawLine();
        _costText.GetComponent<TMP_Text>().SetText("" + _cost);
    }

    void DrawLine()
    {
        _line.SetPosition(0, _fromNode.transform.position);
        _line.SetPosition(1, _toNode.transform.position);
        _costText.transform.position = (_fromNode.transform.position + _toNode.transform.position) / 2 + Vector3.up * 0.2f;
    }
}
