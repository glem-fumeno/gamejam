using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class KeyMapping : MonoBehaviour
{
    [System.Serializable] public struct Key {
        public KeyCode key_code;
        public string key_name;
        public TMPro.TextMeshProUGUI key_text;
    }
    public Key[] keys;

    public Dictionary<KeyCode, string> keyNameMapping  = new Dictionary<KeyCode, string>(){
        {KeyCode.A, "A"},
        {KeyCode.B, "B"},
        {KeyCode.C, "C"},
        {KeyCode.D, "D"},
        {KeyCode.E, "E"},
        {KeyCode.F, "F"},
        {KeyCode.G, "G"},
        {KeyCode.H, "H"},
        {KeyCode.I, "I"},
        {KeyCode.J, "J"},
        {KeyCode.K, "K"},
        {KeyCode.L, "L"},
        {KeyCode.M, "M"},
        {KeyCode.N, "N"},
        {KeyCode.O, "O"},
        {KeyCode.P, "P"},
        {KeyCode.Q, "Q"},
        {KeyCode.R, "R"},
        {KeyCode.S, "S"},
        {KeyCode.T, "T"},
        {KeyCode.U, "U"},
        {KeyCode.V, "V"},
        {KeyCode.W, "W"},
        {KeyCode.X, "X"},
        {KeyCode.Y, "Y"},
        {KeyCode.Z, "Z"},
        {KeyCode.Alpha0, "0"},
        {KeyCode.Alpha1, "1"},
        {KeyCode.Alpha2, "2"},
        {KeyCode.Alpha3, "3"},
        {KeyCode.Alpha4, "4"},
        {KeyCode.Alpha5, "5"},
        {KeyCode.Alpha6, "6"},
        {KeyCode.Alpha7, "7"},
        {KeyCode.Alpha8, "8"},
        {KeyCode.Alpha9, "9"},
        {KeyCode.Space, "_"},
        {KeyCode.LeftAlt, "Al"},
        {KeyCode.RightAlt, "Al"},
        {KeyCode.LeftControl, "Ct"},
        {KeyCode.RightControl, "Ct"},
        {KeyCode.LeftShift, "Sh"},
        {KeyCode.RightShift, "Sh"},
        {KeyCode.CapsLock, "CL"},
        {KeyCode.UpArrow, "▲"},
        {KeyCode.DownArrow, "▼"},
        {KeyCode.LeftArrow, "◄"},
        {KeyCode.RightArrow, "►"},
        {KeyCode.LeftBracket, "["},
        {KeyCode.RightBracket, "]"},
        {KeyCode.Backslash, "\\"},
        {KeyCode.Semicolon, ";"},
        {KeyCode.Quote, "'"},
        {KeyCode.Period, "."},
        {KeyCode.Comma, ","},
        {KeyCode.Return, "En"},
        {KeyCode.Backspace, "←"},
        {KeyCode.Tab, "→"},
    };
    private int key_index = -1;
    public BackBehaviour backBehaviour;
    public void setKey(int index)
    {
        key_index = index;
        keys[key_index].key_text.text = "";
        backBehaviour.enabled = false;
    }

    private bool _firstRead = true;
    private Key[] _tempKeys;
    public KeyCode getKeyCode(string behaviour)
    {
        if (_firstRead)
        {
            _firstRead = false;
            keys = _tempKeys;
        }
        foreach (Key key in keys)
        {
            if(key.key_name == behaviour)
                return key.key_code;
        }
        return KeyCode.None;
    }
    private void Update() {
        if(key_index >= 0)
        {
            Key key = keys[key_index];
            if(Input.GetKey(KeyCode.Escape))
            {
                key.key_text.text = keyNameMapping[key.key_code];
                key_index = -1;
                backBehaviour.enabled = true;
            }
            foreach(KeyValuePair<KeyCode, string> kcode in keyNameMapping)
            {
                if (!Input.GetKey(kcode.Key))
                    continue;
                keys[key_index].key_code = kcode.Key;
                key.key_text.text = kcode.Value;
                key_index = -1;
                backBehaviour.enabled = true;
            }
        }
    }

    private const string KeyMappingPath = "keys.json";

    public void Persist()
    {
        Debug.Log("Saving key mapping...");
        var mapping = new KeyMappingData
        {
            mapping = keys.Select((k, i) => (i, (k.key_code, k.key_name))).ToDictionary(x => x.i, x => x.Item2)
        };
        SaveManager.Save(KeyMappingPath, mapping);
    }

    private struct KeyMappingData
    {
        [CanBeNull] public Dictionary<int, (KeyCode Code, string Name)> mapping;
    }

    private void Awake()
    {
        Debug.Log("Loading key mapping...");
        var mapping = SaveManager.Load<KeyMappingData>(KeyMappingPath);
        if (mapping.mapping is null) return;
        var internalKeys = keys.ToArray();
        foreach (var (idx, (code, keyName)) in mapping.mapping)
        {
            internalKeys[idx].key_code = code;
            internalKeys[idx].key_name = keyName;
            internalKeys[idx].key_text.text = keyNameMapping[code];
        }
        _tempKeys = internalKeys;
    }
}
