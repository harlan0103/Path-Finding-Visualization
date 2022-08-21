using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController instance;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] GameObject node;

    private GameObject[ , ] gridList;               // Use an array to store each node
    private GameObject startNode = null;
    private GameObject endNode = null;
    private bool[ ,] isBlock;

    private Ray ray;
    private RaycastHit hit;
    private bool isRun;           // Pathfinding is running or not

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gridList = new GameObject[height, width];
        isBlock = new bool[height, width];

        InitializeGrid();

        isRun = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.tag == "Node" && !isRun)
                {
                    int selected = UIController.instance.GetMenuSelected();

                    // Based on menu option set up nodes
                    if (selected == 0)
                    {
                        SetStartNode(obj);
                    }
                    else if (selected == 1)
                    {
                        SetEndNode(obj);
                    }
                    else if (selected == 2)
                    {
                        SetBlock(obj);
                    }
                }
            }
        }
    }

    public void InitializeGrid()
    {
        float initPosX = 0.0f;
        float initPosY = 0.0f;
        //float offset = 1.5f;
        float offset = node.gameObject.GetComponent<MeshFilter>().mesh.bounds.size.x;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                // Create clone and set up position
                GameObject newNode = Instantiate(node);
                newNode.transform.position = new Vector3(initPosX, 0.0f, initPosY);     // Set position
                gridList[i, j] = newNode;
                newNode.GetComponent<Node>().SetCoord(i, j);

                initPosX += offset;
            }
            initPosX = 0.0f;
            initPosY += offset;
        }

        // Initialize block array
        for (int m = 0; m < height; m++)
        {
            for (int n = 0; n < width; n++)
            {
                isBlock[m, n] = false;
            }
        }
    }

    // Based on dropdown menu and mouse click to set up a start node
    public void SetStartNode(GameObject selectedNode)
    {
        // If current start is the same as selected then set as default
        if (startNode != null)
        {
            GameObject curr = startNode;
            startNode = null;
            curr.GetComponent<Node>().SetStatus(3);

            if (curr == selectedNode)
            {
                return;
            }
        }

        // Set new start node
        startNode = selectedNode;
        selectedNode.GetComponent<Node>().SetStatus(0);
    }

    public void SetEndNode(GameObject selectedNode)
    {
        // If current start is the same as selected then set as default
        if (endNode != null)
        {
            GameObject curr = endNode;
            endNode = null;
            curr.GetComponent<Node>().SetStatus(3);

            if (curr == selectedNode)
            {
                return;
            }
        }

        // Set new start node
        endNode = selectedNode;
        selectedNode.GetComponent<Node>().SetStatus(1);
    }

    public void SetBlock(GameObject selectedNode)
    {
        int x = selectedNode.GetComponent<Node>().GetCoordX();
        int y = selectedNode.GetComponent<Node>().GetCoordY();
        if (isBlock[x, y])
        {
            // reverse it
            isBlock[x, y] = false;
            selectedNode.GetComponent<Node>().SetStatus(3);
        }
        else
        {
            isBlock[x, y] = true;
            selectedNode.GetComponent<Node>().SetStatus(2);
        }
    }

    // Return neighbors of selected node
    public List<GameObject> GetNeighbors(GameObject node)
    {
        List<GameObject> list = new List<GameObject>();
        int x = node.GetComponent<Node>().GetCoordX();
        int y = node.GetComponent<Node>().GetCoordY();

        if (x - 1 >= 0 && !isBlock[x - 1, y])
        {
            list.Add(gridList[x - 1, y]);
        }
        if (x + 1 < height && !isBlock[x + 1, y])
        {
            list.Add(gridList[x + 1, y]);
        }
        if (y - 1 >= 0 && !isBlock[x, y - 1])
        {
            list.Add(gridList[x, y - 1]);
        }
        if (y + 1 < width && !isBlock[x, y + 1])
        {
            list.Add(gridList[x, y + 1]);
        }

        return list;
    }

    public void PathFinding()
    {
        if (startNode == null || endNode == null)
        {
            Debug.Log("Not set start or end node");
            return;
        }

        List<int> path = AlgorithmController.instance.A_star(startNode, endNode, width, height);

        if (path.Count == 0)
        {
            Debug.Log("No valid path");
        }
        else
        {
            ShowPath(path);
        }
    }

    private void ShowPath(List<int> path)
    {
        foreach (int coord in path)
        {
            int coord_x = coord / width;
            int coord_y = coord % width;

            GameObject obj = gridList[coord_x, coord_y];
            obj.GetComponent<Node>().SetStatus(4);          // Change color to yellow
        }
    }
}
