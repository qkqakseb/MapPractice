using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GFun
{
    // 딕셔너리에 오브젝트 프리팹을 캐싱하는 함수
    public static void AddObjs(this Dictionary<string, GameObject> dict_, GameObject[] prefabs_)
    {
        foreach (var prefab_ in prefabs_)
        {
            Debug.Log(prefab_.name);
            dict_.Add(prefab_.name, prefab_);
        }
    }

    // 리스트를 섞는 함수
    public static void Shuffle<T>(this List<T> targetList, int shuffleCnt = 0)
    {
        if (shuffleCnt.Equals(0)) { shuffleCnt = (int)(targetList.Count * 2.0f); }

        int sourIdx = 0;
        int desIdx = 0;
        T tempVar = default(T);

        for (int i = 0; i < shuffleCnt; i++)
        {
            sourIdx = Random.Range(0, targetList.Count);
            desIdx = Random.Range(0, targetList.Count);

            tempVar = targetList[sourIdx];
            targetList[sourIdx] = targetList[desIdx];
            targetList[desIdx] = tempVar;
        }

    }// shuffle()


    // 리스트의 element 를 다른 값과 Swap 하는 함수
    public static void Swap<T>(this List<T> targetList, ref T swapValue, int swapIdx)
    {
        T tempValue = targetList[swapIdx];
        targetList[swapIdx] = swapValue;
        swapValue = tempValue;
    }

    // int의 값이 범위 안에 속해 있는지 검사하는 함수
    public static bool IsInRage(this int targetValue, int minInclude, int maxExclude)
    {
        return (minInclude <= targetValue && targetValue < maxExclude);
    }

    // float 의 값이 같은지 비교하는 함수
    public static bool IsEquals(this float targetValue, float compareValue)
    {
        bool IsEquals = Mathf.Approximately(targetValue, compareValue);
        return IsEquals;
    }
}
