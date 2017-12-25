using System.Collections.Generic;
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;
using Aimtec.SDK.Orbwalking;

namespace E_Girl_Diana
{
    internal partial class egrilldiana
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
            bool Qmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Wmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.W).Cost;
            bool Rmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.R).Cost;
            foreach (var jungleTarget in GameObjectos.Jungle.Where(m => m.IsValidTarget(Q.Range)).ToList())
            {
                if (!jungleTarget.IsValidTarget() || !jungleTarget.IsValidSpellTarget()) return;
                
                
                if (RootM["jungleclear"]["useq"].Enabled && Q.Ready && Qmana)
                {

                        if (jungleTarget.IsValidTarget(R.Range) && jungleTarget != null)
                        {
                            Q.Cast(jungleTarget);
                        }

                }
                if (RootM["jungleclear"]["usew"].Enabled && W.Ready && Wmana)
                {



                        if (jungleTarget.IsValidTarget(W.Range) && jungleTarget != null)
                        {
                            W.Cast();

                        }


                }
                /*if (RootM["jungleclear"]["user"].Enabled && R.Ready && Rmana)
                {
                    if (RootM["jungclear"]["junglemarked"].Enabled && IsMarked(jungleTarget) && jungleTarget.IsValidTarget(R.Range) && jungleTarget != null)
                    { R.Cast(jungleTarget); }
                    else return;
                    if (jungleTarget.IsValidTarget(R.Range) && jungleTarget != null && !RootM["jungclear"]["junglemarked"].Enabled)
                    {
                        R.Cast(jungleTarget);
                    }
                }*/
                   
            }
        }
    }
}
