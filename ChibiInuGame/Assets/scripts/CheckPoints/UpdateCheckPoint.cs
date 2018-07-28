using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpdateCheckPoint{

    public static int currentCheckPoint = 1;

    public static void NewCheckPoint(int newCheckPoint)
    {
        currentCheckPoint = newCheckPoint;
    }
}
