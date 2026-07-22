using UnityEngine;

public class PickupMount : MonoBehaviour
{
    public Pickup pickup = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Mount(Pickup pickUp)
    {
        pickup = pickUp;
        pickup.applyBack();
    }
}
