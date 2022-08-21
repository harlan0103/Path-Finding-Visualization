using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Test for max heap
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPathFinding()
    {
        GridController.instance.PathFinding();
    }
}
