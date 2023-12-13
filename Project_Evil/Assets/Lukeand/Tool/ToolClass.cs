using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolClass
{
    //we check for durability here.

    ItemToolData data;
    public int condition {  get; private set; }

    public ToolClass(ItemToolData data, int condition = 100)
    {
        this.data = data;
        this.condition = condition;
    }
}
