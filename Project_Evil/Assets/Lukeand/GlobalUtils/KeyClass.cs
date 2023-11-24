using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyClass 
{


    public KeyClass()
    {
        SetUpKeys2();
    }

    KeyCode keyMoveLeft;
    KeyCode keyMoveRight;
    KeyCode keyMoveUp;
    KeyCode keyMoveDown;
    KeyCode keyInventory;
    KeyCode keyInteract;
    KeyCode keyAim;
    KeyCode keyShoot;

    Dictionary<KeyType, KeyCode> keyDictionary = new Dictionary<KeyType, KeyCode>();



    public KeyCode GetKey(KeyType inputType)
    {
        return keyDictionary[inputType];
    }

    void ChangeKey(KeyType keyType, KeyCode key)
    {

    }

    void SetUpKeys()
    {
        keyMoveLeft = KeyCode.A;
        keyMoveRight = KeyCode.D;
        keyMoveDown = KeyCode.S;
        keyMoveUp = KeyCode.W;

        keyInventory = KeyCode.Tab;
        keyInteract = KeyCode.E;

        keyAim = KeyCode.Mouse1;
        keyShoot = KeyCode.Mouse0;  
    }

    void SetUpKeys2()
    {
        keyDictionary.Add(KeyType.MoveRight, KeyCode.D);
        keyDictionary.Add(KeyType.MoveLeft, KeyCode.A);
        keyDictionary.Add(KeyType.MoveUp, KeyCode.W);
        keyDictionary.Add(KeyType.MoveDown, KeyCode.S);

        keyDictionary.Add(KeyType.Inventory, KeyCode.Tab);
        keyDictionary.Add(KeyType.Interact, KeyCode.E);

        keyDictionary.Add(KeyType.Shoot, KeyCode.Mouse0);
        keyDictionary.Add(KeyType.Aim, KeyCode.Mouse1);

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
    Aim
}