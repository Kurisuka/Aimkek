using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Prediction.Skillshots;
using Spell = Aimtec.SDK.Spell;


namespace E_Girl_Diana
{
    internal partial class egrilldiana
    {
        public static Spell Q, W, E, R, Flash;
        public bool IsFlash;

        public bool IsMarked(Obj_AI_Base unit)
        {
            return unit.HasBuff("dianamoonlight");
        }

        public bool IsPassive()
        {
            return Player.HasBuff("dianaarcready");
        }

        public void LoadSpells()
        {
            Q = new Spell(SpellSlot.Q, 900f);
            Q.SetSkillshot(0.25f, 75f, 2000f, false, SkillshotType.Line, false, HitChance.None);
            W = new Spell(SpellSlot.W, 200f);
            E = new Spell(SpellSlot.E, 420f);
            R = new Spell(SpellSlot.R, 825f);

            if (Player.SpellBook.GetSpell(SpellSlot.Summoner1).SpellData.Name.ToLower() == "summonerflash")
            {
                Flash = new Spell(SpellSlot.Summoner1, 425);
                IsFlash = true;
            }
            else if (Player.SpellBook.GetSpell(SpellSlot.Summoner2).SpellData.Name.ToLower() == "summonerflash")
            {
                Flash = new Spell(SpellSlot.Summoner2, 425);
                IsFlash = true;
            }


        }
    }
}
