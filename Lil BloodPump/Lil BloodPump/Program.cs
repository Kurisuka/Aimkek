
namespace Lil_BloodPump

{
    using Aimtec;
    using Aimtec.SDK.Events;


    class Program
    {
        static void Main(string[] args)
        {
            GameEvents.GameStart += GameEvents_GameStart;
            //Gapcloser.OnGapcloser += Gapclose;
        }

        private static void GameEvents_GameStart()
        {
            if (ObjectManager.GetLocalPlayer().ChampionName != "Vladimir")
                return;

            var Vladimir = new Vladimir();
        }
    }
}