using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Name;
    public TMPro.TextMeshProUGUI Description;
    public float time_on_screen = 5f;

    public void setName(string name){
        Name.text = name;
    }
    public void setDescription(string description){
        Description.text = description;
    }
    public void showNotification(){
        gameObject.SetActive(true);
        StartCoroutine(HideAfterDelay());
    }
    private void Start(){
        StartCoroutine(HideAfterDelay());
    }
    IEnumerator HideAfterDelay(){
        yield return new WaitForSeconds(time_on_screen);
        gameObject.SetActive(false);
    }
}
