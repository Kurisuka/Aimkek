using System.Net.Configuration;
using System.Resources;
using System.Security.Authentication.ExtendedProtection;

namespace Lil_Vladimir
{
    #region aimtec usings and stuff
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;
    using Aimtec.SDK.TargetSelector;
    using Aimtec.SDK.Util.Cache;
    using Aimtec.SDK.Prediction.Skillshots;
    using Aimtec.SDK.Util;

    using Spell = Aimtec.SDK.Spell;
    #endregion

    internal class Vladimir
    {
        public static Menu MMenu = new Menu("lil Vladimir", "Lil Vladimir", true);
        public static Menu DifferentCombos, Misc, Self;
        public static Orbwalker Orbwalker = new Orbwalker();
        public static Obj_AI_Hero Player => ObjectManager.GetLocalPlayer();
        public Obj_AI_Hero target => TargetSelector.GetTarget(925);
        public static Spell Q, W, E, R;

        public void LoadSpells()
        {
            Q = new Spell(SpellSlot.Q, 600f);
            W = new Spell(SpellSlot.W, 350f);
            E = new Spell(SpellSlot.E, 610f);
            R = new Spell(SpellSlot.R, 625f);
            R.SetSkillshot(0.25f, 175f, 700f, false, SkillshotType.Circle);

        }

        public Vladimir()
        {
            Orbwalker.Attach(MMenu);
            var ComboStuff = new Menu("combo", "Combo Settings");
            {
                ComboStuff.Add(new MenuBool("useQ", "Use Q in Combo"));
                ComboStuff.Add(new MenuBool("useW", "Use W in Combo"));
                ComboStuff.Add(new MenuBool("useE", "Use E in Combo"));
                ComboStuff.Add(new MenuBool("useR", "Use R in Combo"));
                ComboStuff.Add(new MenuSeperator("rsetts"));
                ComboStuff.Add(new MenuBool("useRkillable", "Only if Killable in 1v1 Situations"));
                //ComboStuff.Add(new MenuSlider("Rifhits", "Use R if hits 1-5 enemies", 1, 1, 5));
                //ComboStuff.Add(new MenuSlider("Rifhealth", "Use R if HP %", 50, 50, 100));
                ComboStuff.Add(new MenuBool("usercast", "Use Semi-Cast R"));
                ComboStuff.Add(new MenuKeyBind("rcast", "Semi-Cast R", KeyCode.T, KeybindType.Press));

                #region maybe later
                /*DifferentCombos = new Menu("diffcombos", "Combo Logic");
                {
                    DifferentCombos.Add = new MenuList("combomodes", "Choose your Combo", new[] { "E->R->W->Q", "R->Q->E", " " });
                }*/
                #endregion

            }
            MMenu.Add(ComboStuff);

            var HarassStuff = new Menu("harass", "Harass Settings");
            {
                HarassStuff.Add(new MenuBool("useQ", "Use Q to Harass"));
                HarassStuff.Add(new MenuBool("UseE", "Use E to Harass"));

            }
            MMenu.Add(HarassStuff);

            var LastHit = new Menu("lasthit", "LastHit Settings");
            LastHit.Add(new MenuBool("lasthitQ", "Use Q to Lasthit Minions"));
            MMenu.Add(LastHit);

            var LaneClear = new Menu("laneclear", "LaneClear Settings");
            {
                LaneClear.Add(new MenuBool("useQ", "Use Q to Clear Lane"));

                LaneClear.Add(new MenuBool("useW", "Use W to Clear Lane"));
                LaneClear.Add(new MenuSlider("mintow", "X  Minions to W", 1, 1, 10));
                LaneClear.Add(new MenuBool("useE", "Use E to Clear Lane"));
                LaneClear.Add(new MenuSlider("mintoe", "X Minions to E", 1, 1, 10));
            }
            MMenu.Add(LaneClear);

            var JungleClear = new Menu("jungleclear", "JungleClear Settings");
            {
                JungleClear.Add(new MenuBool("useQ", "Use Q to Clear Jungle"));
                JungleClear.Add(new MenuBool("useW", "Use W to Clear Jungle"));
                JungleClear.Add(new MenuBool("useE", "Use E to Clear Jungle"));
            }
            MMenu.Add(JungleClear);

            var Killsteal = new Menu("ks", "Killsteal Settings");
            {
                Killsteal.Add(new MenuBool("useQ", "Use Q to Killsteal"));
                Killsteal.Add(new MenuBool("useE", "Use E to Killsteal"));
                Killsteal.Add(new MenuBool("useR", "Use R to Killsteal"));
            }
            MMenu.Add(Killsteal);

            var Drawings = new Menu("drawings", "Drawings Settings");
            {
                Drawings.Add(new MenuBool("drawQ", "Draw Q Range"));
                Drawings.Add(new MenuBool("drawE", "Draw E Range"));
                Drawings.Add(new MenuBool("drawR", "Draw R Range"));
                Drawings.Add(new MenuBool("drawdamage", "Draw Damage"));
            }
            MMenu.Add(Drawings);

            Gapcloser.Attach(MMenu, "W Anti-Gap");

            /*var skins = await GetSkins(Player.ChampionName);
            //misc ting
            Misc = new Menu("misc", "Miscellaneous");
            {
                Misc.Add(new MenuSeperator("skinting", "Choose your skin:"));
                Self = new Menu("self", "Skins") {
                        new MenuList("mySkin", "Champion Skin", skins, 0)};
                Self["mySkin"].OnValueChanged += (sender, args) => Game.OnUpdate += UpdateSkin;
                Misc.Add(Self);
            }*/

            MMenu.Attach();

            Render.OnPresent += Render_OnPresent;
            Game.OnUpdate += Game_OnUpdate;
            Gapcloser.OnGapcloser += OnGapcloser;

            LoadSpells();
            Console.WriteLine("Lil Vladimir - Loaded");
        }

