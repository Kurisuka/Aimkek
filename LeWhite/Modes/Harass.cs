
using System;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util;

namespace LeWhite
{
    internal partial class LB
    {
        public void DoHarass()
        {
            int harassV = RootM["harass"]["harasslogic"].As<MenuList>().Value;
            bool useQ = RootM["harass"]["useQ"].As<MenuBool>().Enabled;
            bool useE = RootM["harass"]["useW"].As<MenuBool>().Enabled;
            bool useW = RootM["harass"]["useE"].As<MenuBool>().Enabled;
            bool useR = RootM["harass"]["useR"].As<MenuBool>().Enabled;
            bool waitP = RootM["harass"]["wait"].As<MenuBool>().Enabled;
            if (MyHero.ManaPercent() < (RootM["harass"]["Mana"].As<MenuSliderBool>().Value) || target == null || !target.IsValid)
            {
                return;
            }
            switch (harassV)
            {
                case 0:
                    if (useQ && useW)
                    {
                        if (IsW1())
                        {
                            CastW(target.ServerPosition);
                        }
                        else if (IsW2())
                        {
                            CastW2();
                        }
                        if (waitP)
                        {
                            if (!W.Ready && IsPassive(target))
                            {
                                CastQ(target);
                            }
                        }
                        else if (!W.Ready)
                        {
                            CastQ(target);
                        }
                    }
                    break;
                case 1:
                    if (useQ && useR)
                    {
                        CastQ(target);
                        if (waitP)
                        {
                            if (IsPassive(target))
                            {
                                CastR("RQ", target);
                            }
                        }
                        else
                        {
                            CastR("RQ", target);
                        }
                    }
                    break;
                case 2:
                    if (useR && useW)
                    {
                        if (IsW1())
                        {
                            CastW(target.ServerPosition);
                        }
                        else if (IsW2())
                        {
                            CastW2();
                        }
                        if (waitP)
                        {
                            if (IsPassive(target))
                            {
                                DelayAction.Queue(300, () => CastR("RW", target));
                            }
                        }
                        else
                        {
                            DelayAction.Queue(300, () => CastR("RW", target));
                        }
                    }
                    break;
                case 3:
                    if (useE && useQ)
                    {
                        CastQ(target);
                        if (waitP)
                        {
                            if (IsPassive(target))
                            {
                                CastE(target);
                            }

                        }
                        else
                        {
                            CastE(target);
                        }
                    }
                    break;
                case 4:
                    if (useQ && useR && useE)
                    {
                        CastQ(target);
                        if (waitP)
                        {
                            if (IsPassive(target))
                            {
                                CastR("RQ", target);
                            }
                        }
                        else
                        {
                            CastR("RQ", target);
                        }
                        if (!Q.Ready && !R.Ready)
                        {
                            CastE(target);
                        }
                    }
                    break;
            }
        }
    }
}
