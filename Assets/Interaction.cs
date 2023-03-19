using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public GameObject Key;
    private GameObject _key;
    public KeyMapping mapping;
    public UnityEvent onInteract;
    private void Update(){
        if(_key == null) return;
        if(!Input.GetKeyDown(mapping.getKeyCode("Interact"))) return;
        // Debug.Log("Interaction");
        onInteract.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("GameController")) return;
        Vector2 pos = transform.position;
        pos.y += 1;
        _key = Instantiate(Key, pos, Quaternion.identity);
        _key.GetComponentInChildren<Transform>().GetComponentInChildren<TMPro.TextMeshProUGUI>().text = mapping.keyNameMapping[mapping.getKeyCode("Interact")];
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("GameController")) return;
        Destroy(_key);
    }
}
