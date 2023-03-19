using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum InputAction
{
    MovementLeft,
    MovementRight,
    MovementJump,
    Interact
}

public class InputManager : Manager<InputManager>
{
    private readonly Dictionary<InputAction, KeyCode> _mappings = new();

    public void SetMapping(InputAction inputAction, KeyCode key)
    {
        _mappings[inputAction] = key;
        SaveManager.Save(KeybindsPath, _mappings);
    }

    public bool GetKeyDown(InputAction inputAction) => Input.GetKeyDown(_mappings[inputAction]);
    public bool GetKey(InputAction inputAction) => Input.GetKey(_mappings[inputAction]);
    public bool GetKeyUp(InputAction inputAction) => Input.GetKeyUp(_mappings[inputAction]);
    public KeyCode GetKeyCode(InputAction inputAction) => _mappings.TryGetValue(inputAction, out var code) ? code : KeyCode.None;
    public string GetKeyDisplayName(InputAction inputAction) => GetKeyDisplayName(GetKeyCode(inputAction));

    public static readonly IReadOnlyDictionary<KeyCode, string> KeyNameMapping = new Dictionary<KeyCode, string>
    {
        { KeyCode.A, "A" },
        { KeyCode.B, "B" },
        { KeyCode.C, "C" },
        { KeyCode.D, "D" },
        { KeyCode.E, "E" },
        { KeyCode.F, "F" },
        { KeyCode.G, "G" },
        { KeyCode.H, "H" },
        { KeyCode.I, "I" },
        { KeyCode.J, "J" },
        { KeyCode.K, "K" },
        { KeyCode.L, "L" },
        { KeyCode.M, "M" },
        { KeyCode.N, "N" },
        { KeyCode.O, "O" },
        { KeyCode.P, "P" },
        { KeyCode.Q, "Q" },
        { KeyCode.R, "R" },
        { KeyCode.S, "S" },
        { KeyCode.T, "T" },
        { KeyCode.U, "U" },
        { KeyCode.V, "V" },
        { KeyCode.W, "W" },
        { KeyCode.X, "X" },
        { KeyCode.Y, "Y" },
        { KeyCode.Z, "Z" },
        { KeyCode.Alpha0, "0" },
        { KeyCode.Alpha1, "1" },
        { KeyCode.Alpha2, "2" },
        { KeyCode.Alpha3, "3" },
        { KeyCode.Alpha4, "4" },
        { KeyCode.Alpha5, "5" },
        { KeyCode.Alpha6, "6" },
        { KeyCode.Alpha7, "7" },
        { KeyCode.Alpha8, "8" },
        { KeyCode.Alpha9, "9" },
        { KeyCode.Space, "_" },
        { KeyCode.LeftAlt, "Al" },
        { KeyCode.RightAlt, "Al" },
        { KeyCode.LeftControl, "Ct" },
        { KeyCode.RightControl, "Ct" },
        { KeyCode.LeftShift, "Sh" },
        { KeyCode.RightShift, "Sh" },
        { KeyCode.CapsLock, "CL" },
        { KeyCode.UpArrow, "▲" },
        { KeyCode.DownArrow, "▼" },
        { KeyCode.LeftArrow, "◄" },
        { KeyCode.RightArrow, "►" },
        { KeyCode.LeftBracket, "[" },
        { KeyCode.RightBracket, "]" },
        { KeyCode.Backslash, "\\" },
        { KeyCode.Semicolon, ";" },
        { KeyCode.Quote, "'" },
        { KeyCode.Period, "." },
        { KeyCode.Comma, "," },
        { KeyCode.Return, "En" },
        { KeyCode.Backspace, "←" },
        { KeyCode.Tab, "→" }
    };

    public static string GetKeyDisplayName(KeyCode code) => KeyNameMapping.TryGetValue(code, out var name) ? name : "??";

    private static KeyCode GetDefaultKey(InputAction inputAction)
    {
        return inputAction switch
        {
            InputAction.MovementLeft => KeyCode.A,
            InputAction.MovementRight => KeyCode.D,
            InputAction.MovementJump => KeyCode.Space,
            InputAction.Interact => KeyCode.E,
            _ => KeyCode.None
        };
    }

    private const string KeybindsPath = "keybinds.json";

    protected override void Prepare()
    {
        // Load mappings from save file
        var savedMappings = SaveManager.Load<Dictionary<InputAction, KeyCode>>(KeybindsPath);
        if (savedMappings != null)
        {
            _mappings.Clear();
            foreach (var (action, key) in savedMappings)
            {
                _mappings[action] = key;
            }
        }

        // Set missing mappings to default
        foreach (InputAction action in Enum.GetValues(typeof(InputAction)))
        {
            if (!_mappings.ContainsKey(action))
            {
                _mappings[action] = GetDefaultKey(action);
            }
        }
    }
}
