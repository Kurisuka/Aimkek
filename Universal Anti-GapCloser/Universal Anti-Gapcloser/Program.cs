using Aimtec;
using Aimtec.SDK.Events;
using Aimtec.SDK.Extensions;
using System;
using System.Linq;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Orbwalking;
using static Universal_Anti_Gapcloser.MenuStuff;
using static Universal_Anti_Gapcloser.Gapcloser;

using Spell = Aimtec.SDK.Spell;

namespace Universal_Anti_Gapcloser
{
    using Aimtec.SDK.Util;
    using System.Drawing;

    internal class Program
    {
        public Obj_AI_Hero target => Aimtec.SDK.TargetSelector.TargetSelector.GetTarget(925);
        public static Obj_AI_Hero Player => ObjectManager.GetLocalPlayer();
        public static Spell Q, W, E, R;

        static void Main(string[] args)
        {
            GameEvents.GameStart += OnStart;
            Game.OnUpdate += OnUpdate;
            Render.OnPresent += Render_OnPresent;
        }

        private static void Render_OnPresent()
        {
            if (MMenu["urspells"]["qsettss"]["debug"].Enabled)
            {
                Render.Circle(Player.Position, Q.Range, 100, Color.Red);
            }

            if (MMenu["urspells"]["wsettss"]["debug"].Enabled)
            {
                Render.Circle(Player.Position, W.Range, 100, Color.Red);
            }

            if (MMenu["urspells"]["esettss"]["debug"].Enabled)
            {
                Render.Circle(Player.Position, E.Range, 100, Color.Red);
            }

            if (MMenu["urspells"]["rsettss"]["debug"].Enabled)
            {
                Render.Circle(Player.Position, R.Range, 100, Color.Red);
            }
        }

        private static void OnStart()
        {
            MenuStuff.Initialize();

            Console.WriteLine("Universal Anti-Gapcloser - Loaded");
            Gapcloser.OnGapcloser += Gapclose;

            Q = new Spell(SpellSlot.Q);
            W = new Spell(SpellSlot.W);
            E = new Spell(SpellSlot.E);
            R = new Spell(SpellSlot.R);
        }

        private static void OnUpdate()
        {
            if (Player.IsDead || MenuGUI.IsChatOpen())
            {
                return;
            }

            if (Game.TickCount >= something)
            {
                canuse = false;
            }

            Q.Range = MMenu["urspells"]["qsettss"]["qrange"].As<MenuSlider>().Value;
            W.Range = MMenu["urspells"]["wsettss"]["wrange"].As<MenuSlider>().Value;
            E.Range = MMenu["urspells"]["esettss"]["erange"].As<MenuSlider>().Value;
            R.Range = MMenu["urspells"]["rsettss"]["rrange"].As<MenuSlider>().Value;




        }

