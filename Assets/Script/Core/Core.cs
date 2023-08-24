using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Core : MonoBehaviour
{
    private readonly List<CoreComponent> coreCompoents = new List<CoreComponent>();

    public void LogicUpdate() //call this in Player Update
    {
        foreach(CoreComponent comp in coreCompoents)
        {
            comp.LogicUpdate();
        }
    }

    public void AddCoreComponent(CoreComponent coreComp)
    {
        if (coreCompoents.Contains(coreComp)) return; //if already has this component  in list return no add 

        coreCompoents.Add(coreComp);
    }

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var comp = coreCompoents.OfType<T>().FirstOrDefault();

        if(comp) return comp; //if has this type of component return

        comp = GetComponentInChildren<T>();  //try to find component again
        if (comp) return comp;

        //if( there is no component will return null and log report
        Debug.Log($"not implement {typeof(T).Name} in {transform.parent.name}");
        return null;
    }
}
