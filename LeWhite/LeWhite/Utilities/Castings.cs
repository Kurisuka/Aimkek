

using Aimtec;
using Aimtec.SDK.Extensions;

namespace LeWhite
{
    internal partial class LB
    {
        public void CastQ(Obj_AI_Base unit)
        {
            if (!Q.Ready)
            {
                return;
            }
            if (MyHero.Distance(unit) < Q.Range)
            {
                Q.CastOnUnit(unit);
            }
        }

        public void CastW(Vector3 pos)
        {
            if (!W.Ready || MyHero.SpellBook.GetSpell(SpellSlot.W).Name.ToLower() != "leblancw")
            {
                return;
            }
            if (MyHero.Distance(pos) < W.Range)
            {
                W.Cast(pos);
            }
        }

        public void CastW2()
        {
            if (!W.Ready || MyHero.SpellBook.GetSpell(SpellSlot.W).Name.ToLower() != "leblancwreturn")
            {
                return;
            }
            W.Cast();
        }

        public void CastE(Obj_AI_Base unit)
        {
            if (!E.Ready)
            {
                return;
            }
            if (MyHero.Distance(unit) < E.Range)
            {
                E.Cast(unit);
            }
        }
        public void CastR(string mode, Obj_AI_Base unit, Vector3 Rpos = new Vector3())
        {

            if (!R.Ready)
            {
                return;
            }
            switch (mode)
            {
                case "D":
                    if (MyHero.SpellBook.GetSpell(SpellSlot.R).Name.ToLower() == "leblancrtoggle")
                    {
                        R.Cast();
                    }
                    else
                    {
                        R.Cast(Rpos);
                    }
                    break;
                case "RQ":
                    if (MyHero.SpellBook.GetSpell(SpellSlot.R).Name.ToLower() == "leblancrtoggle")
                    {
                        R.Cast();
                        this.CastQ(unit);
                    }
                    break;
                case "RW":
                    if (MyHero.SpellBook.GetSpell(SpellSlot.R).Name.ToLower() == "leblancrtoggle")
                    {
                        R.Cast();
                        this.CastW(unit.ServerPosition);
                    }
                    break;
                case "RE":
                    if (MyHero.SpellBook.GetSpell(SpellSlot.R).Name.ToLower() == "leblancrtoggle")
                    {
                        R.Cast();
                        this.CastE(unit);
                    }
                    break;
                case "Return":
                    if (MyHero.SpellBook.GetSpell(SpellSlot.R).Name.ToLower() == "leblancrtoggle")
                    {
                        R.Cast();
                        if (MyHero.SpellBook.GetSpell(SpellSlot.R).Name.ToLower() == "leblancrwreturn")
                        {
                            R.Cast();
                        }
                    }
                    break;
            }
        }


    }
}
