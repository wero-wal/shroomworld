namespace TerrainGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateMap_Test();
            //LoopCreateMap_Test(100, 25);
            Console.ReadKey();
        }
        static void CreateMap_Test(int width = 100, int height = 25, float? seed = null)
        {
            Map map = new Map(width, height);
            map.GenerateSurfaceTerrain(seed);
            map.Display();
        }
        static void LoopCreateMap_Test(int width = 100, int height = 25)
        {
            Map map = new Map(width, height);
            ConsoleKey input;
            do
            {
                map.Clear();
                map.GenerateSurfaceTerrain();
                map.Display();
                input = Console.ReadKey().Key;
            } while (input != ConsoleKey.Escape);
        }
    }
}
