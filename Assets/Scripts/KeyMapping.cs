using System;
using System.Linq;
using UnityEngine;
using Utils;

public class KeyMapping : MonoBehaviour
{
    [Serializable] public struct Key {
        public InputAction inputAction;
        public TMPro.TextMeshProUGUI text;
    }
    public Key[] keys;

    private void Start()
    {
        if (keys.Select(x => x.inputAction).Distinct().Count() != keys.Length)
            throw new Exception("Duplicate input actions in key mapping");

        if (Enum.GetValues(typeof(InputAction)).Cast<InputAction>().Except(keys.Select(x => x.inputAction)).Any())
            throw new Exception("Missing input actions in key mapping");

        foreach (var key in keys)
        {
            key.text.text = InputManager.Instance.GetKeyDisplayName(key.inputAction);
        }
    }

    private int _keyIndex = -1;
    public BackBehaviour backBehaviour;

    public void SetKey(InputActionComponent inputActionComponent)
    {
        foreach (var key in keys)
        {
            if (inputActionComponent.inputAction != key.inputAction) continue;
            _keyIndex = Array.IndexOf(keys, key);
            keys[_keyIndex].text.text = "";
            backBehaviour.enabled = false;
        }
    }

    private void Update()
    {
        if (_keyIndex < 0) return;
        var key = keys[_keyIndex];

        if(Input.GetKey(KeyCode.Escape)) // Not using InputManager here because it's not a keybind
        {
            key.text.text = InputManager.Instance.GetKeyDisplayName(key.inputAction);
            _keyIndex = -1;
            backBehaviour.enabled = true;
        }

        if (!Input.anyKey) return;

        var pressedKeys = InputManager.KeyNameMapping.Where(x => Input.GetKey(x.Key)).ToArray();
        if (!pressedKeys.Any()) return;

        var pressedKey = pressedKeys.First();
        key.text.text = pressedKey.Value;
        _keyIndex = -1;
        backBehaviour.enabled = true;

        InputManager.Instance.SetMapping(key.inputAction, pressedKey.Key);
    }
}
