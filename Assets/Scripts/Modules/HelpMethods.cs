using UnityEngine;
using System.Threading;

public static class HelpMethods
{
    //returns hit.point or point on given distance
    public static Vector3 PointOfHit(this Ray ray, float dist = 100)
    {
        var isHit = Physics.Raycast(ray, out var hit);

        return isHit ? hit.point : ray.GetPoint(dist);


    }

    public static bool Between(this float value, float min, float max)
    {
        return value > min && value < max;
    }
    
    public static bool BetweenOuter(this float value, float min, float max)
    {
        return value <= min || value >= max;
    }

    public static float WrapAngle(this float value)
    {
        if (Mathf.Abs(value) > 180)
        {
            value -= 360 * Mathf.Sign(value);
        }
        
        return value;
    }
    
    public static Timer StartTimer(TimerCallback onElapse, int time)
    {
        var timer = new Timer(onElapse, null, time, Timeout.Infinite);
        
        return timer;
    }
}
