
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;


namespace LeWhite
{
    internal partial class LB
    {
        public static readonly List<string> SpecialChampions = new List<string> { "Annie", "Jhin" };

        public static int SxOffset(Obj_AI_Hero target)
        {
            return SpecialChampions.Contains(target.ChampionName) ? 33 : 30;
        }
       
        public static int SyOffset(Obj_AI_Hero target)
        {
            return SpecialChampions.Contains(target.ChampionName) ? 7 : 2;
        }
        public void DoDraws()
        {
            #region Draw Spells

            if (RootM["draw"]["disable"].As<MenuBool>().Enabled)
            {
                return;
            }
            if (RootM["draw"]["drawS"]["qdraw"].As<MenuBool>().Enabled && Q.Ready)
            {
                Render.Circle(MyHero.Position, Q.Range, 50, Color.Aquamarine);
            }
            if (RootM["draw"]["drawS"]["wdraw"].As<MenuBool>().Enabled && W.Ready)
            {
                Render.Circle(MyHero.Position, W.Range, 50, Color.Cornsilk);
            }
            if (RootM["draw"]["drawS"]["edraw"].As<MenuBool>().Enabled && E.Ready)
            {
                Render.Circle(MyHero.Position, E.Range, 50, Color.LightGreen);
            }
            if (RootM["draw"]["drawS"]["rdraw"].As<MenuBool>().Enabled && R.Ready)
            {
                Render.Circle(MyHero.Position, R.Range, 50, Color.Brown);
            }

            if (RootM["draw"]["combomode"].As<MenuBool>().Enabled)
            {
                string drawpos = "";

                switch (RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value)
                {
                    case 0:
                        drawpos = "Combo Mode: Q>E>W>R";
                        break;
                    case 1:
                        drawpos = "Combo Mode: Q>R>E>W";
                        break;
                    case 2:
                        drawpos = "Combo Mode: E>Q>W>R";
                        break;
                    case 3:
                        drawpos = "Combo Mode: E>W>Q>R";
                        break;
                    case 4:
                        drawpos = "Combo Mode: W>R>Q>E";
                        break;
                    case 5:
                        drawpos = "Combo Mode: W>Q>R>E";
                        break;
                    case 6:
                        drawpos = "Combo Mode: Q>R>W>E";
                        break;
                    case 7:
                        drawpos = "Combo Mode: Double Stun";
                        break;

                }

                var pos = MyHero.FloatingHealthBarPosition;
                pos.X += 40;
                pos.Y += 180;
                Render.Text(pos, Color.White, drawpos);
            }

            if (RootM["draw"]["damage"].As<MenuBool>().Enabled)
            {
                ObjectManager.Get<Obj_AI_Base>()
                    .Where(h => h is Obj_AI_Hero && h.IsValidTarget() && h.IsValidTarget(2000))
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
                                (float) (barPos.X + (unit.Health >
                                                     MyHero.GetSpellDamage(unit, SpellSlot.Q) +
                                                     MyHero.GetSpellDamage(unit, SpellSlot.E) +
                                                     MyHero.GetSpellDamage(unit, SpellSlot.W) +
                                                     MyHero.GetSpellDamage(unit, SpellSlot.Q)
                                             ? width * ((unit.Health - (MyHero.GetSpellDamage(unit, SpellSlot.Q) +
                                                                        MyHero.GetSpellDamage(unit, SpellSlot.E) +
                                                                        MyHero.GetSpellDamage(unit, SpellSlot.W) +
                                                                        MyHero.GetSpellDamage(unit, SpellSlot.Q))) /
                                                        unit.MaxHealth * 100 / 100)
                                             : 0));

                            Render.Line(drawStartXPos, barPos.Y, drawEndXPos, barPos.Y, 5, true,
                                unit.Health < MyHero.GetSpellDamage(unit, SpellSlot.Q) +
                                MyHero.GetSpellDamage(unit, SpellSlot.E) +
                                MyHero.GetSpellDamage(unit, SpellSlot.W) +
                                MyHero.GetSpellDamage(unit, SpellSlot.Q)
                                    ? Color.GreenYellow
                                    : Color.Orange);

                        });

            }
            /*var drawpos = "Default";
            if (RootM["combo"]["combologics"]["rlogic"].As<MenuList>().Value == 0)
            {
                var dp = "DF";
                switch (RootM["combo"]["combologics"]["rslogic"].As<MenuList>().Value)
                {
                    case 0:
                        dp = " Q";
                        break;
                    case 1:
                        dp = " E";
                        break;
                    case 2:
                        dp = " W";
                        break;
                }
                drawpos = "Manual R Selection :" + dp;
            }
            else
            {
                if (RootM["draw"]["combomode"].As<MenuBool>().Enabled)
                {
                    if (RootM["combo"]["combologics"]["select"].As<MenuList>().Value == 0)
                    {
                        drawpos = "Current Combo Mode: Dynamic Combo";
                    }
                    else if (RootM["combo"]["combologics"]["select"].As<MenuList>().Value == 1)
                    {
                        switch (RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value)
                        {
                            case 0:
                                drawpos = "Current Combo Mode: Q>E>W>R";
                                break;
                            case 1:
                                drawpos = "Current Combo Mode: Q>R>E>W";
                                break;
                            case 2:
                                drawpos = "Current Combo Mode: E>Q>W>R";
                                break;
                            case 3:
                                drawpos = "Current Combo Mode: E>W>Q>R";
                                break;
                            case 4:
                                drawpos = "Current Combo Mode: W>R>Q>E";
                                break;
                            case 5:
                                drawpos = "Current Combo Mode: W>Q>R>E";
                                break;
                            case 6:
                                drawpos = "Current Combo Mode: Q>R>W>E";
                                break;
                            case 7:
                                drawpos = "Current Combo Mode: Double Stun";
                                break;
                        }
                    }
                }

            }
            var pos = MyHero.FloatingHealthBarPosition;
            pos.X += 55;
            pos.Y += 55;
            Render.Text(pos, Color.White, drawpos);*/

                #endregion
            }
    }
}
