using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgorithmController : MonoBehaviour
{
    public static AlgorithmController instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<int> A_star(GameObject startNode, GameObject endNode, int width, int height)
    {
        /*
         * 使用一个 max heap (min) 来保存每一个有效节点从起点到当前节点以及当前节点到终点期望值和的值
         * 对 max heap 第一个节点进行操作
         * 找到这个节点的四周节点
         * 对于每一个周围节点，判断其是否被判断过，以及从当前节点到这个周围节点的消耗是否比原本消耗要小
         * 更新这个周围节点的路径以及其消耗
         * 并保存到 max heap 中
         */

        maxHeap heap = new maxHeap();
        int[] cost_so_far = new int[width * height];
        int[] come_from = new int[width * height];

        int end_x = endNode.GetComponent<Node>().GetCoordX();
        int end_y = endNode.GetComponent<Node>().GetCoordY();

        Debug.Log("End-> x: " + end_x + ", y: " + end_y);

        // 处理 start node
        A_starNode start = new A_starNode(startNode, 0);
        heap.Push(start);

        int start_x = startNode.GetComponent<Node>().GetCoordX();
        int start_y = startNode.GetComponent<Node>().GetCoordY();

        Debug.Log("Start-> x: " + start_x + ", y: " + start_y);

        cost_so_far[start_x * width + start_y] = 0;
        come_from[start_x * width + start_y] = -1;
        come_from[end_x * width + end_y] = -1;

        // 对当前最小值节点的周围节点进行更新
        while (!heap.Empty())
        {
            A_starNode current = heap.Pop();
            int current_x = current.GetNode().GetComponent<Node>().GetCoordX();
            int current_y = current.GetNode().GetComponent<Node>().GetCoordY();

            if (current.GetNode() == endNode)
            {
                break;
            }

            if (current_x == end_x && current_y == end_y)
            {
                break;
            }

            // 对于当前的节点找到其邻居节点
            List<GameObject> neighbors = GridController.instance.GetNeighbors(current.GetNode());   // Get current node's neighbors
            foreach (GameObject obj in neighbors)
            {
                int obj_x = obj.GetComponent<Node>().GetCoordX();
                int obj_y = obj.GetComponent<Node>().GetCoordY();
                int new_cost = cost_so_far[current_x * width + current_y] + 1;

                // 判断邻居节点是否通过当前节点有了更好的路径
                if (cost_so_far[obj_x * width + obj_y] == 0 || new_cost < cost_so_far[obj_x * width + obj_y])
                {
                    // 邻居节点有了更优的路径
                    cost_so_far[obj_x * width + obj_y] = new_cost;
                    int heuristic = Mathf.Abs(end_x - obj_x) + Mathf.Abs(end_y - obj_y);
                    int priority = new_cost + heuristic;
                    heap.Push(new A_starNode(obj, priority));
                    come_from[obj_x * width + obj_y] = current_x * width + current_y;
                }
            }
        }

        List<int> path = new List<int>();
        // 判断是否有有效路径
        if (come_from[end_x * width + end_y] == -1)
        {
            return path;
        }
        else
        {
            int idx = end_x * width + end_y;
            while (idx != start_x * width + start_y && idx != 0)
            {
                path.Add(come_from[idx]);
                idx = come_from[idx];
            }

            return path;
        }
    }
}
