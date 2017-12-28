using System.Collections.Generic;
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;
using Aimtec.SDK.Orbwalking;

namespace LeWhite
{
    internal partial class LB

    {
        public static List<Obj_AI_Minion> GetGenericJungleMinionsTargets()
        {
            return GetGenericJungleMinionsTargetsInRange(float.MaxValue);
        }

        public static List<Obj_AI_Minion> GetGenericJungleMinionsTargetsInRange(float range)
        {
            return GameObjectos.Jungle.Where(m => !GameObjectos.JungleSmall.Contains(m) && m.IsValidTarget(range))
            .ToList();
        }

        public void DoJungleClear()
        {
            bool Qmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Wmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.W).Cost;
            bool Emana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.E).Cost;
            bool Rmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.R).Cost;

            foreach (var jungleTarget in GameObjectos.Jungle.Where(m => m.IsValidTarget(Q.Range)).ToList())
            {
                if (!jungleTarget.IsValidTarget() || !jungleTarget.IsValidSpellTarget()) return;
                
                
                if (RootM["farm"]["jungleclear"]["useQ"].Enabled && Q.Ready && Qmana)
                {

                        if (jungleTarget.IsValidTarget(E.Range) && jungleTarget != null)
                        {
                            Q.Cast(jungleTarget);
                        }

                }
                if (RootM["farm"]["jungleclear"]["useW"].Enabled && W.Ready && Wmana)
                {



                        if (jungleTarget.IsValidTarget(W.Range) && jungleTarget != null)
                        {
                            W.Cast(jungleTarget);

                        }


                }
                if (RootM["farm"]["jungleclear"]["useE"].Enabled && E.Ready && Emana)
                {

                    if (jungleTarget.IsValidTarget(E.Range) && jungleTarget != null)
                    {
                        E.Cast(jungleTarget);
                    }

                }

                /*if (RootM["jungleclear"]["user"].Enabled && R.Ready && Rmana && jungleTarget != null)
                {
                    if (RootM["jungleclear"]["junglemarked"].Enabled && jungleTarget.IsValidTarget(R.Range) && jungleTarget != null)
                    { R.Cast(jungleTarget); }
                    
                   
                }*/

            }
        }
    }
}
