using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public GameObject Key;
    private GameObject _key;
    public UnityEvent onInteract;
    private CharacterLightController player;
    private EffectLight lightbulb; // Nie pytaj xD
    private Notification notification;
    void Start(){
        player = Resources.FindObjectsOfTypeAll<CharacterLightController>()[0];
        lightbulb = Resources.FindObjectsOfTypeAll<EffectLight>()[0];
        notification = Resources.FindObjectsOfTypeAll<Notification>()[0];
    }
    public void setNumberOfColors(int colors)
    {
        lightbulb.SetMaxLights(colors);
    }
    public void AddColor(int color_index)
    {
        player.AddColor(color_index);
    }
    public void ShowNotification()
    {
        notification.showNotification();
    }
    public void setNotificationName(string notificationName)
    {
        notification.setName(notificationName);
    }
    public void setNotificationDescription(string NotificationDescription)
    {
        notification.setDescription(NotificationDescription);
    }
    public void loadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    private void Update(){
        if(_key == null) return;
        if (!InputManager.Instance.GetKeyDown(InputAction.Interact)) return;
        // Debug.Log("Interaction");
        onInteract.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("GameController")) return;
        Vector2 pos = transform.position;
        pos.y += 1;
        _key = Instantiate(Key, pos, Quaternion.identity);
        _key.GetComponentInChildren<Transform>().GetComponentInChildren<TMPro.TextMeshProUGUI>().text =
            InputManager.Instance.GetKeyDisplayName(InputAction.Interact);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("GameController")) return;
        Destroy(_key);
    }
}
