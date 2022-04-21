using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalChannelSO", menuName = "ScriptableObjects/GlobalChannelSO")]
public class GlobalChannelSO : ScriptableObject
{
    public Action<int> onNumberLanded;
    public Action onDiceThrown;

    public void RaiseNumberLanded(int number)
    {
        onNumberLanded?.Invoke(number);
    }

    public void RaiseDiceThrown()
    {
        onDiceThrown?.Invoke();
    }
}
