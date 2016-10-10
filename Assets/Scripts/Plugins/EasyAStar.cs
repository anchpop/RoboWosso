using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

public class AStarPath{
    Func<Vector3, List<Vector3>> getValidPos;
    Func<Vector3, Vector3, float> getCost = ((Vector3 d, Vector3 u) => 1);

    public AStarPath(Func<Vector3, List<Vector3>> getValidPositions, Func<Vector3, Vector3, float> getCostValue)
    {
        getValidPos = getValidPositions;
        getCost = getCostValue;
    }
    public AStarPath(Func<Vector3, List<Vector3>> getValidPositions)
    {
        getValidPos = getValidPositions;
    }

    public List<Vector3> findPath(Vector3 start, Vector3 goal, int maxiterations=10000)
    {
        SimplePriorityQueue<Vector3> frontier = new SimplePriorityQueue<Vector3>();
        frontier.Enqueue(start, 0);
        Dictionary<Vector3, Vector3> came_from = new Dictionary<Vector3, Vector3>();
        Dictionary<Vector3, float> cost_so_far = new Dictionary<Vector3, float>();
        //came_from[start] = null;
        cost_so_far[start] = 0;
        List<Vector3> path = new List<Vector3>();
        int iterations = 0;
        while (frontier.Count != 0)
        {
            if (iterations < maxiterations)
            {
                iterations++;
                var current = frontier.Dequeue();
                if (current == goal) break;
                foreach (Vector3 next in getValidPos(current))
                {
                    var new_cost = cost_so_far[current] + getCost(current, next);
                    if (!cost_so_far.ContainsKey(next) || new_cost < cost_so_far[next])
                    {
                        cost_so_far[next] = new_cost;
                        var priority = new_cost + (goal - next).magnitude;
                        frontier.Enqueue(next, priority);
                        came_from[next] = current;
                    }
                }
            }
            else
                return null;
        }


        var earlier = goal;
        while (earlier != start)
        {
            path.Insert(0, earlier);
            earlier = came_from[earlier];
        }
        return path;

    }
 
}