using TMPro;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private TMP_Text numberVisual;
    public bool OnStop(out int number)
    {
        number = this.number;
        return Vector3.Dot(Vector3.up, numberVisual.transform.forward * -1) >= 0.95f;
    }

    private void OnValidate()
    {
        numberVisual.text = number.ToString();
    }
}
