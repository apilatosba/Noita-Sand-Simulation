using Raylib_cs;
using System;

namespace Noita_Sand_Simulation {
   public class App {
      public Tile[][] grid;
      public bool isEditMode;

      public App(int width, int height) {
         grid = new Tile[height][];

         for (int y = 0; y < grid.Length; y++) {
            grid[y] = new Tile[width];
         }

         isEditMode = true;
      }
      
      public void Draw() {
         for (int y = 0; y < grid.Length; y++) {
            for (int x = 0; x < grid[0].Length; x++) {
               Tile tile = grid[y][x];
               Raylib.DrawPixel(x, y, GetTileColor(tile));
            }
         }
      }

      // Go from bottom to top
      public void StepSimulation() {
         for (int y = grid.Length - 1; y >= 0; y--) {
            for (int x = grid[0].Length - 1; x >= 0; x--) {
               Tile tile = grid[y][x];

               if (tile == Tile.Sand) {
                  if (grid[y + 1][x] == Tile.Empty) {
                     grid[y][x] = Tile.Empty;
                     grid[y + 1][x] = Tile.Sand;
                  } else if (grid[y + 1][x - 1] == Tile.Empty) {
                     grid[y][x] = Tile.Empty;
                     grid[y + 1][x - 1] = Tile.Sand;
                  } else if (grid[y + 1][x + 1] == Tile.Empty) {
                     grid[y][x] = Tile.Empty;
                     grid[y + 1][x + 1] = Tile.Sand;
                  }
               }
            }
         }
      }

      // This data should be stored in a file
      public Color GetTileColor(Tile tile) {
         switch (tile) {
            case Tile.Empty: {
               return Color.BLACK;
            }

            case Tile.Sand: {
               return Color.YELLOW;
            }

            case Tile.Rock: {
               return Color.GRAY;
            }

            default: throw new Exception("No color for tile");
         }
      }

      public bool IsInBounds(int x, int y) {
         return x >= 0 && x < grid[0].Length && y >= 0 && y < grid.Length;
      }

      public void CreateSurroundingWalls() {
         // Bottom wall
         for (int x = 0; x < grid[0].Length; x++) {
            grid[grid.Length - 1][x] = Tile.Rock;
         }

         // Left wall
         for (int y = 0; y < grid.Length; y++) {
            grid[y][0] = Tile.Rock;
         }

         // Right wall
         for (int y = 0; y < grid.Length; y++) {
            grid[y][grid[0].Length - 1] = Tile.Rock;
         }
      }
   }
}