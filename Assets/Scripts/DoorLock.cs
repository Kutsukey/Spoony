using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] Material green;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Spoon")
        {
            GetComponent<MeshRenderer>().material = green;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f), Time.deltaTime);
            door.GetComponents<Door>()[0].DecreaseTimeToOpen();
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}