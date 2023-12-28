using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyClass 
{


    public KeyClass()
    {
        SetUpKeys2();
    }

    

    Dictionary<KeyType, KeyCode> keyDictionary = new Dictionary<KeyType, KeyCode>();



    public KeyCode GetKey(KeyType inputType)
    {
        return keyDictionary[inputType];
    }

    void ChangeKey(KeyType keyType, KeyCode key)
    {

    }

    

    void SetUpKeys2()
    {
        keyDictionary.Add(KeyType.MoveRight, KeyCode.D);
        keyDictionary.Add(KeyType.MoveLeft, KeyCode.A);
        keyDictionary.Add(KeyType.MoveUp, KeyCode.W);
        keyDictionary.Add(KeyType.MoveDown, KeyCode.S);

        keyDictionary.Add(KeyType.Inventory, KeyCode.Tab);
        keyDictionary.Add(KeyType.Interact, KeyCode.F);

        keyDictionary.Add(KeyType.Shoot, KeyCode.Mouse0);
        keyDictionary.Add(KeyType.Aim, KeyCode.Mouse1);
        keyDictionary.Add(KeyType.Reload, KeyCode.R);

        keyDictionary.Add(KeyType.UseShield, KeyCode.Q);
        keyDictionary.Add(KeyType.UseSword, KeyCode.E);

        keyDictionary.Add(KeyType.Dash, KeyCode.LeftShift);

        keyDictionary.Add(KeyType.Slot1, KeyCode.Alpha1);
        keyDictionary.Add(KeyType.Slot2, KeyCode.Alpha2);
        keyDictionary.Add(KeyType.Slot3, KeyCode.Alpha3);
    }


}

public enum KeyType
{
    MoveRight,
    MoveLeft,
    MoveUp,
    MoveDown,
    Interact,
    Inventory,
    Shoot,
    Aim,
    UseSword,
    UseShield,
    Reload,
    Dash,
    Slot1,
    Slot2,
    Slot3

}