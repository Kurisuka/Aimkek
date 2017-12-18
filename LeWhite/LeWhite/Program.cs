
using System;


namespace LeWhite

{
    using Aimtec;
    using Aimtec.SDK.Events;
    class Program
    {
        static void Main(string[] args)
        {
            GameEvents.GameStart += OnLoad;
        }
        private static void OnLoad()
        {
            if (ObjectManager.GetLocalPlayer().ChampionName == "Leblanc")
            {
                Console.WriteLine("LeWhite (Classis Misdirection Revived) - Loaded");
                var LB = new LB();

            }

        }
    }
}