using Raylib_cs;

namespace Noita_Sand_Simulation {
   class Program {
      static void Main(string[] args) {
         Raylib.InitWindow(800, 600, "Noita Sand Simulation");
         Raylib.SetTargetFPS(144);

         while (!Raylib.WindowShouldClose()) {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);

            Raylib.DrawText("Noita Sand Simulation", 10, 10, 20, Color.WHITE);

            Raylib.EndDrawing();
         }
      }
   }
}