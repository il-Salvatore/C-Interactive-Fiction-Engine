﻿/*
Interactive Fiction Command line interpreter
Location.cs

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

namespace InteractiveFiction_CLI
{
    partial class Program
    {
        public class Location
        {
            public List<Location> Locations { get; set; }
            public LocID LocationID { get; set; }
            public bool IsCurrentLocation { get; set; }
            public bool HasExitN { get; set; }
            public bool HasExitS { get; set; }
            public bool HasExitE { get; set; }
            public bool HasExitW { get; set; }
            public bool HasExitUp { get; set; }
            public bool HasExitDown { get; set; }
            public string Name { get; set; }
            public string LongName { get; set; }
            public List<Object> LocationInventory { get; set; }
            public static Location CurrentLoc { get; set; }
            public static Location TargetLoc { get; set; }
            public static bool IsConnected { get; set; }

            public Location GetIsCurrentLocation(Location Loc1)
            {
                CurrentLoc = Loc1;
                if (IsCurrentLocation == true)
                {
                    CurrentLoc = this;
                }
                else
                {
                    CurrentLoc = null;
                }
                return CurrentLoc;
            }
            public static bool GetIsConnected(Location Loc1, Location Loc2)
            {
                if (Loc1.HasExitN && Loc2.HasExitS)
                {
                    IsConnected = true;
                }
                else if (Loc1.HasExitS && Loc2.HasExitN)
                {
                    IsConnected = true;
                }
                else if (Loc1.HasExitE && Loc2.HasExitW)
                {
                    IsConnected = true;
                }
                else if (Loc1.HasExitW && Loc2.HasExitE)
                {
                    IsConnected = true;
                }
                else if (Loc1.HasExitUp && Loc2.HasExitDown)
                {
                    IsConnected = true;
                }
                else if (Loc1.HasExitDown && Loc2.HasExitUp)
                {
                    IsConnected = true;
                }
                else
                {
                    IsConnected = false;
                }
                return IsConnected;
            }

            public Location()
            {

            }
            public Location(string locName)
            {
                Name = locName;
            }
            public Location(string locName, LocID locId)
            {
                LocationID = locId;
                Name = locName;
            }
            public Location(string locName, string longName, LocID locId) //Constructor for 3 params
            {
                Name = locName;
                LongName = longName;
                LocationID = locId;
            }
            public Location(string locName, string longName, LocID locId, bool exitN, bool exitS, bool exitE, bool exitW, bool exitUp, bool exitDown, List<Object> locInventory, bool isCurrentLoc)
            {
                Name = locName;
                LongName = longName;
                LocationID = locId;
                HasExitN = exitN;
                HasExitS = exitS;
                HasExitE = exitE;
                HasExitW = exitW;
                HasExitUp = exitUp;
                HasExitDown = exitDown;
                LocationInventory = locInventory;
                IsCurrentLocation = isCurrentLoc;
            }
            public static void GetLocationInventory()
            {
                if (CurrentLoc == null)
                {
                    Location currentLoc = Scene.Scene1.SceneLocations.Where(x => x.IsCurrentLocation == true).FirstOrDefault();
                    if (currentLoc == null)
                    {
                        //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                        Console.WriteLine("I see absolutely nothing.");
                    }
                    else
                    {
                        if (currentLoc.LocationInventory != null)
                        {
                            foreach (var InventoryObject in currentLoc.LocationInventory)
                            {
                                if (currentLoc.LocationInventory.Count > 0)
                                    Console.WriteLine($"{currentLoc.Name} contains: {InventoryObject.Name}");
                                else
                                    Console.WriteLine("There is nothing here.");
                            }
                        }
                        //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                        else if (currentLoc.LocationInventory == null)
                        {
                            Console.WriteLine("There is no storage space here.");
                        }
                    }
                }
                else if (CurrentLoc != null)
                {
                    Location currentLoc = CurrentLoc;
                    if (currentLoc == null)
                    {
                        //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                        Console.WriteLine("I see absolutely nothing.");
                    }
                    else
                    {
                        if (currentLoc.LocationInventory != null)
                        {
                            foreach (var InventoryObject in currentLoc.LocationInventory)
                            {
                                if (currentLoc.LocationInventory.Count > 0)
                                    Console.WriteLine($"{currentLoc.Name} contains: {InventoryObject.Name}");
                                else
                                    Console.WriteLine("There is nothing here.");
                            }
                        }
                        //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                        else if (currentLoc.LocationInventory == null)
                        {
                            Console.WriteLine("There is no storage space here.");
                        }
                    }
                }
            }
            public static Location GetNewLocation()
            {
                string TargetLocName = CommandProcessor.Command.Word2;
                Location targetLoc = new();
                targetLoc = Scene.Scene1.SceneLocations.Where(x => x.Name == TargetLocName).FirstOrDefault();
                Location.TargetLoc = targetLoc;
                return targetLoc;
            }
            public static void GetCurrentLocation()
            {
                if (CurrentLoc == null)
                {
                    Location currentLoc = Scene.Scene1.SceneLocations.Where(x => x.IsCurrentLocation == true).FirstOrDefault();
                    if (currentLoc == null)
                    {
                        //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                        Console.WriteLine($"I am nowhere yet.");
                    }
                    else
                    {
                        Console.WriteLine($"I am in {currentLoc.Name}");
                    }
                }
                else if (CurrentLoc != null)
                {
                    Location currentLoc = CurrentLoc;
                    if (currentLoc == null)
                    {
                        //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                        Console.WriteLine($"I am nowhere yet.");
                    }
                    else
                    {
                        Console.WriteLine($"I am in {currentLoc.Name}");
                    }
                }
            }
            public static void GoToNewLocation(Location targetLoc)
            {
            }
            public static void GoToNewLocation()
            {
                if (CurrentLoc == null)
                {
                    Location currentLoc = Scene.Scene1.SceneLocations.Where(x => x.IsCurrentLocation == true).FirstOrDefault();
                    Location targetLoc = GetNewLocation();
                    Location newLoc = targetLoc;
                    if (newLoc == targetLoc)
                    {
                        GetIsConnected(currentLoc, targetLoc);
                        //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                        if (targetLoc == null || !(GetIsConnected(currentLoc, targetLoc)))
                        {
                            Console.WriteLine("You can't go that way");
                        }
                        else
                        {
                            if (IsConnected == false && targetLoc != null)
                            {
                                Console.WriteLine($"I know where the {targetLoc.Name} is, but how do I get there?");
                            }
                            else
                            {
                                currentLoc = targetLoc;
                                currentLoc.IsCurrentLocation = true;
                                CurrentLoc = currentLoc;
                                Console.WriteLine($"I am now in {currentLoc.LongName}");
                            }

                        }
                    }
                }
                else if (CurrentLoc != null)
                {
                    Location currentLoc = CurrentLoc;
                    Location targetLoc = GetNewLocation();
                    Location newLoc = targetLoc;
                    if (newLoc == targetLoc)
                    {
                        GetIsConnected(currentLoc, targetLoc);
                        //Handle the error condition gracefully, thus avoiding exceptions and crashes.
                        if (targetLoc == null || !(GetIsConnected(currentLoc, targetLoc)))
                        {
                            Console.WriteLine("You can't go that way");
                        }
                        else
                        {
                            if (IsConnected == false && targetLoc != null)
                            {
                                Console.WriteLine($"I know where the {targetLoc.Name} is, but how do I get there?");
                            }
                            else
                            {
                                currentLoc = targetLoc;
                                currentLoc.IsCurrentLocation = true;
                                CurrentLoc = currentLoc;
                                Console.WriteLine($"I am now in {currentLoc.LongName}");
                            }

                        }
                    }
                }
            }
        }
    }
}