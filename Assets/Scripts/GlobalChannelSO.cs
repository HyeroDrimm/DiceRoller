using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalChannelSO", menuName = "ScriptableObjects/GlobalChannelSO")]
public class GlobalChannelSO : ScriptableObject
{
    public Action<int> numberLanded;

    public void RaiseNumberLanded(int number)
    {
        numberLanded?.Invoke(number);
    }
}
