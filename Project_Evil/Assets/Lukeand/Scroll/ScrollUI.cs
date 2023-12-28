using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUI : MonoBehaviour
{
    [SerializeField] ScrollUIUnit scrollTemplate;
    List<ScrollUIUnit> scrollUnitList = new();
    [SerializeField] Transform scrollContainer;

    public void CreateScrollUnit(ScrollClass scroll)
    {
        ScrollUIUnit newObject = Instantiate(scrollTemplate, Vector3.zero, Quaternion.identity);
        newObject.SetUp(scroll);
        newObject.transform.parent = scrollContainer;
        scrollUnitList.Add(newObject);
    }

    public void ControlAllUnitsAnimation(bool isSelectingNewScroll)
    {
        foreach (var unit in scrollUnitList)
        {
            if(isSelectingNewScroll)
            {
                unit.StartSelectingScroll();
            }
            else
            {
                unit.StopSelectingScroll();
            }
        }
    }

    
}

