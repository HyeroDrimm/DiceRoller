using UnityEngine;

public class BounceDice : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.AddForce(transform.forward * 5e4f);
    }
}
