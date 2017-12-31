
using System;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util;

namespace LeWhite
{
    internal partial class LB
    {

        public void DoCombo()
        {
            if (target == null || !target.IsValid || MyHero.ManaPercent() <
                RootM["combo"]["Mana"].As<MenuSliderBool>().Value ||
                RootM["combo"]["blacklist"]["use" + target.ChampionName.ToLower()].As<MenuBool>().Enabled)
            {
                return;
            }

            if (RootM["combo"]["combologics"]["rlogic"].As<MenuList>().Value == 0)
            {
                NewComboLogic();
            }

            if (RootM["combo"]["combologics"]["rlogic"].As<MenuList>().Value == 2)
            {
                ManualCombo();
            }
            else
            {

                DynamicCombo();
               
            }

        }

        private void DynamicCombo()
        {

            bool useQ = RootM["combo"]["useQ"].As<MenuBool>().Enabled;
            bool useW = RootM["combo"]["useW"].As<MenuBool>().Enabled;
            bool useE = RootM["combo"]["useE"].As<MenuBool>().Enabled;
            bool useR = RootM["combo"]["useR"].As<MenuBool>().Enabled;
            bool Qmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Wmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.W).Cost;
            bool Emana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.E).Cost;
            bool Rmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.R).Cost;
            

            if (MyHero.Distance(target) < W.Range) //wQRE
            {
                if (Wmana && useW && IsW1())
                {
                    CastW(W.GetPrediction(target).CastPosition);
                }

                if (useQ && Qmana && IsPassive(target))
                {
                    CastQ(target);
                }

                if (Rmana && useR && IsR1())
                {               
                    CastR("RQ", target);
                }

                if (useE && Emana)
                {
                    E.Cast(target);
                }
            }
            if (MyHero.Distance(target) < E.Range) //REQEW
            {
                if (Rmana && useR && IsR1())
                {
                    CastR("RE", target);
                }

                if (useQ && Qmana && IsPassive(target))
                {
                    CastQ(target);
                }

                if (Emana && useE)
                {
                    E.Cast(target);
                }

                if (useW && Wmana && IsW1())
                {
                    CastW(W.GetPrediction(target).CastPosition);
                }

            }
            else if (target.IsValidTarget(W.Range + Q.Range)) //gapclose combo W-R(E)-E-Q
            {
                var pos = MyHero.ServerPosition.Extend(target.ServerPosition, W.Range);
                if (IsW1() && useW && Wmana)
                {
                    CastW(pos);
                }

                if (useR && Rmana && IsR1())
                {
                    CastR("RE", target);
                }

                if (useQ && Qmana && IsPassive(target))
                {
                    CastQ(target);
                }

                if (Emana && useE)
                {
                    E.Cast(target);
                }
            }

        }

        private void NewComboLogic()
        {
            bool useQ = RootM["combo"]["useQ"].As<MenuBool>().Enabled;
            bool useW = RootM["combo"]["useW"].As<MenuBool>().Enabled;
            bool useE = RootM["combo"]["useE"].As<MenuBool>().Enabled;
            bool useR = RootM["combo"]["useR"].As<MenuBool>().Enabled;
            bool Qmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Wmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.W).Cost;
            bool Emana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.E).Cost;
            bool Rmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.R).Cost;

            if (RootM["combo"]["combologics"]["rslogic"].As<MenuList>().Value == 0)
            {
                if (MyHero.Distance(target) < Q.Range)
                {
                    if (useQ && Qmana)
                    {
                        CastQ(target);
                    }

                    if (useE && Emana && !Q.Ready)
                    {
                        E.Cast(target);
                    }

                    if (useR && Rmana)
                    {
                        CastR("RQ", target);
                    }

                    if (useW && Wmana && IsW1() && !R.Ready)
                    {
                        CastW(target.ServerPosition);
                    }
                }
                else if (MyHero.Distance(target) < E.Range)
                {
                    if (useE && Emana)
                    {
                        E.Cast(target);
                    }

                    if (useQ && Qmana && !E.Ready)
                    {
                        CastQ(target);
                    }

                    if (useR && Rmana && !Q.Ready)
                    {
                        CastR("RQ", target);
                    }

                    if (useW && Wmana && IsW1() && !R.Ready)
                    {
                        CastW(target.ServerPosition);
                    }

                }
            }
            else if (RootM["combo"]["combologics"]["rslogic"].As<MenuList>().Value == 1)
            {
                if (MyHero.Distance(target) < Q.Range)
                {
                    if (useQ && Qmana)
                    {
                        CastQ(target);
                    }

                    if (useE && Emana && !Q.Ready)
                    {
                        E.Cast(target);
                    }

                    if (useR && Rmana)
                    {
                        CastR("RE", target);
                    }

                    if (useW && Wmana && IsW1() && !R.Ready)
                    {
                        CastW(target.ServerPosition);
                    }
                }
                else if (MyHero.Distance(target) < E.Range)
                {
                    if (useE && Emana)
                    {
                        E.Cast(target);
                    }

                    if (useQ && Qmana && IsPassive(target))
                    {
                        CastQ(target);
                    }

                    if (useR && Rmana)
                    {
                        CastR("RE", target);
                    }

                    if (useW && Wmana && IsW1() && !R.Ready)
                    {
                        CastW(target.ServerPosition);
                    }

                }
            }
            else
            {
                if (MyHero.Distance(target) < W.Range)
                {
                    if (useQ && Qmana)
                    {
                        CastQ(target);
                    }

                    if (useE && Emana && !Q.Ready)
                    {
                        E.Cast(target);
                    }

                    if (useR && Rmana)
                    {
                        CastR("RW", target);
                    }

                    if (useW && Wmana && IsW1() && !R.Ready)
                    {
                        CastW(target.ServerPosition);
                    }
                }

            }


        }

        private void ManualCombo()
        {
            bool useQ = RootM["combo"]["useQ"].As<MenuBool>().Enabled;
            bool useW = RootM["combo"]["useW"].As<MenuBool>().Enabled;
            bool useE = RootM["combo"]["useE"].As<MenuBool>().Enabled;
            bool useR = RootM["combo"]["useR"].As<MenuBool>().Enabled;
            bool Qmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Wmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.W).Cost;
            bool Emana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.E).Cost;
            bool Rmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.R).Cost;
           

            switch (RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value)
            {
                case 0:
                    
                    if (useQ && Qmana)
                    {
                        CastQ(target);
                    }

                    if (Wmana && useW)
                    {
                        CastW(W.GetPrediction(target).CastPosition);
                    }

                    if (useE && Emana)
                    {
                        E.Cast(target);
                    }


                    if (useR && Rmana && IsR1())
                    {
                         
                            CastR("RW", target);
                        
                    }
                    

                    break;
                case 1:
                    if (useQ && Qmana)
                    {
                        CastQ(target);
                    }

                    if (useR && Rmana && IsPassive(target))
                    {
                        CastR("RQ", target);
                    }

                    if (useE && Emana)
                    {
                        E.Cast(target);
                    }

                    if (useW && Wmana)
                    {
                        CastW(W.GetPrediction(target).CastPosition);
                    }

                    break;
                case 2:

                    if (useE && Emana)
                    {
                        E.Cast(target);
                    }

                    if (Qmana && useQ && IsPassive(target))
                    {
                        CastQ(target);
                    }

                    if (Wmana && useW && IsW1())
                    {
                        CastW(W.GetPrediction(target).CastPosition);
                    }

                    if (IsR1() && useR && Rmana)
                    {
                        CastR("RW", target);
                    }

                    break;
                case 3:
                    if (useE && Emana)
                    {
                        E.Cast(target);
                    }

                    if (Wmana && useW && IsW1())
                    {
                        CastW(W.GetPrediction(target).CastPosition);
                    }

                    if (useQ && Qmana && IsPassive(target))
                    {
                        CastQ(target);
                    }

                    if (useR && Rmana)
                    {
                        CastR("RQ", target);
                    }

                    break;
                case 4:
                    if (Wmana && useW && IsW1())
                    {
                        CastW(W.GetPrediction(target).CastPosition);
                    }

                    if (Rmana && useR)
                    {
                        CastR("RW", target);
                    }

                    if (useQ && Qmana && IsPassive(target))
                    {
                        CastQ(target);
                    }

                    if (useE && Emana)
                    {
                        E.Cast(target);
                    }

                    break;
                case 5:
                    if (Wmana && useW && IsW1())
                    {
                        CastW(W.GetPrediction(target).CastPosition);
                    }

                    if (useQ && Qmana && IsPassive(target))
                    {
                        CastQ(target);
                    }

                    if (Rmana && useR && IsR1())
                    {
                        CastR("RQ", target);
                    }

                    if (useE && Emana)
                    {
                        E.Cast(target);
                    }

                    break;
                case 6:
                    if (useQ && Qmana)
                    {
                        CastQ(target);
                    }

                    if (useR && Rmana && IsR1())
                    {
                        CastR("RQ", target);
                    }

                    if (useW && Rmana && IsW1())
                    {
                        CastW(W.GetPrediction(target).CastPosition);
                    }

                    if (useE && Emana)
                    {
                        DelayAction.Queue(450, () => E.Cast(target));
                    }

                    break;
                case 7:
                    if (!delaycheck)
                    {
                        if (useQ && Qmana)
                        {
                            CastQ(target);
                        }

                        if (Wmana && useW && IsW1())
                        {
                            CastW(W.GetPrediction(target).CastPosition);
                        }

                        if (Emana && useE)
                        {
                            E.Cast(target);
                        }

                        if (useR && Rmana && IsR1())
                        {
                            R.Cast();
                            delaycheck = true;
                            DelayAction.Queue(RootM["combo"]["combologics"]["delay"].As<MenuSliderBool>().Value, () =>
                                {
                                    this.CastE(target);
                                    delaycheck = false;
                                }
                            );
                        }
                    }

                    break;
            }
        }
    }
}