        private void OnGapcloser(Obj_AI_Hero target, GapcloserArgs Args)
        {
            if (target != null && Args.EndPosition.Distance(Player) < W.Range && W.Ready)
            {

                W.Cast();


            }

        }
        #region bunch of statics
        private static void OnCastSpell(Obj_AI_Base sender, SpellBookCastSpellEventArgs Args)
        {
            if (sender.IsMe)
            {
                if (Args.Slot == SpellSlot.E)
                {
                    lastETime = Game.TickCount;
                }
            }
        }
        private static void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs Args)
        {
                if (sender.IsMe)
                {
                    if (Args.SpellSlot == SpellSlot.E)
                    {
                        lastETime = Game.TickCount;
                    }
                }
        }
        internal static int lastETime { get; set; }
        internal static bool isEActive
            =>
          ObjectManager.GetLocalPlayer().Buffs.Any(x => x.IsActive && x.Name.ToLower() == "vladimire") ||
          Game.TickCount - lastETime <= 1200 + Game.Ping;
        public static readonly List<string> SpecialChampions = new List<string> { "Annie", "Jhin" };
        public static Obj_AI_Hero GetBestEnemyHeroTarget()
        {
            return GetBestEnemyHeroTargetInRange(float.MaxValue);
        }

        public static List<Obj_AI_Minion> GetAllGenericMinionsTargets()
        {
            return GetAllGenericMinionsTargetsInRange(float.MaxValue);
        }

        public static List<Obj_AI_Minion> GetAllGenericMinionsTargetsInRange(float range)
        {
            return GetEnemyLaneMinionsTargetsInRange(range).Concat(GetGenericJungleMinionsTargetsInRange(range)).ToList();
        }

        public static List<Obj_AI_Base> GetAllGenericUnitTargets()
        {
            return GetAllGenericUnitTargetsInRange(float.MaxValue);
        }

        public static List<Obj_AI_Base> GetAllGenericUnitTargetsInRange(float range)
        {
            return GameObjects.EnemyHeroes.Where(h => h.IsValidTarget(range)).Concat<Obj_AI_Base>(GetAllGenericMinionsTargetsInRange(range)).ToList();
        }


        public static List<Obj_AI_Minion> GetEnemyLaneMinionsTargets()
        {
            return GetEnemyLaneMinionsTargetsInRange(float.MaxValue);
        }

        public static List<Obj_AI_Minion> GetEnemyLaneMinionsTargetsInRange(float range)
        {
            return GameObjects.EnemyMinions.Where(m => m.IsValidTarget(range)).ToList();
        }

        public static List<Obj_AI_Minion> GetGenericJungleMinionsTargets()
        {
            return GetGenericJungleMinionsTargetsInRange(float.MaxValue);
        }

        public static List<Obj_AI_Minion> GetGenericJungleMinionsTargetsInRange(float range)
        {
            return GameObjects.Jungle.Where(m => !GameObjects.JungleSmall.Contains(m) && m.IsValidTarget(range)).ToList();
        }


