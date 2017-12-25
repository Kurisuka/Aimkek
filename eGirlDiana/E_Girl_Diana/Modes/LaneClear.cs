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
            return GameObjects.EnemyMinions.Where(m => m.IsValidTarget(range)).ToList();
        }
        public void DoLaneClear()
        {
            bool Qmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Wmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.W).Cost;
            foreach (var minion in GetEnemyLaneMinionsTargetsInRange(Q.Range))
            {
                if (minion != null && (RootM["laneclear"]["useq"].As<MenuBool>().Enabled && Q.Ready && Qmana ))
                {
                    if (!minion.IsValidTarget(Q.Range) || minion.UnitSkinName.Contains("Plant"))
                    {
                        return;
                    }
                    else if (GameObjects.EnemyMinions.Count(t => t.IsValidTarget(Q.Range, false, false, Player.ServerPosition)) >= RootM["laneclear"]["mintoq"].Value) 
                            { Q.Cast(minion); }

                    
                }
                    if (RootM["laneclear"]["usew"].As<MenuBool>().Enabled && W.Ready && Wmana)
                    {
                        if (!minion.IsValidTarget(W.Range) || minion.UnitSkinName.Contains("Plant"))
                        {
                        return;
                        }
                        else if (GameObjects.EnemyMinions.Count(t => t.IsValidTarget(W.Range, false, false, Player.ServerPosition)) >= RootM["laneclear"]["mintow"].As<MenuSliderBool>().Value)
                        {
                            W.Cast();
                        }
                    }


            }
        }
    }
}
