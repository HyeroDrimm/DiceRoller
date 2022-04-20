using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] GlobalChannelSO globalChannel;
    [SerializeField] private DiceSide[] sides;
    private bool isDiceThrown = false;
    private Rigidbody rb;

    public bool IsDiceThrown { get => isDiceThrown; set => isDiceThrown = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isDiceThrown = true && rb.velocity == Vector3.zero && rb.angularVelocity == Vector3.zero)
        {
            OnDiceStoped();
        }
    }



    private void OnDiceStoped()
    {
        isDiceThrown = false;
        foreach (var diceSide in sides)
        {
            if (diceSide.OnStop(out int number))
            {
                print(number);
                globalChannel.RaiseNumberLanded(number);
            }
        }
    }
}