        public static Obj_AI_Hero GetBestEnemyHeroTargetInRange(float range)
        {
            var ts = TargetSelector.Implementation;
            var target = ts.GetTarget(range);
            if (target != null && target.IsValidTarget() && !Invulnerable.Check(target))
            {
                return target;
            }
            return null;
        }

        public static int SxOffset(Obj_AI_Hero target)
        {
            return SpecialChampions.Contains(target.ChampionName) ? 1 : 10;
        }

        public static int SyOffset(Obj_AI_Hero target)
        {
            return SpecialChampions.Contains(target.ChampionName) ? 3 : 20;
        }

        public static Obj_AI_Hero GetBestKillableHero(Spell spell, DamageType damageType = DamageType.True,
        bool ignoreShields = false)
        {
            return TargetSelector.Implementation.GetOrderedTargets(spell.Range).FirstOrDefault(t => t.IsValidTarget());
        }
        #endregion

        private void Render_OnPresent()
        {
            if (MMenu["drawings"]["drawQ"].Enabled)
            {
                Render.Circle(Player.Position, Q.Range, 40, Color.CornflowerBlue);
            }
            if (MMenu["drawings"]["drawE"].Enabled)
            {
                Render.Circle(Player.Position, E.Range, 40, Color.CornflowerBlue);
            }
            if (MMenu["drawings"]["drawR"].Enabled)
            {
                Render.Circle(Player.Position, R.Range, 40, Color.Crimson);
            }
            if (MMenu["drawings"]["drawdamage"].Enabled)
            {

                ObjectManager.Get<Obj_AI_Base>()
                    .Where(h => h is Obj_AI_Hero && h.IsValidTarget() && h.IsValidTarget(Q.Range * 2))
                    .ToList()
                    .ForEach(
                        unit =>
                        {

                            var heroUnit = unit as Obj_AI_Hero;
                            int width = 103;
                            int height = 8;
                            int xOffset = SxOffset(heroUnit);
                            int yOffset = SyOffset(heroUnit);
                            var barPos = unit.FloatingHealthBarPosition;
                            barPos.X += xOffset;
                            barPos.Y += yOffset;
                            var drawEndXPos = barPos.X + width * (unit.HealthPercent() / 100);
                            var drawStartXPos =
                                (float)(barPos.X + (unit.Health >
                                                     Player.GetSpellDamage(unit, SpellSlot.Q) +
                                                     Player.GetSpellDamage(unit, SpellSlot.E) +
                                                     Player.GetSpellDamage(unit, SpellSlot.R)
                                             ? width * ((unit.Health - (Player.GetSpellDamage(unit, SpellSlot.Q) +
                                                         Player.GetSpellDamage(unit, SpellSlot.E) +
                                                         Player.GetSpellDamage(unit, SpellSlot.R))) /
                                                        unit.MaxHealth * 100 / 100)
                                             : 0));

                            Render.Line(drawStartXPos, barPos.Y, drawEndXPos, barPos.Y, 8, true,
                                unit.Health < Player.GetSpellDamage(unit, SpellSlot.Q) +
                                Player.GetSpellDamage(unit, SpellSlot.E) +
                                Player.GetSpellDamage(unit, SpellSlot.R)
                                    ? Color.GreenYellow
                                    : Color.Orange);

                        });
            }
        }

