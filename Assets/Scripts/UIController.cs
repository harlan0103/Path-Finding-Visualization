using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] private Dropdown menu;

    private int selected;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        selected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDropDownMenuSelected()
    {
        Debug.Log(menu.value);
        switch (menu.value)
        {
            case 0:     // Start
                selected = 0;
                break;
            case 1:     // End
                selected = 1;
                break;
            case 2:     // Block
                selected = 2;
                break;
        }
    }

    public int GetMenuSelected()
    {
        return selected;
    }

    // Start path finding when click "start" btn on screen
    public void StartPathFinding()
    { 
        
    }
}
