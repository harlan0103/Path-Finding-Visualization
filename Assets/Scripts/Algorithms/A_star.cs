using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains A_star algorithms helper classes
public class A_starNode
{
    private GameObject node;
    private int distance;

    public A_starNode(GameObject _obj, int _dis)
    {
        node = _obj;
        distance = _dis;
    }

    public int GetVal()
    {
        return distance;
    }

    public GameObject GetNode()
    {
        return node;
    }

    public void SetVal(int _dis)
    {
        distance = _dis;
    }

    // override < comparision
    public static bool operator < (A_starNode lhs, A_starNode rhs)
    {
        if (lhs.distance < rhs.distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator > (A_starNode lhs, A_starNode rhs)
    {
        if (lhs.distance >= rhs.distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class maxHeap
{
    private List<A_starNode> list;
    private int size;

    public maxHeap()
    {
        list = new List<A_starNode>();
        size = 0;
        list.Add(new A_starNode(null, -1));     // 设置占位元素使得首元素的下标为 1
    }

    // Add new node
    public void Push(A_starNode node)
    {
        list.Add(node);
        size++;

        // 对于父节点 i 来说，左右子节点的位置为 2 * i 以及 2 * i + 1
        // 对于一个子节点 i 来说，其父节点为 i / 2
        int count = size;
        while (count > 0 && list[count] < list[count / 2])
        {
            A_starNode temp = list[count / 2];
            list[count / 2] = list[count];
            list[count] = temp;

            count = count / 2;
        }
    }

    // Pop up first node
    public A_starNode Pop()
    {
        if (!Empty())
        {
            // 将首元素和末尾元素调换并删除首元素
            A_starNode node = list[1];
            A_starNode temp = list[size];
            list[1] = temp;
            list[size] = node;

            list.Remove(node);
            size--; 

            // 将当前的首元素即末尾元素从上到下进行交换直到换到合适的位置
            int idx = 1;
            while (idx <= size)
            {
                int l = idx * 2 <= size ? idx * 2 : idx;
                int r = idx * 2 + 1 <= size ? idx * 2 + 1 : idx;

                int min = list[l] < list[r] ? l : r;

                if (list[min] < list[idx])
                {
                    A_starNode temp1 = list[idx];
                    list[idx] = list[min];
                    list[min] = temp1;
                    idx = min;
                }
                else
                {
                    break;
                }
            }

            return node;
        }

        return null;
    }

    // Return size
    public int Size()
    {
        return size;
    }

    public bool Empty()
    {
        return size == 0;
    }

    // Show maxheap structure based on node value
    public void Show()
    {
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i].GetVal());
        }
    }
    
}
