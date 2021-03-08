using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public int rotation;
    public Vector3 stopVelocity;
    public float floatSpeed;
    private bool stop;
    private bool start;
    private bool rotate;
    private Vector3 stopPosition;
    private Quaternion stopRotation;
    private float positionMarker;
    private float rotationMarker;
    public int ItemId { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        stop = false;
        start = false;
        rotate = false;
        StartCoroutine(DelayStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (stop)
        {
            positionMarker += Time.deltaTime;
            transform.position = Vector3.Lerp(stopPosition, stopPosition + new Vector3(0, 0.5f, 0), Mathf.PingPong(positionMarker, 1));
            if (!rotate)
            {
                rotationMarker += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(stopRotation, Quaternion.Euler(Vector3.zero), rotationMarker);
                if (transform.rotation.eulerAngles.x < 0.1f || transform.rotation.eulerAngles.z < 0.1f)
                {
                    rotate = true;
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y + Time.deltaTime * rotation, 0));
            }
        }

        // Pickup Loot
        if (false)
        {
#pragma warning disable CS0162 // Unerreichbarer Code wurde entdeckt.
            Inventory.instance.IncreaseResource(ItemId,1);
#pragma warning restore CS0162 // Unerreichbarer Code wurde entdeckt.
        }
    }

    private void FixedUpdate()
    {
        if (start && !stop)
        {
            Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
            if (velocity.x <= stopVelocity.x && velocity.y <= stopVelocity.y && velocity.z <= stopVelocity.z &&
                velocity.x >= -stopVelocity.x && velocity.y >= -stopVelocity.y && velocity.z >= -stopVelocity.z)
            {
                stopPosition = transform.position;
                stopRotation = transform.rotation;
                stop = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }

    private IEnumerator DelayStart()
    {  
        yield return new WaitForSecondsRealtime(1);
        start = true;
    }
}
