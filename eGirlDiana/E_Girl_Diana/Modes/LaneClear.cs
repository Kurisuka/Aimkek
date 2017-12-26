using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;


namespace E_Girl_Diana
{
    internal partial class egrilldiana
    {
        public static List<Obj_AI_Minion> GetEnemyLaneMinionsTargets()
        {
            return GetEnemyLaneMinionsTargetsInRange(float.MaxValue);
        }

        public static List<Obj_AI_Minion> GetEnemyLaneMinionsTargetsInRange(float range)
        {
            return GameObjects.EnemyMinions.Where(m =>
                    m.IsValidTarget(range) && m.UnitSkinName.Contains("Minion") && !m.UnitSkinName.Contains("Odin"))
                .ToList();
        }

        public void DoLaneClear()
        {
            bool Qmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Wmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.W).Cost;

            if (RootM["laneclear"]["useq"].As<MenuBool>().Enabled && Q.Ready && Qmana)
            {
                foreach (var minion in GetEnemyLaneMinionsTargetsInRange(Q.Range))
                {


                    if (minion.IsValidTarget(Q.Range) && minion != null)
                    {
                        if (GameObjects.EnemyMinions.Count(t =>
                                t.IsValidTarget(190, false, false, minion.ServerPosition)) >=
                            RootM["laneclear"]["mintoq"].Value)
                        {
                            Q.Cast(minion);
                        }
                    }
                }



            }
            if (RootM["laneclear"]["usew"].As<MenuBool>().Enabled && W.Ready && Wmana)
            {
                foreach (var minion in GetEnemyLaneMinionsTargetsInRange(Q.Range))
                {


                    if (minion.IsValidTarget(Q.Range) && minion != null)
                    {
                        if (GameObjects.EnemyMinions.Count(t =>
                                t.IsValidTarget(W.Range, false, false, Player.ServerPosition)) >=
                            RootM["laneclear"]["mintow"].Value)
                        {
                            W.Cast();
                        }
                    }
                }
            }


        }
    }
}