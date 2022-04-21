using System.Collections;
using UnityEngine;

public class DragNRoll : MonoBehaviour
{
    [SerializeField] private GlobalChannelSO globalChannel;

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
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 10e3f, diceLayerMask))
            {
                heldGameObject = hit.rigidbody;
                heldGameObject.useGravity = false;
                heldGameObject.drag = 20f;


                Cursor.visible = false;
            }
        }


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (heldGameObject != null)
            {
                heldGameObject.useGravity = true;
                heldGameObject.drag = 0.1f;

                heldGameObject.GetComponent<Dice>().IsDiceThrown = true;
                globalChannel.RaiseDiceThrown();

                heldGameObject = null;
                Cursor.visible = true;
            }
        }

        if (heldGameObject != null)
        {
            var screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(heldGameObject.position).z);
            var worldPosition = cam.ScreenToWorldPoint(screenPosition);
            var distanceDelta = new Vector3(worldPosition.x, 2f, worldPosition.z) - heldGameObject.position;
            heldGameObject.AddForce(distanceDelta * 1000, ForceMode.Force);
            heldGameObject.AddForceAtPosition(distanceDelta, Vector3.up);
        }
    }

    public void SelfRollDice(Rigidbody dice)
    {
        dice.AddForceAtPosition(Vector3.up * 20000, new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1));
        dice.AddForce(new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1) * 20000);
        StartCoroutine(WaitAndRegisterDiceThrown(dice));
        globalChannel.RaiseDiceThrown();
    }

    private IEnumerator WaitAndRegisterDiceThrown(Rigidbody dice)
    {
        yield return new WaitForSeconds(0.1f);
        dice.GetComponent<Dice>().IsDiceThrown = true;
    }
}
