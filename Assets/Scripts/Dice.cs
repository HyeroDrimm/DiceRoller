using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] GlobalChannelSO globalChannel;
    [SerializeField] private DiceSide[] sides;
    private bool isDiceThrown = false;
    private Rigidbody rb;
    private Vector3 comebackPosition = new Vector3(-2, 2, -4);

    public bool IsDiceThrown { get => isDiceThrown; set => isDiceThrown = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isDiceThrown == true && rb.velocity == Vector3.zero && rb.angularVelocity == Vector3.zero)
        {
            OnDiceStoped();
        }

        if (transform.position.y < -10)
        {
            rb.MovePosition(comebackPosition);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }



    private void OnDiceStoped()
    {
        isDiceThrown = false;
        foreach (var diceSide in sides)
        {
            if (diceSide.OnStop(out int number))
            {
                globalChannel.RaiseNumberLanded(number);
            }
        }
    }
}
