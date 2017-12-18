
using System;
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;

namespace LeWhite
{
    internal partial class LB
    {

        public void DoLaneClear()
        {
            bool Qmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Wmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.W).Cost;
            bool Emana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.E).Cost;
            bool Rmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.R).Cost;
            int qcount = 0;
            foreach (var minion in GameObjects.EnemyMinions.Where(m => m.IsValidTarget(1400)))
            {
                if (IsPassiveM(minion))
                {
                    qcount += 1;
                }
                if (minion != null)
                {
                    if (RootM["farm"]["laneclear"]["useW"].As<MenuBool>().Enabled && W.Ready && Wmana)
                    {
                        if (GameObjects.EnemyMinions.Count(t => t.IsValidTarget(W.Width, false, false,
                                W.GetPrediction(minion).CastPosition)) >
                            RootM["farm"]["laneclear"]["wcount"].As<MenuSliderBool>().Value)
                        {
                            W.Cast(W.GetPrediction(minion).CastPosition);
                        }
                    }
                    if (RootM["farm"]["laneclear"]["useE"].As<MenuBool>().Enabled && E.Ready && Emana)
                    {
                        CastE(minion);
                    }

                    if (RootM["farm"]["laneclear"]["useQ"].As<MenuBool>().Enabled && Q.Ready && Qmana && qcount >=
                        RootM["farm"]["laneclear"]["qcount"].As<MenuSliderBool>().Value)
                    {
                        this.CastQ(minion);
                    }
                    if (RootM["farm"]["laneclear"]["useR"].As<MenuBool>().Enabled && R.Ready && Rmana)
                    {
                        if (GameObjects.EnemyMinions.Count(t => t.IsValidTarget(R.Range, false, false,
                                W.GetPrediction(minion).CastPosition)) >
                            RootM["farm"]["laneclear"]["rcount"].As<MenuSlider>().Value)
                        {
                            if (IsR1())
                            {
                                R.Cast();
                                this.CastW(W.GetPrediction(minion).CastPosition);
                            }
                        }
                    }
                }

            }
        }
    }
}
