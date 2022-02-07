using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Canvas mainMenu;

    public Canvas editorMenu;
    // Start is called before the first frame update

    void Start()
    {
        mainMenu.gameObject.SetActive(true);
        editorMenu.gameObject.SetActive(false);
    }

    public void OpenEditor()
    {
        mainMenu.gameObject.SetActive(false);
        editorMenu.gameObject.SetActive(true);
    }

    public void GoBack()
    {
        editorMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
}
