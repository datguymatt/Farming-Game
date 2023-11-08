using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    public List<GraphNode> _nodes = new List<GraphNode>();

    public GraphNode _startNode;
    public GraphNode _targetNode;

    public LineRenderer _pathLine;

    Dictionary<GraphNode, float> distances = new Dictionary<GraphNode, float>();
    Dictionary<GraphNode, GraphNode> previousNodes = new Dictionary<GraphNode, GraphNode>();

    

    private void Awake()
    {
        // Adding all the nodes
        foreach (GraphNode node in FindObjectsOfType(typeof(GraphNode)))
        {
            _nodes.Add(node);
        }
    }

    private void Update()
    {
        if (_startNode == null || _targetNode == null) return;

        CalculateDijkstras();
    }

    public void CalculateDijkstras()
    {
        // Initialize the distances and previous nodes
        foreach (GraphNode node in _nodes)
        {
            distances[node] = float.MaxValue;
            previousNodes[node] = null;
        }

        // Distance from start node to startnode is 0
        distances[_startNode] = 0;

        // Create a priority queue for storing nodes to visit
        List<GraphNode> unvisitedNodes = new List<GraphNode>(_nodes);

        while (unvisitedNodes.Count > 0)
        {
            // Find the node with the smallest distance
            GraphNode currentNode = null;
            foreach (GraphNode node in unvisitedNodes)
            {
                if (currentNode == null || distances[node] < distances[currentNode])
                {
                    currentNode = node;
                }
            }

            unvisitedNodes.Remove(currentNode);

            //Update distances to neighboring nodes
            foreach (GraphEdge edge in currentNode._edges)
            {
                float distance = distances[currentNode] + edge._cost;

                // Choose the proper node
                GraphNode neighbourNode = currentNode == edge._fromNode ? edge._toNode : edge._fromNode;

                if (distance < distances[neighbourNode])
                {
                    distances[neighbourNode] = distance;
                    previousNodes[neighbourNode] = currentNode;
                }
            }
        }

        // Draw the shortestpath
        List<GraphNode> path = new List<GraphNode>();

        GraphNode pathNode = _targetNode;
        while (pathNode != null)
        {
            path.Insert(0, pathNode);
            pathNode = previousNodes[pathNode];
        }

        _pathLine.positionCount = path.Count;

        for(int i=0; i<path.Count; i++)
        {
            _pathLine.SetPosition(i, path[i].transform.position);
        }
    }

}
