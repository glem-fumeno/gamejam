using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public GameObject Key;
    private GameObject _key;
    public UnityEvent onInteract;
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
