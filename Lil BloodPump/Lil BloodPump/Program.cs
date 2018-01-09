
namespace Lil_BloodPump

{
    using Aimtec;
    using Aimtec.SDK.Events;
    using Aimtec.SDK.Menu;
    using static Lil_BloodPump.Vladimir;


    internal class Program
    {
        
        static void Main(string[] args)
        {
            GameEvents.GameStart += GameEvents_GameStart;
            //Gapcloser.OnGapcloser += Gapclose;
            /*GameEvents.GameStart += GameEventsOnGameStart;
            Game.OnUpdate += UpdateSkin;
            GameObject.OnRevive += GameObjectOnOnRevive;*/
            
        }
        private static void GameEvents_GameStart()
        {
            if (ObjectManager.GetLocalPlayer().ChampionName != "Vladimir")
                return;

            var Vladimir = new Vladimir();
        }
        /*public static void GameEventsOnGameStart()
        {


            Self["mySkin"].OnValueChanged += (sender, args) => Game.OnUpdate += UpdateSkin;

        }
        public static void UpdateSkin()
        {

            var mySkin = Misc["mySkin"].Value;
            Player.SetSkinId(mySkin);
            Game.OnUpdate -= UpdateSkin;
        }
        private static void GameObjectOnOnRevive(GameObject sender)
        {
            if (sender is Obj_AI_Hero hero)
            {
                if (hero.IsMe)
                {
                    var mySkin = Self["mySkin"].Value;
                    Player.SetSkinId(mySkin);
                }
            }
        }*/

    }
}