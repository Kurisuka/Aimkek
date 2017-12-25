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
            foreach (var jungleTarget in GameObjectos.Jungle.Where(m => m.IsValidTarget(Q.Range)).ToList())
            {
                if (!jungleTarget.IsValidTarget() || !jungleTarget.IsValidSpellTarget()) return;
                
                if (RootM["jungleclear"]["useq"].Enabled && Q.Ready && Qmana)
                {
                    foreach (var minion in GetGenericJungleMinionsTargetsInRange(Q.Range))
                    {

                        if (minion.IsValidTarget(R.Range) && minion != null)
                        {
                            Q.Cast();
                        }
                    }
                }
                if (RootM["jungleclear"]["usew"].Enabled && W.Ready && Wmana)
                {
                    foreach (var minion in GetGenericJungleMinionsTargetsInRange(W.Range))
                    {


                        if (minion.IsValidTarget(W.Range) && minion != null)
                        {
                            W.Cast();

                        }
                    }

                }
            }
        }
    }
}
