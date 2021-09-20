﻿#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.KeyBase;
using Trero.ClientBase.UIBase;
using Trero.ClientBase.VersionBase;
using Trero.Modules;

#endregion

namespace Trero
{
    internal static class Program
    {
        public static bool quit;
        public static bool limiter;
        public static bool unlimiter;
        public static readonly List<Module> Modules = new List<Module>();

        private static void Main(string[] args)
        {
            MCM.openGame();
            MCM.openWindowHost();

            // ReSharper disable once ObjectCreationAsStatement
            new Keymap();

            //Console.WriteLine(Game.screenData);

            new Thread(() => { Application.Run(new Overlay()); }).Start(); // UI Application

            Console.WriteLine(@"Registering modules...");

            Modules.Add(new ClickGUI());
            Modules.Add(new Antibot());

            Modules.Add(new Debug());
            Modules.Add(new AboveAura());
            Modules.Add(new AirJump());
            Modules.Add(new AirStuck());
            Modules.Add(new AntiImmoblie());
            Modules.Add(new AutoWalk());
            Modules.Add(new Bhop());
            Modules.Add(new BulkFly());
            Modules.Add(new ClickTP());
            Modules.Add(new ClosestPlayerDisplay());
            Modules.Add(new CreativeFly());
            Modules.Add(new Eject());
            Modules.Add(new FlickerExample());
            Modules.Add(new Fly());
            Modules.Add(new Friends());
            Modules.Add(new Gamemode());
            Modules.Add(new Glide());
            Modules.Add(new HighJump());
            Modules.Add(new Hitbox());
            Modules.Add(new HiveAntibot());
            Modules.Add(new InventoryMove());
            Modules.Add(new Jesus());
            Modules.Add(new Jetpack());
            Modules.Add(new KillGame());
            Modules.Add(new Killaura());
            Modules.Add(new LBSH());
            Modules.Add(new Limiter()); // CPU saver
            Modules.Add(new LongJump());
            Modules.Add(new MineplexFly());
            Modules.Add(new NoSwing());
            Modules.Add(new NoYFly());
            Modules.Add(new Noclip());
            Modules.Add(new Nofriends());
            Modules.Add(new Phase());
            Modules.Add(new PhaseDown());
            Modules.Add(new PhaseUp());
            Modules.Add(new PlayerDisplay());
            Modules.Add(new PlayerTP());
            Modules.Add(new Rapeaura());
            Modules.Add(new Reach());
            Modules.Add(new Sexaura());
            Modules.Add(new SlimeWall());
            Modules.Add(new Speed());
            Modules.Add(new Step());
            Modules.Add(new TPAura());
            Modules.Add(new Teleport());
            Modules.Add(new TriggerBot());
            Modules.Add(new Unlimiter()); // Remove safty ill make these a single module soon
            Modules.Add(new Zoom()); //dexter likes tudou

            Console.WriteLine(@"Registered modules!");

            //Console.WriteLine("LookingEntityID Address: " + (Game.localPlayer + 0x0).ToString("X"));

            //modules.Add(new TestModule());

            // Recall (Teleportation)
            // Tower (Veloicty & getKey)

            // Note that these are all possible but might not be added/changed just ideas ill slowly filter through over time
            // also rather do things i can think of ways to actually do externally so dont ask for anything else if you 100% know its impossible :(
            // for yaammi to do list :penisve:
            // CubeCraftFly
            // FastUse (Haven't put much thought into this so idk if its possible externally probs is)
            // AutoFish (Seems pretty simple and wont need pointers tbh)
            // Commands
            // Aimbot
            // NoHurtCam
            // InventoryDisplay // need local player inventory proxey
            // Zoom - Turns out this is also in local player as FieldOfView so i still need fov pointer Oof
            // Fov (FovPointer needed for W2S)
            // WorldToScreen/W2S (GameFunc)
            // Rader (Already possible btw as we have entitylist) // 100% adding these W2S ones btw
            // Tracers (W2S Required)
            // Waypoints (W2S Required)
            // ArrowTracers (W2S Required)
            // Reach
            // FastWater
            // CustomTablist
            // Nuker
            // BlockFly (Cant do this without scaffold so...)
            // Im never doing fightbot so fuck up
            // BowAimbot
            // ChestAura
            // CompassDisplay // when i get around to it i want to add waypoints into this aswell
            // FullBright
            // Fix scaffold
            // Noknockback

            // TreroInternal - Modules List
            // 

            // TODO: Commands -- i think ill do all the commands in about aweek if i get a good idea on where to put them
            // tp (x) (y) (z)
            // gm (registeryId)
            // eject
            // bind (module) (key)
            // unbind (module)
            // toggle (module)
            // friend (add/remove) (plr)
            // draw (module) (true/false) - Show or hide module from array list
            // coords
            // vclip (number)
            // waypoint (add/remove/list/tp) (waypointName)
            // rename (module) (name)
            // modules
            // block (plr) - ill replace there messages in chat to [Message deleted] x1(etc) using chat pointer
            // unblock
            // durability
            // nametags - if possible add armor and in hand display above their head

            Modules.Sort((c1, c2) => string.Compare(c2.name, c1.name, StringComparison.Ordinal)); // ABC Order

            VersionClass.setVersion(VersionClass.versions[0]);

            // Keymap.keyEvent += keyParse;

            Console.WriteLine(@"--- Trero Terminal ---");
            Console.WriteLine(@"Welcome to the trero terminal");
            Console.WriteLine(@"");
            Console.WriteLine(@"--- Trero Keybinds ---");
            Console.WriteLine(@"R - ClampJet");
            Console.WriteLine(@"P - Terminate Process");
            Console.WriteLine(@"Y - Hitboxes");
            Console.WriteLine(@"C - PhaseUp(ServerBypass)");
            Console.WriteLine(@"V - PhaseDown(ServerBypass)");

            // Console.WriteLine(Game.level.ToString("X"));

            new Thread(() => // Improved ticking modules
            {
                while (quit == false) // freeze
                {
                    if (limiter && !unlimiter)
                        Thread.Sleep(1);

                    if (!unlimiter)
                        Thread.Sleep(1);

                    //tickc++;
                    foreach (var mod in Modules.Where(mod => mod.enabled))
                        mod.OnTick();
                }
            }).Start();

            while (quit == false)
            {
            }
        }
        /*
         
        Game.isLookingAtBlock = 0;
        Game.SideSelect = 1;
        Game.SelectedBlock = Base.iVec3((int)Game.position.x, (int)Game.position.y - 1, (int)Game.position.z);

        Mouse.MouseEvent(Mouse.MouseEventFlags.MOUSEEVENTF_RIGHTDOWN);

        */
    }
}
