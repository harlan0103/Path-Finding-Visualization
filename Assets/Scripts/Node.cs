using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Node : MonoBehaviour
{

    [SerializeField] private TextMeshPro coord;
    private int coord_x = 0;
    private int coord_y = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCoord(int x, int y)
    {
        coord.text = "(" + x + ", " + y + ")";
        coord_x = x;
        coord_y = y;
    }

    // Return the coordinate of this node
    public int GetCoordX()
    {
        return coord_x;
    }

    public int GetCoordY()
    {
        return coord_y;
    }

    // Set up node color
    public void SetStatus(int val)
    {
        switch (val)
        {
            case 0:
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 1:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 2:
                gameObject.GetComponent<Renderer>().material.color = Color.black;
                break;
            case 3:
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                break;
            case 4:
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                break;
        }
    }
}

