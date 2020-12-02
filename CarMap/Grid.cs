using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMap
{
    /// <summary>
    /// An object built on top of a 2D array of characters. 
    /// 0 signifies a space in the grid that should be ignored,
    /// 'p' means a part of the road, and 's' means a stop sign.
    /// The origin point of the path uses the character 'a' to signify the starting point A, and
    /// the final destination of the path uses the character 'b' to signify the end point B.
    /// </summary>
    class Grid
    {
        public char[,] Map;
        public Dictionary<char, Texture2D> Images;
        public Grid(string path, Dictionary<char, Texture2D> image)
        {
            Images = image;

            var lines = File.ReadAllLines(path);
            int width = lines[0].Split(' ').Length;
            int height = lines.Length;
            Map = new char[width, height];
            for (int i = 0; i < height; i++) //looping through the lines (vertical component)
            {
                var splitLine = lines[i].Trim().Split(' ');
                for (int j = 0; j < width; j++) //looping through each character in each line (horizontal component)
                {
                    Map[j, i] = splitLine[j][0];
                }
            }
        }
        public Grid(int width, int height, Dictionary<char, Texture2D> image)
        {
            Images = image;
            Map = new char[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Map[width, height] = (char)0;
                }
            }
        }

        public void drawMap(SpriteBatch spriteBatch, Vector2 origin, int size = 20)
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    //testing
                    if (!Images.ContainsKey(Map[i, j]))
                        throw new Exception();

                    spriteBatch.Draw(Images[Map[i, j]],
                    new Rectangle(new Point((i * size) + (int)origin.X, (j * size) + (int)origin.Y),
                        new Point(size, size)), null, Color.White);
                }
            }
        }

        /// <summary>
        /// Gets a list of coordinates for adjacent cells that can be traveled to.
        /// Note: After you have stopped at a stop sign tile, your previous coordinates will be equal to your current coordinates.
        /// </summary>
        /// <param name="currX">The x-coordinate of the car's current tile.</param>
        /// <param name="currY">The y-coordinate of the car's current tile.</param>
        /// <param name="prevX">The x-coordinate of the car's previous tile.</param>
        /// <param name="prevY">The y-coordinate of the car's previous tile.</param>
        /// <returns>A list of valid cells that can be traveled to. Returns null if no valid moves are found.</returns>
        public List<Vector2> GetValidMoves(int currX, int currY, int prevX, int prevY)
        {
            if (!isInMap(currX, currY)) //checking if the current position is in the map
                return null;

            //checking if the current position is a stop sign, and if you just arrived at the stop sign
            //in the case that you have just arrived at a stop sign, then the only valid move is waiting at that stop sign.
            if (Map[currX, currY] == 's' && (currX != prevX || currY != prevY))
            {
                var list = new List<Vector2>();
                list.Add(new Vector2(currX, currY));
                return list;
            }
            var adjCells = GetAdjCells(currX, currY);
            List<Vector2> moves = new List<Vector2>();
            foreach (var coord in adjCells)
            {
                if (coord.X != prevX || coord.Y != prevY)
                {
                    moves.Add(coord);
                }
            }

            return moves;
        }
        /// <summary>
        /// Given a coordinate, calculates the adjacent valid cells in the form of 2D vectors.
        /// </summary>
        /// <param name="x">The x-coordinate of the current cell</param>
        /// <param name="y">The y-coordinate of the current cell</param>
        /// <returns>A list of coordinates that correspond to valid cells in the Map.</returns>
        public List<Vector2> GetAdjCells(int x, int y)
        {
            /*
            if (!isInMap(x, y))
                return null;
            */ //redundant?
            List<Vector2> positions = new List<Vector2>();
            if (isInMap(x - 1, y) && Map[x - 1, y] != 0)
            {
                positions.Add(new Vector2(x - 1, y));
            }
            if (isInMap(x + 1, y) && Map[x + 1, y] != 0)
            {
                positions.Add(new Vector2(x + 1, y));
            }
            if (isInMap(x, y - 1) && Map[x, y - 1] != 0)
            {
                positions.Add(new Vector2(x, y - 1));
            }
            if (isInMap(x, y + 1) && Map[x, y + 1] != 0)
            {
                positions.Add(new Vector2(x, y + 1));
            }

            if (positions.Count < 1)
            {
                return null;
            }
            return positions;
        }

        private bool isInMap(int x, int y)
        {
            if ((x < 0 || y < 0) || (x > Map.GetLength(0) - 1 || y > Map.GetLength(1) - 1))
            {
                return false;
            }
            return true;
        }
    }
}
