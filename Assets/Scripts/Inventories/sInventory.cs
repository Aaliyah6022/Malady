using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary> an inventory scriptable object </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "ZapChariot/Inventory" )]
public class sInventory : ScriptableObject, IEnumerable, IEnumerable<Slot>, IEnumerable<Item>
{
    //an inventory class we can directly run foreach, linq on with various indexers in a way we could later convert to an database checker later lol
    /* Feature list:
        slot indexing:      inventory[0] accesses first slot
        events:                  inventory.slotAdded.AddListener(slot=>{
                                            //do code for what happens when a slot is added for example
                                        })
        list casting to either a list of all slots or all items in the inventory:
                                        var l = (List<Item>)inventory;
                                        var s = (List<Slot>)inventory;
                                        //the list of all items will not contain empty nulls for missing items, 
                                        //it's literally a list of all items. this rule carries over to foreach lol
        foreach:                
                                        foreach (Slot slot in inventory) //loop over all slot in the inventory
        basic listing types 
                                        AddItem(item), RemoveItem(item), AddSlot(), AddSlot(slot), RemoveSlot(bool), SetSlotCount(count)
                                        //they return true or false if we were able to do their action
                                        if (inventory.AddItem(item)) {
                                            //what to do if we succeeded to add an item lol
                                        }
        item count and slot count:
                                        inventory.ItemCount; inventory.SlotCount;
    */

    public int slotCount = 16;

    //all we need to override an inventory for online is changing this single property from local storage to online
    List<Slot> slots {
        get {
            return s;
	    }
        set {
            s = value;
		}
	}

    List<Slot> s = new List<Slot>();
    /// <summary> how many slots are in this inventory </summary>
    public int SlotCount => slots.Count;
    /// <summary> how many items are in this inventory </summary>
    public int ItemCount => slots.Where( s => s.item != null ).Count();

    /// <summary> event for when a slot is added </summary>
    public UnityEvent<Slot> slotAdded;
    /// <summary> event for when a slot is removed </summary>
    public UnityEvent<Slot> slotRemoved;
    /// <summary> event for when we fail to remove a slot </summary>
    public UnityEvent slotRemoveFailed;
    /// <summary> event for when an item is inserted into a slot </summary>
    public UnityEvent<Item> itemAdded;
    /// <summary> event for when an item is removed from a slot </summary>
    public UnityEvent<Item> itemRemoved;
    /// <summary> event for when we fail to add an item to a slot </summary>
    public UnityEvent<Item> itemAddFailed;
    /// <summary> event for when we fail to remove an item from a slot </summary>
    public UnityEvent<Item> itemRemoveFailed;

    /// <summary> a cast to a list of slots </summary>
    public static explicit operator List<Slot>( sInventory b ) => b.slots.ToList();
    /// <summary> a cast to a list of items in slots </summary>
    public static explicit operator List<Item>( sInventory b ) => b.slots.Select(s=>s.item).ToList( );

