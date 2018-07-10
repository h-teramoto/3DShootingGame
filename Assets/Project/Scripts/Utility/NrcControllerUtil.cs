using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NrcControllerUtil 
{
    public static void ControllerPause(INrcController nrcController)
    {
       nrcController.Pause();
    }
    
    public static void ControllerListPause(List<INrcController> nrcControllerList)
    {
        foreach(INrcController inc in nrcControllerList)
        {
            ControllerPause(inc);
        }
    }

    public static void ControllerBeginning(INrcController nrcController)
    {
        nrcController.Beginning();
    }

    public static void ControllerListBeginning(List<INrcController> nrcControllerList)
    {
        foreach (INrcController inc in nrcControllerList)
        {
            ControllerBeginning(inc);
        }
    }


}
