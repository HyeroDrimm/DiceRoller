using UnityEngine;

public class BounceDice : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.AddForceAtPosition(transform.forward * 500, Vector3.up);
    }
}