        private static void Gapclose(Obj_AI_Base target, GapcloserArgs Args)
        {
            if (canuse)
            {

                bool useW = MMenu["urspells"]["wsettss"]["wUsage"].Enabled;
                bool useE = MMenu["urspells"]["esettss"]["eUsage"].Enabled;
                bool useR = MMenu["urspells"]["rsettss"]["rUsage"].Enabled;
                bool useQ = MMenu["urspells"]["qsettss"]["qUsage"].Enabled;
                if (!MMenu["urspells"]["onlyifcombo"].Enabled)
                {
                    if (useQ)
                    {

                        if (target != null)
                        {

                            if (target.IsValidTarget(Q.Range))
                            {

                                switch (MMenu["urspells"]["qsettss"]["chooseplzq"].Value)
                                {
                                    case 0:
                                        Q.Cast(target);
                                        break;
                                    case 1:
                                        var pos = Player.ServerPosition +
                                                  (Player.ServerPosition - Args.EndPosition).Normalized() * Q.Range;
                                        Q.Cast(pos);
                                        break;
                                    case 2:
                                        Q.Cast();
                                        break;
                                    case 3:
                                        Q.Cast(Args.EndPosition);

                                        break;
                                }
                            }
                        }
                    }

                    if (useW)
                    {
                        if (target != null && target.IsValidTarget(W.Range))
                        {
                            switch (MMenu["urspells"]["wsettss"]["chooseplzw"].Value)
                            {
                                case 0:
                                    W.Cast(target);
                                    break;
                                case 1:
                                    var pos = Player.ServerPosition +
                                              (Player.ServerPosition - Args.EndPosition).Normalized() * W.Range;
                                    W.Cast(pos);
                                    break;
                                case 2:
                                    W.Cast();
                                    break;
                                case 3:
                                    W.Cast(Args.EndPosition);
                                    break;
                            }
                        }
                    }

                    if (useE)
                    {
                        if (target != null && target.IsValidTarget(E.Range))
                        {
                            switch (MMenu["urspells"]["esettss"]["chooseplze"].Value)
                            {
                                case 0:
                                    E.Cast(target);
                                    break;
                                case 1:
                                    var pos = Player.ServerPosition +
                                              (Player.ServerPosition - Args.EndPosition).Normalized() * E.Range;
                                    E.Cast(pos);
                                    break;
                                case 2:
                                    E.Cast();
                                    break;
                                case 3:
                                    E.Cast(Args.EndPosition);
                                    break;
                            }
                        }
                    }

                    if (useR)
                    {
                        if (target != null && target.IsValidTarget(R.Range))
                        {
                            switch (MMenu["urspells"]["rsettss"]["chooseplzr"].Value)
                            {
                                case 0:
                                    R.Cast(target);
                                    break;
                                case 1:
                                    var pos = Player.ServerPosition +
                                              (Player.ServerPosition - Args.EndPosition).Normalized() * R.Range;
                                    R.Cast(pos);
                                    break;
                                case 2:
                                    R.Cast();
                                    break;
                                case 3:
                                    R.Cast(Args.EndPosition);
                                    break;
                            }
                        }
                    }
                }
            }
            if (MMenu["urspells"]["onlyifcombo"].Enabled)
            {
                bool useW = MMenu["urspells"]["wsettss"]["wUsage"].Enabled;
                bool useE = MMenu["urspells"]["esettss"]["eUsage"].Enabled;
                bool useR = MMenu["urspells"]["rsettss"]["rUsage"].Enabled;
                bool useQ = MMenu["urspells"]["qsettss"]["qUsage"].Enabled;
                if (Orbwalker.Implementation.Mode == OrbwalkingMode.Combo)
                {
                    if (useQ)
                    {

                        if (target != null)
                        {

                            if (target.IsValidTarget(Q.Range))
                            {

                                switch (MMenu["urspells"]["qsettss"]["chooseplzq"].Value)
                                {
                                    case 0:
                                        Q.Cast(target);
                                        break;
                                    case 1:
                                        var pos = Player.ServerPosition +
                                                  (Player.ServerPosition - Args.EndPosition).Normalized() * Q.Range;
                                        Q.Cast(pos);
                                        break;
                                    case 2:
                                        Q.Cast();
                                        break;
                                    case 3:
                                        Q.Cast(Args.EndPosition);
                                        break;
                                }
                            }
                        }
                    }

                    if (useW)
                    {
                        if (target != null && target.IsValidTarget(W.Range))
                        {
                            switch (MMenu["urspells"]["wsettss"]["chooseplzw"].Value)
                            {
                                case 0:
                                    W.Cast(target);
                                    break;
                                case 1:
                                    var pos = Player.ServerPosition +
                                              (Player.ServerPosition - Args.EndPosition).Normalized() * W.Range;
                                    W.Cast(pos);
                                    break;
                                case 2:
                                    W.Cast();
                                    break;
                                case 3:
                                    W.Cast(Args.EndPosition);
                                    break;
                            }
                        }
                    }

                    if (useE)
                    {
                        if (target != null && target.IsValidTarget(E.Range))
                        {
                            switch (MMenu["urspells"]["esettss"]["chooseplze"].Value)
                            {
                                case 0:
                                    E.Cast(target);
                                    break;
                                case 1:
                                    var pos = Player.ServerPosition +
                                              (Player.ServerPosition - Args.EndPosition).Normalized() * E.Range;
                                    E.Cast(pos);
                                    break;
                                case 2:
                                    E.Cast();
                                    break;
                                case 3:
                                    E.Cast(Args.EndPosition);
                                    break;
                            }
                        }
                    }

                    if (useR)
                    {
                        if (target != null && target.IsValidTarget(R.Range))
                        {
                            switch (MMenu["urspells"]["rsettss"]["chooseplzr"].Value)
                            {
                                case 0:
                                    R.Cast(target);
                                    break;
                                case 1:
                                    var pos = Player.ServerPosition +
                                              (Player.ServerPosition - Args.EndPosition).Normalized() * R.Range;
                                    R.Cast(pos);
                                    break;
                                case 2:
                                    R.Cast();
                                    break;
                                case 3:
                                    R.Cast(Args.EndPosition);
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
