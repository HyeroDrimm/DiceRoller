using TMPro;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private Transform pointCheck;
    [SerializeField] private TMP_Text numberVisual;
    public bool OnStop(out int number)
    {
        number = this.number;
        return Physics.SphereCast(pointCheck.position, 0.2f, Vector3.up, out RaycastHit hit, Mathf.Infinity, 1 << 10);
    }

    private void OnValidate()
    {
        numberVisual.text = number.ToString();
    }
}
