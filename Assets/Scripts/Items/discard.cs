using UnityEngine;

public class discard : MonoBehaviour
{
    public LayerMask objectMask;
    public GameObject pickupObj;


    public void discardWeapon()
    {
        Collider target = detect();
        Weapon wep = States.inventory.weapons[States.itemPoint];


        float x = rounded(States.stats.pos.x, 1.5f);
        float z = rounded(States.stats.pos.z, 1.5f);

        int indX = (int)(States.leveldata.radX + (x / 1.5f));
        int indZ = (int)(States.leveldata.radY + (z / 1.5f));

        int finalIndex = (indX * States.leveldata.width) + indZ;
        
        Tile curTile = States.tiles[finalIndex];

        GameObject obj = null;
        Pickup pickup = null;

        if (target == null)
        {
            Vector3 spawn = new Vector3(x,0.1875f,z);
            Debug.Log(spawn);
            obj = Instantiate(pickupObj, spawn, Quaternion.identity);
            PickupMount pickupMount = obj.GetComponent<PickupMount>();
            pickup = new Pickup();
            pickupMount.Mount(pickup);
        }
        else
        {
            obj = target.gameObject;
            PickupMount pickupMount = obj.GetComponent<PickupMount>();
            pickup = pickupMount.pickup;
        }
        pickup.weapons.Add(wep);
        States.inventory.weapons.RemoveAt(States.itemPoint);

        pickup.applyBack();
        curTile.obj = JsonUtility.ToJson(pickup);
    }

    public void discardArmour()
    {
        Collider target = detect();
        Armour arm = States.inventory.armours[States.itemPoint];


        float x = rounded(States.stats.pos.x, 1.5f);
        float z = rounded(States.stats.pos.z, 1.5f);

        int indX = (int)(States.leveldata.radX + (x / 1.5f));
        int indZ = (int)(States.leveldata.radY + (z / 1.5f));

        int finalIndex = (indX * States.leveldata.width) + indZ;
        
        Tile curTile = States.tiles[finalIndex];

        GameObject obj = null;
        Pickup pickup = null;

        if (target == null)
        {
            Vector3 spawn = new Vector3(x,0.1875f,z);
            Debug.Log(spawn);
            obj = Instantiate(pickupObj, spawn, Quaternion.identity);
            PickupMount pickupMount = obj.GetComponent<PickupMount>();
            pickup = new Pickup();
            pickupMount.Mount(pickup);
        }
        else
        {
            obj = target.gameObject;
            PickupMount pickupMount = obj.GetComponent<PickupMount>();
            pickup = pickupMount.pickup;
        }
        pickup.armours.Add(arm);
        States.inventory.armours.RemoveAt(States.itemPoint);

        pickup.applyBack();
        curTile.obj = JsonUtility.ToJson(pickup);
    }

    public void discardItem()
    {
        Item item = States.inventory.items[States.itemPoint];
        States.itemCount--;
        States.inventory.items[States.itemPoint].count = States.itemCount;
        Debug.Log(States.inventory.items[States.itemPoint].count);

        Collider target = detect();

        float x = rounded(States.stats.pos.x, 1.5f);
        float z = rounded(States.stats.pos.z, 1.5f);

        int indX = (int)(States.leveldata.radX + (x / 1.5f));
        int indZ = (int)(States.leveldata.radY + (z / 1.5f));

        int finalIndex = (indX * States.leveldata.width) + indZ;
        
        Tile curTile = States.tiles[finalIndex];

        GameObject obj = null;
        Pickup pickup = null;

        if (target == null)
        {
            Vector3 spawn = new Vector3(x,0.1875f,z);
            Debug.Log(spawn);
            obj = Instantiate(pickupObj, spawn, Quaternion.identity);
            PickupMount pickupMount = obj.GetComponent<PickupMount>();
            pickup = new Pickup();
            pickupMount.Mount(pickup);
        }
        else
        {
            obj = target.gameObject;
            PickupMount pickupMount = obj.GetComponent<PickupMount>();
            pickup = pickupMount.pickup;
        }
        bool isHere = false;
            foreach(Item tem in pickup.items)
            {
                if (item.name == tem.name)
                {
                    Debug.Log("IS HERE");
                    isHere = true;
                    tem.count ++;
                }
            }
            if (!isHere)
            {
                item.count ++;
                pickup.items.Add(item);
            }

        pickup.applyBack();
        curTile.obj = JsonUtility.ToJson(pickup);

        if (States.inventory.items[States.itemPoint].count < 1)
        {
            States.itemName = null;
            States.itemDesc = null;
            States.itemIcon = null;
            States.inventory.items.RemoveAt(States.itemPoint);
        }  
    }


    Collider detect()
    {
        Collider[] list = Physics.OverlapSphere(States.stats.pos, 1.5f, objectMask);

        if (list.Length == 0)
        {
            return null;
        }

        return list[0];
    }

    float rounded(float val, float round)
    {
        if (val > 0)
        {
            float division = val/round;
            float mod = division % 1;
            if (mod >= 0.5)
            {
                return ((((int) division) - 1) * round);
            }
            else 
            {
                return (((int) division) * round);
            } 
        }
        else
        {
            float division = (val * -1)/round;
            float mod = division % 1;
            if (mod >= 0.5)
            {
                return ((((int) division) + 1) * round) * -1;
            }
            else 
            {
                return (((int) division) * round) * -1;
            } 
        }

        // float mod = val % round;
        // Debug.Log("VALUE: " + val + ", ROUND: " + round + ", MOD: " + mod);

        // if (mod < (round/2))
        // {
        //     return val-mod;
        // }
        // else
        // {
        //     return val + (round-mod);
        // }
    }
}
