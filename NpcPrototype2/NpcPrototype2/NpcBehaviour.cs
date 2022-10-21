using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpcPrototype2
{
    internal class NpcBehaviour
    {
        public static void RegenerateHealth(Npc npc)
        {
            npc.IncreaseHealth(Settings.HealthRegenPerSecond / Settings.UpdatesPerSecond);
        }
        public static void Pathfind(Npc npc)
        {
            Console.WriteLine($"{npc.Name} found a path");
        }
        public static void Move(Npc npc)
        {
            npc.SetVelocity(new Random().Next(0, 100), new Random().Next(0, 100));
            npc.SetPosition();
            Console.WriteLine($"{npc.Name} moved");
        }
        public static void TakeDamage(Npc npc, int damageAmount)
        {
            npc.DecreaseHealth(damageAmount);
        }
    }
}
