using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public GameObject[] GameObjects;
    void Awake(){
        foreach(GameObject obj in GameObjects){
            DontDestroyOnLoad(obj);
        }
    }
}
