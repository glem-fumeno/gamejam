using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBehaviour : MonoBehaviour
{
    public GameObject currentObject;
    public GameObject previousObject;
    public MenuManager menuManager;
    public void goBack(){
        menuManager.block_resume = false;
        previousObject.SetActive(true);
        currentObject.SetActive(false);
    }
    void Update()
    {
        if(menuManager != null)
            menuManager.block_resume = true;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            goBack();
        }
    }
}
