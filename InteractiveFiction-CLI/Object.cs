﻿/*
Interactive Fiction Command line interpreter
Object.cs

© 2021 Iain Marais (il-Salvatore on Github)
Licence: Apache v2.0 or 3-clause BSD Licence

Please see www.apache.org/licenses/LICENSE-2.0.html || opensource.org/licenses/BSD-3-Clause for more information.

The scope of this project is to build a simple but efficient command line interpreter for a console-based interactive fiction engine,
Think classic Zork, where one entered commands and read the output.

This project will be entirely c# based.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InteractiveFiction_CLI
{
    partial class Program
    {
        //Top level class for Objects.
        public class Object
        {
            public bool HasStackCount { get; set; }
            public int StackCount { get; set; }
            public string LongName { get; set; }
            public string Name { get; set; }
            public Guid ID { get; set; }
            public static string ObjectName { get; set; }
            public static string ConsumableName { get; set; }
            public static string ContainerName { get; set; }
            public static Object CurrentObject { get; set; }
            public static Object TargetObject { get; set; }
            public class Container : Object
            {
                public bool IsLocked { get; set; }
                public bool IsOpen { get; set; }
                public static Container CurrentContainer { get; set; }
                public static Container TargetContainer { get; set; }
                public List<Object> ContainerInventory { get; set; }
                public List<Object.Consumable> ConsumablesInventory { get; set; }
                public Container()
                {

                }
                public Container(string containerName)
                {
                    Name = containerName;
                }
                public Container(string containerName, string longName)
                {
                    Name = containerName;
                    LongName = longName;
                }
                public Container(string containerName, string longName, List<Object> containerInventory)
                {
                    Name = containerName;
                    LongName = longName;
                    ContainerInventory = containerInventory;
                }
                public Container(string containerName, string longName, List<Object> containerInventory, bool isOpen, bool isLocked)
                {
                    Name = containerName;
                    LongName = longName;
                    ContainerInventory = containerInventory;
                    IsOpen = isOpen;
                    IsLocked = isLocked;
                }
                public Container(string containerName, string longName, List<Object.Consumable> consumablesInventory)
                {
                    Name = containerName;
                    LongName = longName;
                    ConsumablesInventory = consumablesInventory;
                }
                public Container(string containerName, string longName, List<Object.Consumable> consumablesInventory, bool isOpen, bool isLocked)
                {
                    Name = containerName;
                    LongName = longName;
                    ConsumablesInventory = consumablesInventory;
                    IsOpen = isOpen;
                    IsLocked = isLocked;
                }

                //This function handles querying the container inventory in each Loc. 
                //It gets the Loc from the instantiated objects of said type,
                //and stores them in a static CurrentLoc and uses that to handle the active instance.
            }
            public Object()
            {

            }
            public Object(string objectName)
            {
                Name = objectName;
            }
            public Object(string objectName, string longName)
            {
                Name = objectName;
                LongName = longName;
            }
            //Root class for consumables, it inherits traits from the Object class
            public class Consumable : Object
            {
                public bool IsConsumable = true;

                //Root class for Potions (aka drinks), inherits traits from the consumable class
                public class Potion : Consumable
                {
                    public bool IsPotion { get; set; }
                    public Potion()
                    {

                    }
                    public Potion(string potionName)
                    {
                        Name = potionName;
                    }
                    public Potion(string potionName, int stackCount)
                    {
                        Name = potionName;
                        StackCount = stackCount;
                    }
                    public Potion(string potionName, int stackCount, bool hasStackCount)
                    {
                        Name = potionName;
                        StackCount = stackCount;
                        HasStackCount = hasStackCount;
                    }
                }
                public Consumable()
                {

                }
                public Consumable(string consumableName)
                {
                    Name = consumableName;
                }
                public Consumable(string consumableName, int stackCount)
                {
                    Name = consumableName;
                    StackCount = stackCount;
                }
                public Consumable(string potionName, int stackCount, bool hasStackCount)
                {
                    Name = potionName;
                    StackCount = stackCount;
                    HasStackCount = hasStackCount;
                }
            }
            //Copied over from InvSys file.
            public class InventorySystem
            {
                private const int InvMaxSlots = 15;
                public List<InventoryEntry> InventoryEntries = new();
                public void AddItem(PickuppableObject item, int AddToStack)
                {
                    while (AddToStack > 0)
                    {
                        if (InventoryEntries.Exists(x => (x.InvObject.ID == item.ID) && (x.Amount < item.MaxStackCount)))
                        {
                            InventoryEntry inventoryEntry = InventoryEntries.First(x => (x.InvObject.ID == item.ID) && (x.Amount < item.MaxStackCount));
                            int MaxAddable = (item.MaxStackCount - inventoryEntry.Amount);
                            int AddStackCount = Math.Min(item.MaxStackCount, inventoryEntry.Amount);
                            inventoryEntry.AddToAmount(AddStackCount);
                            AddToStack -= AddStackCount;
                        }
                        else
                        {
                            if (InventoryEntries.Count < InvMaxSlots)
                            {
                                InventoryEntries.Add(new InventoryEntry(item, 0));
                            }
                            else
                            {
                                Console.WriteLine("There is no more inventory space");
                            }
                        }
                    }
                }
                public InventorySystem()
                {

                }
            }
            public class InventoryEntry
            {
                public PickuppableObject InvObject { get; set; }
                public int Amount { get; set; }
                public InventoryEntry(PickuppableObject item, int amount)
                {
                    InvObject = item;
                    Amount = amount;
                }
                public void AddToAmount(int amountToAdd)
                {
                    Amount += amountToAdd;
                }
            }
            public class PickuppableObject : Object
            {
                public int MaxStackCount { get; set; }

                public PickuppableObject()
                {
                    MaxStackCount = 1;
                }

                public class Weapon : PickuppableObject
                {

                }

                public class ConsumableItem : PickuppableObject
                {
                    public class Potion : ConsumableItem
                    {
                        public Potion()
                        {
                            MaxStackCount = 10;
                        }
                    }
                }
                public class Ammo : PickuppableObject
                {
                    public Ammo()
                    {
                        MaxStackCount = 50;
                    }
                }
            }

        }
    }
}