    /// <summary> get a list of slots </summary>
    public List<Slot> ToList() {
        return slots.ToList( );
	}
    /// <summary> returns a list of all slots with items, or no items</summary>
    public List<Slot> GetOccupiedSlots( bool empty=false ) {
        return slots.Where( s => ( empty ) ? s.item == null : s.item != null ).ToList( );
    }
    /// <summary> indexer for slots </summary>
    public Slot this[ int i ]
    {
        get => slots[ i ];
        set => slots[ i ] = value;
    }
    /// <summary> use this to get a list of all slots that are or are not empty </summary>
    public List<Slot> this[ bool empty ]
    {
        get
        {
                return slots.Where( s => (empty) ? s.item == null : s.item != null ).ToList();
        }
    }
    /// <summary> attempt to add an item to the inventory </summary>
    public bool AddItem(Item item) {
        var emptyslots = slots.Where( x => x.item == null );

        if ( emptyslots.Any( ) )
        {
            emptyslots.First().item = item;
            itemAdded?.Invoke( item );
            return true;
        } else {
            itemAddFailed ?.Invoke(item);
            return false;
		}
	}
    /// <summary> attempt to remove an item from the inventory </summary>
    public bool RemoveItem( Item item )
    {
        var matchingslots = slots.Where( x => x.item == item );

        if ( matchingslots.Any( ) )
        {
            slots.First( ).item = null;
            itemRemoved?.Invoke( item );
            return true;
        }
        else
        {
            itemRemoveFailed?.Invoke( item );
            return false;
        }
    }
    /// <summary> attempt to target a total number of slots, will fail if there are items unless you ignore them </summary>
    public bool SetSlotCount (int count, bool ignoreItems = false) {
        var emptyslots = ( !ignoreItems ) ? slots.Where( x => x.item == null ) : slots;
        var total = count - emptyslots.Count( );
        if (total > 0)
		for ( int i = 0; i < total; i++ )
		{
                AddSlot( );
		}
        if (total < 0) {
			for ( int i = 0; i < Mathf.Abs(total); i++ )
			{
                if (!RemoveSlot(ignoreItems)) {
                    return false;                    
				}
			}
		}
        return true;
	}
    /// <summary> add a blank slot to the inventory </summary>
    public bool AddSlot( )
    {
        var slot = new Slot( );
        slots.Add( slot );
        slotAdded?.Invoke( slot );
        return true;
    }
    /// <summary> add predefined slot to the inventory </summary>
    public bool AddSlot( Slot slot )
    {
        slots.Add( slot );
        slotAdded?.Invoke( slot );
        return true;
    }
    /// <summary> attempt to remove a slot from the inventory. if ignoreitems is false if the slot has an item</summary>
    public bool RemoveSlot( bool ignoreItems = false ) {
        var emptyslots =(!ignoreItems) ? slots.Where( x => x.item == null ) : slots;
        if ( emptyslots.Any( ) )
        {
            var slot = emptyslots.First( );
            slotRemoved?.Invoke( slot );
            slots.Remove( slot );
            return true;
        } else {
            slotRemoveFailed?.Invoke( );
            return false;
		}

	}


    public IEnumerator GetEnumerator( )
    {
        return new InventorySlotEnumerator( slots );
    }

	IEnumerator<Item> IEnumerable<Item>.GetEnumerator( ) {
        return new InventoryItemEnumerator( slots );
	}

	IEnumerator<Slot> IEnumerable<Slot>.GetEnumerator( )
    {
        return new InventorySlotEnumerator( slots );
    }

    void OnValidate() {
        if ( SlotCount != slotCount )
            SetSlotCount( slotCount );
	}

    //private enumerator class for slots
    private class InventorySlotEnumerator : IEnumerator<Slot>
    {
        public List<Slot> items;
        int position = -1;

        //constructor
        public InventorySlotEnumerator( List<Slot> list )
        {
            items = list;
        }
        private IEnumerator<Slot> getEnumerator( )
        {
            return this;
        }
        //IEnumerator
        public bool MoveNext( )
        {
            position++;
            return ( position < items.Count );
        }
        //IEnumerator
        public void Reset( )
        {
            position = -1;
        }

        void IDisposable.Dispose( ) {
            items = null;
        }

		//IEnumerator
		public object Current
        {
            get
            {
                try
                {
                    return items[ position ];
                }
                catch ( IndexOutOfRangeException )
                {
                    throw new InvalidOperationException( );
                }
            }
        }

		Slot IEnumerator<Slot>.Current {
            get
            {
                try
                {
                    return items[ position ]; //idk?!
                }
                catch ( IndexOutOfRangeException )
                {
                    throw new InvalidOperationException( );
                }
            }
        }
	}  //end nested class

    //private enumerator class for items
    private class InventoryItemEnumerator : IEnumerator<Item>
    {
        public List<Item> items;
        int position = -1;

        //constructor
        public InventoryItemEnumerator( List<Slot> list )
        {
            items = list.Select(s=>s.item).ToList();
        }
        private IEnumerator<Item> getEnumerator( )
        {
            return this;
        }
        //IEnumerator
        public bool MoveNext( )
        {
            position++;
            return ( position < items.Count );
        }
        //IEnumerator
        public void Reset( )
        {
            position = -1;
        }

        void IDisposable.Dispose( ) => items = null;

        //IEnumerator
        public object Current
        {
            get
            {
                try
                {
                    return items[ position ];
                }
                catch ( IndexOutOfRangeException )
                {
                    throw new InvalidOperationException( );
                }
            }
        }

        Item IEnumerator<Item>.Current
        {
            get
            {
                try
                {
                    return items[ position ]; //idk?!
                }
                catch ( IndexOutOfRangeException )
                {
                    throw new InvalidOperationException( );
                }
            }
        }
    }  //end nested class
}
