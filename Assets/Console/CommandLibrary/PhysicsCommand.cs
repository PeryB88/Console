﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console
{
    [AutoRegisterConsoleCommand]
    public class PhysicsCommand : IConsoleCommand
    {
        public void Execute(string[] args)
        {
            int count = args.Length;
            if (count > 0)
            {
                switch (args[0].ToLower())
                {
                    case "simulation":
                        if(count > 1)
                        {
                            int steps;

                            if(args[1].ToLower() == "auto")
                            {
                                Physics.autoSimulation = true;
                                Console.Log(GetName(), "Set simulation physics to Auto");
                            }
                            else if (args[1].ToLower() == "off")
                            {
                                Physics.autoSimulation = false;
                                Console.Log(GetName(), "Set simulation physics to OFF");
                            }
                            else if (int.TryParse(args[1], out steps))
                            {
                                Physics.defaultSolverIterations = steps;
                                Console.Log(GetName(), string.Format("Set physics steps to {0}",steps));
                            }
                            else
                            {
                                Console.Log(GetName(), "Invalid simulation value : " + args[1], LogType.Error);
                                Console.Log(GetName(), GetHelp());
                            }
                        }
                        else
                        {
                            Console.Log(GetName(), string.Format("Simulation : {0}, {1} step(s)",
                                Physics.autoSimulation ? "auto" : "manual",
                                Physics.defaultSolverIterations
                                ));
                        }
                        break;
                    case "gravity":
                        if(count == 1)
                        {
                            Console.Log(GetName(), string.Format("Gravity Vector : {0}", Physics.gravity));
                        }
                        else if (count == 4)
                        {
                            float x, y, z;
                            if(float.TryParse(args[1], out x) && float.TryParse(args[2], out y) && float.TryParse(args[3], out z))
                            {
                                Physics.gravity = new Vector3(x, y, z);
                                Console.Log(GetName(), string.Format("Set Gravity Vector : {0}", Physics.gravity));
                            }
                        }
                        break;
                    default:
                        Console.Log(GetName(), string.Format("Invalid command : {0}", args[0]), LogType.Error);
                        break;
                }
            }
            else
                Console.Log(GetName(), GetHelp());
        }

        public string GetHelp()
        {
            return @"usage: physics <i>command</i>
    * physics simulation (auto,off,intValue) : sets simulation
    * physics gravity (x)(y)(z): sets gravity vector
";
        }

        public string GetName()
        {
            return "physics";
        }

        public IEnumerable<Console.Alias> GetAliases()
        {
            yield return Console.Alias.Get("gravity", "physics gravity");
        }

        public string GetSummary()
        {
            return "Performs physics debuging";
        }
    }

}
