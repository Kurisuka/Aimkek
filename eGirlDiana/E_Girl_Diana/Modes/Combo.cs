using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using System;

namespace E_Girl_Diana
{
    internal partial class egrilldiana
    {

        public void DoCombo()
        {

            bool useQ = RootM["combo"]["useq"].Enabled;
            bool useW = RootM["combo"]["usew"].Enabled;
            bool useE = RootM["combo"]["usee"].Enabled;
            bool useR = RootM["combo"]["user"].Enabled;
            bool Qmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Wmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.W).Cost;
            bool Emana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.E).Cost;
            bool Rmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.R).Cost;
            double Rdmg = Player.GetSpellDamage(target, SpellSlot.R);
            if (target != null)
            {
                if (target.IsValidTarget() || Player.ManaPercent() >
                    RootM["combo"]["mana"].Value)
                    if (!target.HasBuffOfType(BuffType.Invulnerability) || !target.HasBuff("UndyingRage")) 
                     DoTheCombo();
            }





        }
        public void DoTheCombo()
        {
            bool useQ = RootM["combo"]["useq"].Enabled;
            bool useW = RootM["combo"]["usew"].Enabled;
            bool useE = RootM["combo"]["usee"].Enabled;
            bool useR = RootM["combo"]["user"].Enabled;
            bool Qmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Wmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.W).Cost;
            bool Emana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.E).Cost;
            bool Rmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.R).Cost;
            double Rdmg = Player.GetSpellDamage(target, SpellSlot.R);
            if (RootM["combo"]["blacklist"]["Use on " + target.ChampionName.ToLower()].Enabled) return;

            switch (RootM["combo"]["rcombo"].Value)
            {

                case 0:
                    if (useQ && Qmana && Q.GetPrediction(target).CastPosition.Distance(Player) < Q.Range - 39)
                    {
                        Q.Cast(target);

                    }
                    if (useR && Rmana && !Q.Ready)
                    {
                        R.Cast(target);
                    }
                    if (useW && Wmana && Player.Distance(target) < W.Range)
                    {
                        if (target.IsDead) return;
                        else CastW();
                    }

                    if (useE && Emana && target.IsInRange(E.Range))
                    {
                        if (target.IsDead) return;
                        else
                            CastE(target);
                    }
                    break;
                case 1:
                    if (useQ && Qmana && Q.GetPrediction(target).CastPosition.Distance(Player) < Q.Range - 39)
                    {
                        Q.Cast(target);

                    }
                    if (useR && Rmana && (IsMarked(target) || Rdmg > target.Health))
                    {
                        R.Cast(target);
                    }
                    if (useW && Wmana && Player.Distance(target) < W.Range)
                    {
                        if (target.IsDead) return;
                        else CastW();
                    }

                    if (useE && Emana && target.IsInRange(E.Range))
                    {
                        if (target.IsDead) return;
                        else
                            CastE(target);
                    }
                    break;
                case 2:
                    if (useQ && Qmana && Q.GetPrediction(target).CastPosition.Distance(Player) < Q.Range - 39)
                    {
                        Q.Cast(target);

                    }
                    if (useW && Wmana && Player.Distance(target) < W.Range)
                    {
                        if (target.IsDead) return;
                        else CastW();
                    }
                    if (useR && Rmana)
                    {
                        R.Cast(target);
                    }
                    if (useE && Emana && target.IsInRange(E.Range))
                    {
                        if (target.IsDead) return;
                        else
                            CastE(target);
                    }
                    break;
            }
        }
    }
}
