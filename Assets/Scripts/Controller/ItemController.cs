using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public int rotation;
    public Vector3 stopVelocity;
    public float floatSpeed;
    public float pickupDistance;
    public float pickupSpeed;

    private bool stop;
    private bool start;
    private bool rotate;
    private Vector3 stopPosition;
    private Quaternion stopRotation;
    private float positionMarker;
    private float rotationMarker;
    private bool collecting;
    public int ItemId { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        stop = false;
        start = false;
        rotate = false;
        collecting = false;
        StartCoroutine(DelayStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (stop && !collecting)
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
        if (collecting)
        {
            float distance = Vector3.Distance(transform.position, Playerstats.mousePosition);
            if (distance < pickupDistance)
            {
                // TODO Fancy Animation and pickup effects
                Inventory.instance.IncreaseResource(ItemId, 1);
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (start && !stop && !collecting)
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

        if (collecting)
        {
            transform.position = Vector3.MoveTowards(transform.position, Playerstats.mousePosition, pickupSpeed * Time.deltaTime);
        }
    }

    private IEnumerator DelayStart()
    {  
        yield return new WaitForSecondsRealtime(1);
        start = !collecting;
    }

    public void Collect()
    {
        collecting = true;
    }
}
