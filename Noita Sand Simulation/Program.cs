using Raylib_cs;
using System;
using System.Numerics;

namespace Noita_Sand_Simulation {
   class Program {
      static void Main(string[] args) {
         const int WIDTH = 800;
         const int HEIGHT = 600;
         const int SIMULATION_FPS = 20;
         const int EDIT_FPS = 144;
         const int TILE_SIZE = 8;
         
         App app = new App(WIDTH / TILE_SIZE, HEIGHT / TILE_SIZE);
         app.CreateSurroundingWalls();

         Raylib.InitWindow(WIDTH, HEIGHT, "Noita Sand Simulation");

         Raylib.SetTargetFPS(app.isEditMode ? EDIT_FPS : SIMULATION_FPS);

         Camera2D camera = new Camera2D() {
            Target = Vector2.Zero,
            Offset = Vector2.Zero,
            Rotation = 0,
            Zoom = TILE_SIZE
         };
         
         while (!Raylib.WindowShouldClose()) {
            Raylib.BeginDrawing();
            Raylib.BeginMode2D(camera);

            Raylib.ClearBackground(Color.BLACK);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)) {
               app.isEditMode = !app.isEditMode;

               Console.WriteLine($"Edit mode: {app.isEditMode}");
               Raylib.SetTargetFPS(app.isEditMode ? EDIT_FPS : SIMULATION_FPS);
            }

            if (app.isEditMode) {
               if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON)) {
                  Vector2 mousePosition = Raylib.GetMousePosition();
                  int x = (int)mousePosition.X / TILE_SIZE;
                  int y = (int)mousePosition.Y / TILE_SIZE;

                  if (app.IsInBounds(x, y)) {
                     app.grid[y][x] = Tile.Sand;
                  }
               } else if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON)) {
                  Vector2 mousePosition = Raylib.GetMousePosition();
                  int x = (int)mousePosition.X / TILE_SIZE;
                  int y = (int)mousePosition.Y / TILE_SIZE;

                  if (app.IsInBounds(x, y)) {
                     if (app.grid[y][x] == Tile.Sand) {
                        app.grid[y][x] = Tile.Empty;
                     }
                  }
               }
            } else {
               app.StepSimulation();
            }
            
            app.Draw();
            Raylib.EndMode2D();

            Raylib.DrawFPS(10, 0);
            if (app.isEditMode) {
               Raylib.DrawText("Edit Mode", 10, 30, 20, Color.RED);
               Raylib.DrawText("LMB to place sand", 10, 50, 20, Color.LIME);
               Raylib.DrawText("RMB to remove sand", 10, 70, 20, Color.LIME);
               Raylib.DrawText("Space to run simulation", 10, 90, 20, Color.LIME);
            } else {
               Raylib.DrawText("Simulation Mode", 10, 30, 20, Color.GREEN);
               Raylib.DrawText("Space to enter edit mode", 10, 50, 20, Color.LIME);
            }

            Raylib.EndDrawing();
         }
      }
   }
}