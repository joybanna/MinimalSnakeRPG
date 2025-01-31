using System.Collections.Generic;

public static class Random
{
    public static List<T> Shuffle<T>(this List<T> list)
    {
        var newList = new List<T>(list);
        for (int i = newList.Count - 1; i >= 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, newList.Count);
            T temp = newList[randomIndex];
            newList[randomIndex] = newList[i];
            newList[i] = temp;
        }

        return newList;
    }
}