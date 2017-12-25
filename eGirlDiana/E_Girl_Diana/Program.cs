using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK.Events;


namespace E_Girl_Diana
{
    internal partial class egrilldiana
    {
        static void Main(string[] args)
        {
            GameEvents.GameStart += OnLoad;
        }
        private static void OnLoad()
        {
            if (ObjectManager.GetLocalPlayer().ChampionName == "Diana") Console.WriteLine("e-Girl Diana - Loaded");
            var egrilldiana = new egrilldiana();
        }
    }
}
