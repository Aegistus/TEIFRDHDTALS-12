using UnityEngine;

public static class Extensions
{
    public static Color SetAlpha(this Color c, float alpha)
    {
        c.a = alpha;
        return c;
    }

}