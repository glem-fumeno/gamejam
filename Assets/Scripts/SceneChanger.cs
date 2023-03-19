using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string LevelName;
    public Transform TransportPosition;
    void OnTriggerEnter2D(Collider2D other){
        if(!other.gameObject.CompareTag("GameController") || Time.timeSinceLevelLoad < 0.1f) return;
        other.transform.position = other.transform.position + TransportPosition.localPosition;
        SceneManager.LoadScene(LevelName);
    }
}
