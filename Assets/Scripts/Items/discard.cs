using UnityEngine;

public class discard : MonoBehaviour
{
    public LayerMask objectMask;
    public GameObject pickupObj;


    void discardWeapon(Weapon wep)
    {
        Collider target = detect();

        float x = rounded(States.stats.pos.x, 0.75f);
        float y = rounded(States.stats.pos.y, 0.75f);

        int indX = (int)rounded(((States.leveldata.radX + x) * 1.3f),0.1f);
        int indY = (int)rounded(((States.leveldata.radY + y) * 1.3f),0.1f);

        int finalIndex = (indX * States.leveldata.width) + indY;
        
        Tile curTile = States.tiles[finalIndex];

        GameObject obj = null;
        if (target)
        {
            obj = target.gameObject;
        }
        else
        {
            obj = Instantiate(pickupObj, new Vector3(x,0.1875f,y), Quaternion.identity);
        }

        Pickup pickup = obj.GetComponent<PickupMount>().pickup;
        pickup.weapons.Add(wep);

        pickup.applyBack();
        curTile.obj = JsonUtility.ToJson(pickup);
    }


    Collider detect()
    {
        Collider[] list = Physics.OverlapSphere(transform.position, 0.75f, objectMask);

        return list[0];
    }

    float rounded(float val, float round)
    {
        float mod = val % round;

        if (mod < (round/2))
        {
            return val-mod;
        }
        else
        {
            return val + (round-mod);
        }
    }
}
