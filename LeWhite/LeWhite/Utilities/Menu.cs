using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.Util;
using Aimtec.SDK.Util.Cache;

namespace LeWhite
{
    internal partial class LB
    {
        #region Static Operations
        public static Menu RootM, Combo, ComboBack, ComboLogics, ComboBlacklist, Harass, AutoHarass, Farm, LastHit, LaneClear, JungleClear, OneShot,
            KillSteal, Escape, Draw, WShadow, RShadow, DrawOptions, Misc, TurretDive, SkinHax, AutoLevel, AntiAfk, Key;
        #endregion

        public void LoadMenu()
        {
            // Inıtialize the first menu
            RootM = new Menu("leblanc", "LeWhite - Classic Misdirection Revived", true);
            Orbwalker.Implementation.Attach(RootM);
            {
                #region  Combo Menu
                {
                    Combo = new Menu("combo", "Combo Settings");
                    {
                        /* ComboBack = new Menu("turnback", "Turn Back settings");
                         {
                             ComboBack.Add(new MenuList("turnbacklogic", "Turn Back Logic", new[] { "Led Script Decide", "Depends On Settings Below", "Always", "Never" }, 0));
                             ComboBack.Add(new MenuSliderBool("wbackhp", "Turn back depends on hp", false, 30, 10, 99));
                             ComboBack.Add(new MenuBool("wbackcp", "Turn back depends on enemy", true));
                             ComboBack.Add(new MenuList("enemy", "Enemy Number", new[] { "2 enemy", "3 enemy", "4 enemy", "5 enemy" }, 0));
                         }
                         Combo.Add(ComboBack);*/

                        Combo.Add(new MenuBool("useQ", "Use Q in Combo", true));
                        Combo.Add(new MenuBool("useW", "Use W in Combo", false));
                        Combo.Add(new MenuBool("useE", "Use E in Combo", true));
                        Combo.Add(new MenuBool("useR", "Use R in Combo", true));
                        if (IsIgnite)
                        {
                            Combo.Add(new MenuBool("useI", "Use Ignite in Combo", true));
                        }
                        //  Combo.Add(new MenuBool("wait", "Always wait for the passive", true));
                        {
                            ComboLogics = new Menu("combologics", "Combo Logic Setings");
                            {
                                ComboLogics.Add(new MenuList("rlogic", "Select R Logic", new[] { "Manual Settings", "Stick to combo logic" }, 0));
                                ComboLogics.Add(new MenuSeperator("info2", "Manual Settings activates Manual Settings"));
                                ComboLogics.Add(new MenuSeperator("info3", "Stick to combo activates Combo Logic options"));
                                ComboLogics.Add(new MenuList("select", "Select Combo Logic", new[] { "Dynamic combo", "Manual Combo" }, 0));
                                ComboLogics.Add(new MenuSeperator("b1", "Manual Combo Settings"));
                                ComboLogics.Add(new MenuList("mCombo", "Select Combo Logic", new[] { "Q>E>W>R", "Q>R>E>W", "E>Q>W>R", "E>W>Q>R", "W>R>Q>E", "W>Q>R>E", "Q>R>W>E", "Double Stun" }, 0));
                                ComboLogics.Add(new MenuSliderBool("delay", "Delay For Double Stun", true, 1650, 0, 3000));
                                ComboLogics.Add(new MenuSeperator("info", "Manual R Setings"));
                                ComboLogics.Add(new MenuList("rslogic", "Select Your Ulti", new[] { "Q", "E","W" }, 0));
                            }
                            Combo.Add(ComboLogics);
                        }

                        Combo.Add(new MenuSliderBool("Mana", "Mana Manager %", true, 15, 10, 99));

                        ComboBlacklist = new Menu("blacklist", "Blacklist For Combo");
                        foreach (var tar in GameObjects.EnemyHeroes)
                        {
                            ComboBlacklist.Add(new MenuBool("use" + tar.ChampionName.ToLower(), "Don't use on :" + tar.ChampionName, false));
                        }
                        Combo.Add(ComboBlacklist);

                    }

                }
                RootM.Add(Combo);
                #endregion

                #region Harass Menu
                {
                    Harass = new Menu("harass", "Harass Settings");
                    {


                        Harass.Add(new MenuList("harasslogic", "Harass Logic", new[] { "[ W-Q ]", "[ Q-R ]", "[ W-R ]", "[ Q-E ]", "[ Q-R-E ]" }, 0));
                        Harass.Add(new MenuBool("useQ", "Use Q in Harass", true));
                        Harass.Add(new MenuBool("useW", "Use W in Harass", true));
                        Harass.Add(new MenuBool("useE", "Use E in Harass", true));
                        Harass.Add(new MenuBool("useR", "Use R in Harass", true));
                        Harass.Add(new MenuBool("wait", "Always wait for the passive", true));
                        Harass.Add(new MenuSliderBool("Mana", "Mana Manager %", true, 15, 10, 99));
                    }
                }
                RootM.Add(Harass);
                #endregion

                #region Farm Menu
                {
                    Farm = new Menu("farm", "Farm Settings");
                    {
                        LaneClear = new Menu("laneclear", "LaneClear Settings");
                        {
                            LaneClear.Add(new MenuBool("useQ", "Use Q in LaneClear", true));
                            LaneClear.Add(new MenuSliderBool("qcount", "Minimum minion to Q", true, 2, 1, 10));
                            LaneClear.Add(new MenuBool("useW", "Use W in LaneClear", true));
                            LaneClear.Add(new MenuSliderBool("wcount", "Minimum minion to W", true, 2, 1, 10));
                            LaneClear.Add(new MenuBool("useE", "Use E in LaneClear", false));
                            LaneClear.Add(new MenuBool("useR", "Use R in LaneClear", false));
                            LaneClear.Add(new MenuSliderBool("rcount", "Minimum minion to R", true, 2, 1, 10));

                            LaneClear.Add(new MenuSeperator("blank1"));
                            LaneClear.Add(new MenuSeperator("energylane", "Mana Manager"));
                            LaneClear.Add(new MenuSliderBool("QMana", "Q Skill Mana Manager  %", true, 30, 10, 99));
                            LaneClear.Add(new MenuSliderBool("WMana", "W Skill Mana Manager  %", true, 30, 10, 99));
                            LaneClear.Add(new MenuSliderBool("Emana", "E Skill Mana Manager  %", true, 30, 10, 99));
                        }
                        Farm.Add(LaneClear);

                        /*JungleClear = new Menu("jungleclear", "JungleClear Settings");
                         {
                             JungleClear.Add(new MenuBool("useQ", "Use Q in JungleClear", true));
                             JungleClear.Add(new MenuBool("useW", "Use W in JungleClear", true));
                             JungleClear.Add(new MenuBool("useE", "Use E in JungleClear", true));
                             JungleClear.Add(new MenuSeperator("blank1"));
                             JungleClear.Add(new MenuSeperator("energylane", "Mana Manager"));
                             JungleClear.Add(new MenuSliderBool("QMana", "Q Skill Mana Manager  %", true, 30, 10, 99));
                             JungleClear.Add(new MenuSliderBool("WMana", "W Skill Mana Manager  %", true, 30, 10, 99));
                             JungleClear.Add(new MenuSliderBool("Emana", "E Skill Mana Manager  %", true, 30, 10, 99));
                         }
                         Farm.Add(JungleClear);*/

                        LastHit = new Menu("lasthit", "LastHit Settings");
                        {
                            LastHit.Add(new MenuBool("autolasthit", "Use Auto LastHit", false));
                            LastHit.Add(new MenuBool("useQ", "Use Q in LastHit", true));
                            LastHit.Add(new MenuBool("useW", "Use W in LastHit", true));
                            LastHit.Add(new MenuBool("useE", "Use E in LastHit", true));
                            LastHit.Add(new MenuSeperator("blank1"));
                            LastHit.Add(new MenuSeperator("energylane", "Mana Manager"));
                            LastHit.Add(new MenuSliderBool("QMana", "Q Skill Mana Manager  %", true, 30, 10, 99));
                            LastHit.Add(new MenuSliderBool("WMana", "W Skill Mana Manager  %", true, 30, 10, 99));
                            LastHit.Add(new MenuSliderBool("Emana", "E Skill Mana Manager  %", true, 30, 10, 99));
                        }
                        Farm.Add(LastHit);

                    }
                }
                RootM.Add(Farm);
                #endregion

                #region Escape Menu
                {
                    Escape = new Menu("escape", "Flee Settings");
                    Escape.Add(new MenuBool("useW", "Use W While Fleeing", true));
                    Escape.Add(new MenuBool("useR", "Use RW While Fleeing", false));
                }
                RootM.Add(Escape);
                #endregion

                #region Killsteal Menu
                {
                    KillSteal = new Menu("killsteal", "KillSteal Settings");
                    {

                    }
                    KillSteal.Add(new MenuBool("ks", "Use KillSteal", true));
                    KillSteal.Add(new MenuBool("useQ", "Use Q in Killsteal", true));
                    KillSteal.Add(new MenuBool("useW", "Use W in Killsteal", true));
                    KillSteal.Add(new MenuBool("useE", "Use E in Killsteal", true));
                    KillSteal.Add(new MenuBool("useR", "UseR in Killsteal", true));
                    if (IsIgnite)
                    {
                        KillSteal.Add(new MenuBool("useI", "Use Ignite to Killsteal", true));
                    }
                }

                #endregion
                RootM.Add(KillSteal);

                #region Misc Menu
                {
                       Misc = new Menu("misc", "Misc Settings");
                      

                           var Skinhax = new Menu("skinhack", "Skinhack Settings");
                           {
                        /*Skinhax.Add(new MenuBool("useskin", "Use Skin Hack", false));
                        Skinhax.Add(new MenuList("selectedskin", "Select Skin", new[] { "
                        
                        
                        
                        ", "Shockblade", "SKT T1", "PROJECT" }, 0));*/
                        Skinhax.Add(new MenuSeperator( "soon", "Soon"));
                           }
                           Misc.Add(Skinhax);

                       }
                }
                RootM.Add(Misc);
                #endregion


                #region Drawing Menu
                {
                    Draw = new Menu("draw", "Drawing Settings");
                    {
                        DrawOptions = new Menu("drawS", "Skill Drawing Settings");
                        DrawOptions.Add(new MenuBool("qdraw", "Draw Q Range", true));
                        DrawOptions.Add(new MenuBool("wdraw", "Draw W Range", false));
                        DrawOptions.Add(new MenuBool("edraw", "Draw E Range", false));
                        DrawOptions.Add(new MenuBool("rdraw", "Draw R Range", false));
                    }
                    Draw.Add(DrawOptions);

                    Draw.Add(new MenuBool("combomode", "Draw Combo Mode", true));
                    // Draw.Add(new MenuBool("damage", "Draw Damage Indicator", false));
                    // Draw.Add(new MenuBool("targetcal", "Target Calculation", false));
                    Draw.Add(new MenuBool("disable", "Disable All Drawings", false));

                }
                RootM.Add(Draw);
                #endregion

                #region Key Menu
                {
                    Key = new Menu("keys", "Key Settings");
                    Key.Add(new MenuSeperator("combo1", "Combo Key Settings"));
                    Key.Add(new MenuKeyBind("combokey", "Combo Key", KeyCode.Space, KeybindType.Press));
                    Key.Add(new MenuKeyBind("combomode", "Combo Mode Key", KeyCode.G, KeybindType.Press));
                    Key.Add(new MenuKeyBind("onlye", "Only Use E Skill (only stun )", KeyCode.O, KeybindType.Press));
                    Key.Add(new MenuSeperator("harass1", "Harass Key Settings"));
                    Key.Add(new MenuKeyBind("harasskey", "Smart Harass Key", KeyCode.C, KeybindType.Press));
                    Key.Add(new MenuSeperator("lane1", "Clear Key Settings"));
                    Key.Add(new MenuKeyBind("lasthitkey", "LastHit Key", KeyCode.X, KeybindType.Press));
                    Key.Add(new MenuKeyBind("laneclearkey", "LaneClear Key", KeyCode.V, KeybindType.Press));
                    //Key.Add(new MenuKeyBind("jungleclearkey", "JungleClear Key", KeyCode.V, KeybindType.Press));
                    Key.Add(new MenuSeperator("other", "Other Key Settings"));
                    Key.Add(new MenuKeyBind("escape", "Escape Key", KeyCode.Y, KeybindType.Press));
                    //Key.Add(new MenuKeyBind("wallkey", "Wall Jump Key", KeyCode.Y, KeybindType.Press));

                }
                RootM.Add(Key);
                #endregion

                RootM.Add(new MenuSeperator("1", "LeWhite"));
                RootM.Add(new MenuSeperator("2", "Script Originally made by Krystra"));
                RootM.Add(new MenuSeperator("3", "Revived, with love, by Nekky"));
                RootM.Add(new MenuSeperator("4", "All credits to the original owner"));
                RootM.Attach();
            }
        }
 }
 