        public void Game_OnUpdate()
        {
            if (Player.IsDead || MenuGUI.IsChatOpen())
            {
                return;
            }
            if (Player.HasBuff("VladimirSanguinePool"))
            {
                Orbwalker.Implementation.AttackingEnabled = false;
            }
            else
                Orbwalker.Implementation.AttackingEnabled = true;
            if (Player.HasBuff("VladimirE"))
            {
                Orbwalker.Implementation.AttackingEnabled = false;
            }
            else
                Orbwalker.Implementation.AttackingEnabled = true;
            DoKillsteal();

            switch (Orbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    DoCombo();
                    break;
                case OrbwalkingMode.Mixed:
                    DoHarass();
                    break;
                case OrbwalkingMode.Laneclear:
                    DoLane();
                    DoJungle();
                    break;
                case OrbwalkingMode.Lasthit:
                    DoLasthit();
                    break;


            }

            if (MMenu["combo"]["usercast"].Enabled)
            {
                if (MMenu["combo"]["rcast"].Enabled)
                {
                    if (R.Ready)
                    {
                        var target = GetBestEnemyHeroTargetInRange(R.Range - 20);

                        if (target.IsValidTarget() && target != null)
                        {
                            var rPred = R.GetPrediction(target);

                            if (rPred.HitChance >= HitChance.High)
                                R.Cast(rPred.CastPosition);
                        }

                    }
                }
            }
        }
        #region Killsteal
        private void DoKillsteal()
        {
            if (MMenu["ks"]["useQ"].Enabled && Q.Ready)
            {
                var bestTarget = GetBestKillableHero(Q, DamageType.Magical, false);
                if (bestTarget != null &&
                    Player.GetSpellDamage(bestTarget, SpellSlot.Q) >= bestTarget.Health &&
                    bestTarget.IsValidTarget(Q.Range))
                {
                    Q.Cast(bestTarget);
                }

            }
            if (MMenu["ks"]["useE"].Enabled && E.Ready)
            {
                var bestTarget = GetBestKillableHero(E, DamageType.Magical, false);
                if (bestTarget != null &&
                    Player.GetSpellDamage(bestTarget, SpellSlot.E) >= bestTarget.Health &&
                    bestTarget.IsValidTarget(E.Range - 10))
                {
                    if (isEActive)
                    {
                        if (Game.TickCount - lastETime > 1050 + Game.Ping)
                        {
                            E.Cast();
                        }
                    }
                    else
                    {
                        Player.SpellBook.CastSpell(SpellSlot.E);
                    }
                }

            }

            if (MMenu["ks"]["useR"].Enabled && R.Ready)
            {
                var bestTarget = GetBestKillableHero(R, DamageType.Magical, false);
                if (bestTarget != null &&
                    Player.GetSpellDamage(bestTarget, SpellSlot.R) >= bestTarget.Health &&
                    bestTarget.IsValidTarget(R.Range))
                {
                    R.Cast(bestTarget);
                }
            }

        }
        #endregion

