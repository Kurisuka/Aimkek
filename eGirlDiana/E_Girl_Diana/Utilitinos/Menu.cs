using Aimtec;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.Util;
using Aimtec.SDK.Util.Cache;
using Newtonsoft.Json;

namespace E_Girl_Diana
{
    internal partial class egrilldiana
    {
        #region Static Operations
        public static Menu RootM, Combo, ComboBack, ComboLogics, BlacklistCombo, Harass, AutoHarass, Farm, LastHit, LaneClear, JungleClear, OneShot,
            KillSteal, Escape, Draw, WShadow, RShadow, DrawOptions, Misc, TurretDive, SkinHack, AutoLevel, AntiAfk, Key, Self, skins;
        #endregion
        
        public async void LoadMenuAsync()
        {
            RootM = new Menu("Diana", "E-Girl Diana", true);
            Orbwalker.Implementation.Attach(RootM);
            {
                #region combo
                {
                    //start of combo ting
                    Combo = new Menu("combo", "Combo Stuff");

                    Combo.Add(new MenuBool("useq", "Use Q in Combo", true));
                    Combo.Add(new MenuBool("usew", "Use W in Combo", true));
                    Combo.Add(new MenuBool("usee", "Use E in Combo", true));
                    Combo.Add(new MenuBool("user", "Use R in Combo", true));
                    Combo.Add(new MenuList("rcombo", "Different Combos", new[] { "Burst", "Use R only if marked", "Spam QWER like the flash" }, 0));
                    Combo.Add(new MenuSlider("mana", "Mana Manager in %", 50, 10, 99));
                    //Combo.Add(new MenuBool("emisc", "Use E in enemy range only", true));

                    /*BlacklistCombo = new Menu("blacklist", "Champion Blacklist");
                    foreach (var tar in GameObjects.EnemyHeroes) BlacklistCombo.Add(new MenuBool("Use on " + tar.ChampionName.ToLower(),
                            "Don't use on: " + tar.ChampionName, false));
                    Combo.Add(BlacklistCombo);*/

                    RootM.Add(Combo);

                    //end of combo ting
                }
                #endregion

                #region harass

                {
                    //harass ting
                    Harass = new Menu("harass", "Harass ting");
                    {
                        Harass.Add(new MenuBool("useq", "Use Q to Harass", true));
                        Harass.Add(new MenuBool("usew", "Use W to Harass", false));
                        Harass.Add(new MenuSliderBool("mana", "Mana Manager in %", true, 50, 10, 99));
                    }
                    RootM.Add(Harass);
                    //end of harass ting bruv ty
                    #endregion


                    //tired af lol laneclear and shit
                    LaneClear = new Menu("laneclear", "Lane Clearing");
                    {
                        LaneClear.Add(new MenuSeperator("qset", "Q Settings"));
                        LaneClear.Add(new MenuBool("useq", "Use Q to Clean a Lane", true));
                        LaneClear.Add(new MenuSliderBool("mintoq", "Minions to Q", true, 1, 1, 10));
                        LaneClear.Add(new MenuSliderBool("manaq", "% Mana to use Q", true, 50, 10, 99));
                        LaneClear.Add(new MenuSeperator("wset", "W Settings"));
                        LaneClear.Add(new MenuBool("usew", "Use W to Clean a Lane", false));
                        LaneClear.Add(new MenuSliderBool("mintow", "Minions to W", true, 1, 1, 10));
                        LaneClear.Add(new MenuSliderBool("manaw", "% Mana to use W", true, 50, 10, 99));
                    }
                    RootM.Add(LaneClear);
                    //end me

                    //I hate cleaning
                    JungleClear = new Menu("jungleclear", "Jungle Clearing");
                    {
                        JungleClear.Add(new MenuSeperator("qset", "Q Settings"));
                        JungleClear.Add(new MenuBool("useq", "Use Q to Clear Jungle", true));
                        JungleClear.Add(new MenuSliderBool("manaq", "% Mana to use Q", true, 50, 10, 99));
                        JungleClear.Add(new MenuSeperator("wset", "W Settings"));
                        JungleClear.Add(new MenuBool("usew", "Use W to Clear Jungle", true));
                        JungleClear.Add(new MenuSliderBool("manaw", "% Mana to use W", true, 50, 10, 99));
                        JungleClear.Add(new MenuSeperator("soonR", "Will add R settings later."));
                       /* JungleClear.Add(new MenuBool("user", "Use R to Clear Jungle", true));
                        JungleClear.Add(new MenuSliderBool("manar", "% Mana to use W", true, 50, 10, 99));
                        JungleClear.Add(new MenuBool("junglemarked", "Only if Marked", true));*/
                    }
                    RootM.Add(JungleClear);
                    //end of jungclear ting

                    #region Killsteal
                    {
                        KillSteal = new Menu("killsteal", "Killsteal");

                    }
                    KillSteal.Add(new MenuBool("ks", "Killsteal", true));
                    KillSteal.Add(new MenuBool("useq", "Use Q to Steal", true));
                    KillSteal.Add(new MenuBool("user", "Use R to Steal", false));
                    KillSteal.Add(new MenuBool("onlymarked", "Only if Marked", false));
                }

                RootM.Add(KillSteal);
                #endregion

                #region Misc Menu
                {
                    var skins = await GetSkins(Player.ChampionName);
                    //misc ting
                    Misc = new Menu("misc", "Miscellaneous");
                    {
                        Misc.Add(new MenuSeperator("skinting", "Choose your skin:"));
                        Self = new Menu("self", "Skins") {
                        new MenuList("mySkin", "Champion Skin", skins, 0)};
                        Self["mySkin"].OnValueChanged += (sender, args) => Game.OnUpdate += UpdateSkin;
                        Misc.Add(Self);
                        //Misc.Add(new MenuSeperator("soon", "SoonBIK"));
                    }
                    //end of that

                }
                RootM.Add(Misc);
                #endregion

                #region Drawing Menu
                {
                    Draw = new Menu("draw", "Drawings");
                    {
                        Draw.Add(new MenuBool("drawq", "Draw Q Range", false));
                        Draw.Add(new MenuBool("draww", "Draw W Range", false));
                        Draw.Add(new MenuBool("drawe", "Draw E Range", false));
                        Draw.Add(new MenuBool("drawr", "Draw R Range", false));
                        //Draw.Add(new MenuBool("drawmode", "Draw Combo Mode", true));
                        Draw.Add(new MenuBool("disabled", "Disable Drawings", false));

                    }
                    RootM.Add(Draw);
                }
                    #endregion

                    /*#region Key Menu
                    {
                        //keybinds and shit
                        Key = new Menu("binds", "Key Binds");
                        Key.Add(new MenuKeyBind("combomode", "Change Combo Mode", KeyCode.J, KeybindType.Press));
                        //end of that too

                    }
                    RootM.Add(Key);
                    #endregion*/
                    RootM.Attach();
                //}

            }
        }
    }
}
