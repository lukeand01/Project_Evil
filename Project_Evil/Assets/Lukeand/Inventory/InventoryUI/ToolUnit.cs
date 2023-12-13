using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolUnit : MonoBehaviour
{
    //this is representation of the unit. it uses the description.
    //
    [SerializeField] TextMeshProUGUI toolTypeName;
    [SerializeField] Image portrait;
    [SerializeField] Image chargeBar;

    ToolClass toolClass = null;

}