        #region combo
        private void DoCombo()
        {
            bool useQ = MMenu["combo"]["useQ"].Enabled;
            bool useW = MMenu["combo"]["useW"].Enabled;
            bool useE = MMenu["combo"]["useE"].Enabled;
            bool useR = MMenu["combo"]["useR"].Enabled;
            bool useRkillable = MMenu["combo"]["useRkillable"].Enabled;

            if (useQ && Q.Ready)
            {
                if (target != null)
                {
                    if (target.IsValidTarget())
                    {
                        Q.Cast(target);
                    }
                }
            }
            if (useW && W.Ready)
            {
                if (target != null)
                {
                    if (target.IsValidTarget())
                    {
                        if (target.IsInRange(W.Range - 10))
                        {
                            W.Cast();
                        }

                    }
                }
            }
            if (useE && E.Ready)
            {
                if (target != null)
                {
                    if (target.IsValidTarget())
                    {
                        if (target.IsInRange(E.Range - 10))
                        {
                            if (isEActive)
                            {
                                if (Game.TickCount - lastETime > 1050 + Game.Ping)
                                {
                                    E.Cast();
                                }
                            }
                            else
                            {
                                Player.SpellBook.CastSpell(SpellSlot.E);
                            }

                        }
                    }
                }
            }
            if (useR)
            {
                if (target != null && useRkillable && target.Health < Player.GetSpellDamage(target, SpellSlot.R))
                {
                    //if (target != null)
                    {
                        if (target.IsValidTarget() && target.IsInRange(R.Range - 10))
                        {
                            R.Cast(target);
                        }
                    }
                }
                if (!useRkillable && target != null)
                {
                    if (target.IsValidTarget())
                    {
                        if (target.IsInRange(R.Range - 10))
                        {
                            R.Cast(target);
                        }
                    }
                }
            }

        }
        #endregion
        #region harassting
        private void DoHarass()
        {
            bool useQ = MMenu["harass"]["useQ"].Enabled;
            bool useE = MMenu["harass"]["useE"].Enabled;

            if (useQ)
            {
                if (target != null)
                {
                    if (target.IsInRange(Q.Range) && target.IsValidTarget())
                    {

                        Q.Cast(target);
                    }

                }
            }
            if (useE)
            {
                if (target != null)
                {
                    if (target.IsInRange(E.Range - 15) && target.IsValidTarget())
                    {
                        if (isEActive)
                        {
                            if (Game.TickCount - lastETime > 1050 + Game.Ping)
                            {
                                E.Cast();
                            }
                        }
                        else
                        {
                            Player.SpellBook.CastSpell(SpellSlot.E);
                        }

                    }

                }
            }
        }
        #endregion
        #region lasthit
        private void DoLasthit()
        {
            bool useQ = MMenu["lasthit"]["lasthitQ"].Enabled;

            if (useQ)
            {
                foreach (var minion in GetEnemyLaneMinionsTargetsInRange(Q.Range))
                {
                    if (minion.Health <= Player.GetSpellDamage(minion, SpellSlot.Q))
                    {
                        Q.CastOnUnit(minion);
                    }
                }
            }

        }
        #endregion
        #region jungleclear
        private void DoJungle()
        {
            bool useQ = MMenu["jungleclear"]["useQ"].Enabled;
            bool useW = MMenu["jungleclear"]["useW"].Enabled;
            bool useE = MMenu["jungleclear"]["useE"].Enabled;

            foreach (var jungleTarget in GetGenericJungleMinionsTargetsInRange(Q.Range))
            {
                if (useQ && jungleTarget.IsValidTarget(Q.Range) && jungleTarget != null && !jungleTarget.UnitSkinName.Contains("Plant"))
                {
                    Q.Cast(jungleTarget);
                }
                if (useW && jungleTarget.IsValidTarget(W.Range) && jungleTarget != null && !jungleTarget.UnitSkinName.Contains("Plant"))
                {
                    W.Cast();
                }


                if (useE && jungleTarget.IsValidTarget(E.Range) && jungleTarget != null && !jungleTarget.UnitSkinName.Contains("Plant"))
                {
                    if (isEActive)
                    {
                        if (Game.TickCount - lastETime > 1050 + Game.Ping)
                        {
                            E.Cast();
                        }
                    }
                    else
                    {
                        Player.SpellBook.CastSpell(SpellSlot.E);
                    }

                }
            }

        }
        #endregion
        #region laneclear
        private void DoLane()
        {
            bool useQ = MMenu["laneclear"]["useQ"].Enabled;
            bool useW = MMenu["laneclear"]["useW"].Enabled;
            bool useE = MMenu["laneclear"]["useE"].Enabled;

            if (useQ && Q.Ready)
            {
                foreach (var minion in GetEnemyLaneMinionsTargetsInRange(Q.Range))
                {
                    if (minion.IsValidTarget(Q.Range) && minion != null)
                    {
                        Q.Cast(minion);
                    }
                }
            }
            if (useW && W.Ready && !target.IsInRange(3000))
            {
                foreach (var minion in GetEnemyLaneMinionsTargetsInRange(W.Range))
                {
                    if (minion.IsValidTarget(W.Range) && minion != null)
                    {
                        if (GameObjects.EnemyMinions.Count(t =>
                                t.IsValidTarget(W.Range, false, false, Player.ServerPosition)) >=
                            MMenu["laneclear"]["mintow"].As<MenuSlider>().Value)
                        {
                            W.Cast();
                        }

                    }
                }
            }
            if (useE && E.Ready)
            {
                foreach (var minion in GetEnemyLaneMinionsTargetsInRange(E.Range))
                {
                    if (minion.IsValidTarget(E.Range - 10) && minion != null)
                    {
                        if (GameObjects.EnemyMinions.Count(t =>
                                t.IsValidTarget(E.Range, false, false, Player.ServerPosition)) >=
                            MMenu["laneclear"]["mintoe"].As<MenuSlider>().Value)
                        {
                            if (isEActive)
                            {
                                if (Game.TickCount - lastETime > 1050 + Game.Ping)
                                {
                                    E.Cast();
                                }
                            }
                            else
                            {
                                Player.SpellBook.CastSpell(SpellSlot.E);
                            }

                        }
                    }
                }
            }

        }
    }
}

        #endregion
        /*#region Misc
            private static void GameEventsOnGameStart()
            {

                Self["mySkin"].OnValueChanged += (sender, args) => Game.OnUpdate += UpdateSkin;

            }
            private static void UpdateSkin()
            {

                var mySkin = Misc["mySkin"].Value;
                Player.SetSkinId(mySkin);
                Game.OnUpdate -= UpdateSkin;
            }
            private static void GameObjectOnOnRevive(GameObject sender)
            {
                if (sender is Obj_AI_Hero hero)
                {
                    if (hero.IsMe)
                    {
                        var mySkin = Self["mySkin"].Value;
                        Player.SetSkinId(mySkin);
                    }
                }
            }

        }
    }
    #endregion*/

