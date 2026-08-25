using UnityEngine;

public class discard : MonoBehaviour
{
    public LayerMask objectMask;
    public GameObject pickupObj;
    private GameObject itemHolder;
    private Manager manager;


    void Start()
    {
        manager = GameObject.Find("EventSystem").GetComponent<Manager>();
        itemHolder = GameObject.Find("Items");
    }

    public void discardWeapon()
    {
        Collider target = detect();
        Weapon wep = States.inventory.weapons[States.itemPoint];


        float x = rounded(States.stats.pos.x, 1.5f);
        float z = rounded(States.stats.pos.z, 1.5f);

        int indX = (int)(States.leveldata.radX + (x / 1.5f));
        int indZ = (int)(States.leveldata.radY + (z / 1.5f));

        int finalIndex = (indZ * States.leveldata.width) + indX;
        
        Tile curTile = States.tiles[finalIndex];

        GameObject obj = null;
        Pickup pickup = null;

        if (target == null)
        {
            obj = Instantiate(pickupObj, new Vector3(x,0.1875f,z), Quaternion.identity);
            PickupMount pickupMount = obj.GetComponent<PickupMount>();
            pickup = new Pickup();
            pickup.loc = finalIndex;
            pickupMount.Mount(pickup);
            obj.transform.parent = itemHolder.transform;
        }
        else
        {
            obj = target.gameObject;
            PickupMount pickupMount = obj.GetComponent<PickupMount>();
            pickup = pickupMount.pickup;
        }
        pickup.weapons.Add(wep);
        States.inventory.weapons.RemoveAt(States.itemPoint);

        pickup.applyTo();
        string dropStr = JsonUtility.ToJson(pickup);
        curTile.obj = dropStr;
    }

    public void discardArmour()
    {
        Collider target = detect();
        Armour arm = States.inventory.armours[States.itemPoint];


        float x = rounded(States.stats.pos.x, 1.5f);
        float z = rounded(States.stats.pos.z, 1.5f);

        int indX = (int)(States.leveldata.radX + (x / 1.5f));
        int indZ = (int)(States.leveldata.radY + (z / 1.5f));

        int finalIndex = (indZ * States.leveldata.width) + indX;
        
        Tile curTile = States.tiles[finalIndex];

        GameObject obj = null;
        Pickup pickup = null;

        if (target == null)
        {
            obj = Instantiate(pickupObj, new Vector3(x,0.1875f,z), Quaternion.identity);
            PickupMount pickupMount = obj.GetComponent<PickupMount>();
            pickup = new Pickup();
            pickup.loc = finalIndex;
            pickupMount.Mount(pickup);
            obj.transform.parent = itemHolder.transform;
        }
        else
        {
            obj = target.gameObject;
            PickupMount pickupMount = obj.GetComponent<PickupMount>();
            pickup = pickupMount.pickup;
        }
        pickup.armours.Add(arm);
        States.inventory.armours.RemoveAt(States.itemPoint);

        pickup.applyTo();
        curTile.obj = JsonUtility.ToJson(pickup);
    }

    public void discardItem()
    {
        States.itemCount --;
        Item item = new Item(States.inventory.items[States.itemPoint].itemId);
        States.inventory.items[States.itemPoint].count = States.itemCount;
        int counter = States.inventory.items[States.itemPoint].count;
        Debug.Log(counter);

        Collider target = detect();

        float x = rounded(States.stats.pos.x, 1.5f);
        float z = rounded(States.stats.pos.z, 1.5f);

        int indX = (int)(States.leveldata.radX + (x / 1.5f));
        int indZ = (int)(States.leveldata.radY + (z / 1.5f));

        int finalIndex = (indZ * States.leveldata.width) + indX;
        
        Tile curTile = States.tiles[finalIndex];

        GameObject obj = null;
        Pickup pickup = null;

        if (target == null)
        {
            obj = Instantiate(pickupObj, new Vector3(x,0.1875f,z), Quaternion.identity);
            PickupMount pickupMount = obj.GetComponent<PickupMount>();
            pickup = new Pickup();
            pickup.loc = finalIndex;
            pickupMount.Mount(pickup);
            obj.transform.parent = itemHolder.transform;
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
                    isHere = true;
                    tem.count ++;
                }
            }
            if (isHere == false)
            {
                pickup.items.Add(item);
                
            }

        pickup.applyTo();
        string pickupStr = JsonUtility.ToJson(pickup);
        Debug.Log(indX + ", " + indZ);
        curTile.obj = pickupStr;
    
        if (counter <= 0)
        {
            States.itemName = null;
            States.itemDesc = null;
            States.itemIcon = null;
            States.hideCanvas = true;
            States.inventory.items.RemoveAt(States.itemPoint);
        }
        manager.clearButtons();
        manager.doItem();
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
    }
}
