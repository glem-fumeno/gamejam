using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool is_paused = false;
    public bool block_resume = false;
    public Animator player_animator;
    public void Play(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartRoom");
    }
    public void Pause(){
        Time.timeScale = 0f;
        is_paused = true;
        player_animator.SetBool("Paused", true);
        gameObject.SetActive(true);
    }
    public void Resume(){
        if(block_resume)
            return;
        Time.timeScale = 1f;
        is_paused = false;
        player_animator.SetBool("Paused", false);
        gameObject.SetActive(false);
    }
    public void Menu(){
        SceneManager.LoadScene(0);
    }
    public void Quit(){
        Debug.Log("Quitting");
        Application.Quit();
    }

}
