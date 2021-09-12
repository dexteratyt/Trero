﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trero.ClientBase;
using Trero.ClientBase.EntityBase;

namespace Trero.Modules
{
    class Rapeaura : Module
    {
        public Rapeaura() : base("Rapeaura", (char)0x07, "Combat") { } // Not defined

        public override void onTick()
        {
            if (Game.isNull) return;

            Actor plr = Game.getClosestPlayer();
            if (Game.position.Distance(plr.position) < 6f)
            {
                Game.SexActor(plr);
                Game.Attack(plr);
            }
        }
    }
}
