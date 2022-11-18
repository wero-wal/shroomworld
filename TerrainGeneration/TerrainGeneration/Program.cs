namespace TerrainGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateMap_Test();
            //LoopCreateMap_Test(100, 25);
            //CreateMapWithCaves_Test();
            LoopCreateMapWithCaves_Test();
            //TestingSmoothness();
            Console.ReadKey();
        }
        static void CreateMap_Test(int width = 100, int height = 25, float? seed = null)
        {
            Map map = new Map(width, height, 0.3f, seed);
            map.GenerateSurfaceTerrain();
            map.Display();
        }
        static void LoopCreateMap_Test(int width = 100, int height = 25)
        {
            ConsoleKey input;
            do
            {
                Map map = new Map(width, height, 0.3f);
                map.Clear();
                map.GenerateSurfaceTerrain();
                map.Display();
                input = Console.ReadKey().Key;
            } while (input != ConsoleKey.Escape);
        }
        static void CreateMapWithCaves_Test(float? seed = null)
        {
            Map map = new Map(100, 25, 0.3f, seed);
            map.GenerateSurfaceTerrain();
            map.GenerateCaves();
            map.Display();
            Console.ReadKey();
        }
        static void LoopCreateMapWithCaves_Test(float? seed = null)
        {
            ConsoleKey input;
            do
            {
                Map map = new Map(100, 25, 0.3f, seed);
                map.GenerateSurfaceTerrain();
                map.GenerateCaves();
                map.Display();
                input = Console.ReadKey().Key;
                Console.ResetColor();
                Console.Clear();
            } while (input != ConsoleKey.X);
        }
        static void TestingSmoothness()
        {
            Map map = new Map(50, 25, 0.3f, 50);
            for (int smoothness = 0; smoothness < 5; smoothness++)
            {
                map.GenerateSurfaceTerrain();
                map.GenerateCaves(smoothness);
                map.Display();
                //Console.ReadKey();
            }
        }
    }
}
