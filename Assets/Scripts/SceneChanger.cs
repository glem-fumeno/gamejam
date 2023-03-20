using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string LevelName;
    public Transform TransportPosition;
    // public Color color = Color.white;
    void OnTriggerEnter2D(Collider2D other){
        if(!other.gameObject.CompareTag("GameController") || Time.timeSinceLevelLoad < 0.1f) return;
        other.transform.position = other.transform.position + TransportPosition.localPosition;
        LoadScene(LevelName);
        // Camera.main.backgroundColor = color;
    }
    public static void LoadScene( string SceneNameToLoad)
    {
        PendingPreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += ActivatorAndUnloader;
        SceneManager.LoadScene( SceneNameToLoad, LoadSceneMode.Additive);
    }
 
    static string PendingPreviousScene;
    static void ActivatorAndUnloader( Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= ActivatorAndUnloader;
        SceneManager.SetActiveScene( scene);
        SceneManager.UnloadSceneAsync( PendingPreviousScene);
    }
}
