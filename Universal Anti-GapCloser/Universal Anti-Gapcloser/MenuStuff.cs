using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universal_Anti_Gapcloser;
namespace Universal_Anti_Gapcloser
{
    internal partial class MenuStuff
    {

        public static Menu MMenu, playerSpells, enemySpells, qPlayerSet, wPlayerSet, ePlayerSet, rPlayerSet;
        public static void Initialize()
        {
            MMenu = new Menu("rootmenu", "Universal Anti-GapCloser", true);
            playerSpells = new Menu("urspells", "Player's Settings [Spells]");
            qPlayerSet = new Menu("qsettss", "Q Settings");
            {
                qPlayerSet.Add(new MenuBool("qUsage", "Use Q on Gapclose", false));
                qPlayerSet.Add(new MenuList("chooseplzq", "Choose How to Anti-Gapclose", new[] { "Use on Enemy", "Use to Flee", "Self Cast", "Is Skillshot" }, 0));
                qPlayerSet.Add(new MenuSlider("qrange", "Spell Range", 0, 0, 2000));
                qPlayerSet.Add(new MenuSeperator("uhhh", "Check ranges on Wiki, or use Enable Drawings to Set it up"));
                qPlayerSet.Add(new MenuBool("debug", "Range Drawings to Set Up", false));
            };
            playerSpells.Add(qPlayerSet);
            wPlayerSet = new Menu("wsettss", "W Settings");
            {
                wPlayerSet.Add(new MenuBool("wUsage", "Use W on Gapclose", false));
                wPlayerSet.Add(new MenuList("chooseplzw", "Choose How to Anti-Gapclose", new[] { "Use on Enemy", "Use to Flee", "Self Cast", "Is Skillshot" }, 0));
                wPlayerSet.Add(new MenuSlider("wrange", "Spell Range", 0, 0, 2000));
                wPlayerSet.Add(new MenuSeperator("uhhh", "Check ranges on Wiki, or  Enable Drawings to Set it up"));
                wPlayerSet.Add(new MenuBool("debug", "Range Drawings to Set Up", false));
            }
            playerSpells.Add(wPlayerSet);
            ePlayerSet = new Menu("esettss", "E Settings");
            {
                ePlayerSet.Add(new MenuBool("eUsage", "Use E on Gapclose", false));
                ePlayerSet.Add(new MenuList("chooseplze", "Choose How to Anti-Gapclose", new[] { "Use on Enemy", "Use to Flee", "Self Cast", "Is Skillshot" }, 0));
                ePlayerSet.Add(new MenuSlider("erange", "Spell Range", 0, 0, 2000));
                ePlayerSet.Add(new MenuSeperator("uhhh", "Check ranges on Wiki, or use Enable Drawings to Set it up"));
                ePlayerSet.Add(new MenuBool("debug", "Range Drawings to Set Up", false));
            }

            playerSpells.Add(ePlayerSet);

            rPlayerSet = new Menu("rsettss", "R Settings");
            {
                rPlayerSet.Add(new MenuBool("rUsage", "Use R on Gapclose", false));
                rPlayerSet.Add(new MenuList("chooseplzr", "Choose How to Anti-Gapclose", new[] { "Use on Enemy", "Use to Flee", "Self Cast", "Is Skillshot" }, 0));
                rPlayerSet.Add(new MenuSlider("rrange", "Spell Range", 0, 0, 2000));
                rPlayerSet.Add(new MenuSeperator("uhhh", "Check ranges on Wiki, or Enable Drawings to Set it up"));
                rPlayerSet.Add(new MenuBool("debug", "Range Drawings to Set Up", false));
            }
            playerSpells.Add(rPlayerSet);
            playerSpells.Add(new MenuBool("onlyifcombo", "Only if Combo key is Pressed"));           
            MMenu.Add(playerSpells);
            Gapcloser.Attach(MMenu, "Enemy Settings [Gapclose Spells]");
            MMenu.Attach();
        }
    }
    
}
