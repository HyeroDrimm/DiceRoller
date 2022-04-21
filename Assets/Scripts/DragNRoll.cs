using System.Collections;
using UnityEngine;

public class DragNRoll : MonoBehaviour
{
    [SerializeField] private GlobalChannelSO globalChannel;
    [SerializeField] private float speedAfterIsRandom = 6;

    private Rigidbody heldGameObject;
    private Camera cam;
    private LayerMask diceLayerMask;

    private void Awake()
    {
        cam = Camera.main;
        diceLayerMask = LayerMask.GetMask("Dice");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            heldGameObject = null;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1e3f, diceLayerMask))
            {
                heldGameObject = hit.rigidbody;
                heldGameObject.useGravity = false;
                heldGameObject.drag = 20f;
                heldGameObject.mass = 50f;


                Cursor.visible = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (heldGameObject != null)
            {
                if (heldGameObject.velocity.magnitude + heldGameObject.angularVelocity.magnitude >= speedAfterIsRandom)
                {
                    heldGameObject.GetComponent<Dice>().IsDiceThrown = true;
                    globalChannel.RaiseDiceThrown();
                }
                else
                {
                    Debug.Log("Too low Speed for Throw");
                    heldGameObject.angularVelocity = Vector3.zero;
                    heldGameObject.velocity = Vector3.zero;
                }
                heldGameObject.useGravity = true;
                heldGameObject.drag = 0.5f;
                heldGameObject.mass = 300f;

                heldGameObject = null;
                Cursor.visible = true;
            }
        }

        if (heldGameObject != null)
        {
            var screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(heldGameObject.position).z);
            var worldPosition = cam.ScreenToWorldPoint(screenPosition);
            var distanceDelta = new Vector3(worldPosition.x, 2f, worldPosition.z) - heldGameObject.position;
            heldGameObject.AddForce(distanceDelta * 1e3f, ForceMode.Force);
            heldGameObject.AddForceAtPosition(distanceDelta * 10f, Vector3.up);
        }
    }

    public void SelfRollDice(Rigidbody dice)
    {
        dice.AddTorque(new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1) * 5e4f);
        dice.AddForce(Vector3.up * 8e4f);
        StartCoroutine(WaitAndRegisterDiceThrown(dice));
        globalChannel.RaiseDiceThrown();
    }

    private IEnumerator WaitAndRegisterDiceThrown(Rigidbody dice)
    {
        yield return new WaitForSeconds(0.1f);
        dice.GetComponent<Dice>().IsDiceThrown = true;
    }
}
