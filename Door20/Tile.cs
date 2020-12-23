using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door20
{
    internal class Tile
    {
        public enum Modifications
        {
            RotateLeft,
            SwapHorizontal
        };

        public static List<Modifications> ModificationList = new List<Modifications> {
        Modifications.RotateLeft,
        Modifications.RotateLeft,
        Modifications.RotateLeft,
        Modifications.SwapHorizontal,
        Modifications.RotateLeft,
        Modifications.RotateLeft,
        Modifications.RotateLeft,
        Modifications.SwapHorizontal};

        private enum Neighbour
        {
            Above,
            Left,
            Under,
            Right
        }

        private static List<Neighbour> m_neighbours = new List<Neighbour> { 
            Neighbour.Right,
            Neighbour.Under, 
            Neighbour.Left,
            Neighbour.Above, };

        private byte[,] m_grid = new byte[Size, Size];
        private int m_nextModificationIdx = 0;

        private Tile()
        {
            Coordinate = (-1, -1);
        }

        public static int Size => 10;
        public static (int X, int Y) UnusedCoordinate => (-1, -1);

        public long Number { get; private set; }

        public bool Used => Coordinate != UnusedCoordinate;


        internal bool CheckFit()
        {
            for (int orientationChanges = 0; orientationChanges < ModificationList.Count; orientationChanges++)
            {
                if (CheckIfMatchToNeighbours())
                {
                    if (CheckFitNeighbours())
                        return true;

                }
                ChangeOrientationToNextOne();
            }
            return false;
        }

        internal bool CheckFitNeighbours()
        {
            return CheckFitNeighbour(0);
        }

        internal bool CheckFitNeighbour(int a_neighbourIdx)
        {
            if (a_neighbourIdx >= m_neighbours.Count)
                return true;

            var neigbourCoordinate = GetNeighbourCoordinate(m_neighbours[a_neighbourIdx]);
            if (!Picture.IsValidCoordinate(neigbourCoordinate) || 
                Picture.IsATileInPlace(neigbourCoordinate))
                return CheckFitNeighbour(a_neighbourIdx + 1);

            var unusedTiles = Picture.GetAllUnusedTiles();
            foreach (var neighbourTile in unusedTiles)
            {
                if (!Picture.PlaceTile(neighbourTile, neigbourCoordinate))
                    continue;

                if (neighbourTile.CheckFit())
                    return CheckFitNeighbour(a_neighbourIdx + 1);
                Picture.RemoveTileFromPicture(neighbourTile);
            }
            return false;
        }

        private (int X, int Y) GetNeighbourCoordinate(Neighbour a_neighbour)
        {
            switch(a_neighbour)
            {
                case Neighbour.Above:
                    return (Coordinate.X, Coordinate.Y - 1);
                case Neighbour.Left:
                    return (Coordinate.X - 1, Coordinate.Y);
                case Neighbour.Under:
                    return (Coordinate.X, Coordinate.Y + 1);
                case Neighbour.Right:
                    return (Coordinate.X + 1, Coordinate.Y);
            }
            return Tile.UnusedCoordinate;
        }

        public (int X, int Y) Coordinate { get; set; }

        public Picture Picture { get; set; }

        private bool CheckIfMatchToNeighbours()
        {
            var above = Picture.GetAbove(this);
            if (null != above)
            {
                var theirBorder = above.GetLowerBorder();
                var ourBorder = GetUpperBorder();
                if (!EqualBorder(theirBorder, ourBorder))
                    return false;
            }
            var under = Picture.GetUnder(this);
            if (null != under)
            {
                var theirBorder = under.GetUpperBorder();
                var ourBorder = GetLowerBorder();
                if (!EqualBorder(theirBorder, ourBorder))
                    return false;
            }
            var left = Picture.GetLeft(this);
            if (null != left)
            {
                var theirBorder = left.GetRightBorder();
                var ourBorder = GetLeftBorder();
                if (!EqualBorder(theirBorder, ourBorder))
                    return false;
            }
            var right = Picture.GetRight(this);
            if (null != right)
            {
                var theirBorder = right.GetLeftBorder();
                var ourBorder = GetRightBorder();
                if (!EqualBorder(theirBorder, ourBorder))
                    return false;
            }
            return true;
        }

        private static bool EqualBorder(byte[] a_first, byte[] a_second)
        {
            if (a_first == null || a_second == null ||
                a_first.Length != a_second.Length ||
                a_first.Length != Tile.Size)
                return false;
            for (int i = 0; i < Tile.Size; i++)
            {
                if (a_first[i] != a_second[i])
                    return false;
            }
            return true;
        }

        private byte[] GetUpperBorder()
        {
            var border = new byte[Tile.Size];
            for (int x = 0; x < Tile.Size; x++)
                border[x] = m_grid[x,0];
            return border;
        }

        private byte[] GetLowerBorder()
        {
            var border = new byte[Tile.Size];
            for (int x = 0; x < Tile.Size; x++)
                border[x] = m_grid[x,Tile.Size - 1];
            return border;
        }

        private byte[] GetLeftBorder()
        {
            var border = new byte[Tile.Size];
            for (int y = 0; y < Tile.Size; y++)
                border[y] = m_grid[0,y];
            return border;
        }

        private byte[] GetRightBorder()
        {
            var border = new byte[Tile.Size];
            for (int y = 0; y < Tile.Size; y++)
                border[y] = m_grid[Tile.Size - 1,y];
            return border;
        }


        private bool ChangeOrientationToNextOne()
        {
            switch(ModificationList[m_nextModificationIdx])
            {
                case Modifications.RotateLeft:
                    RotateLeft();
                    break;
                case Modifications.SwapHorizontal:
                    SwapHorizontal();
                    break;
            }
            m_nextModificationIdx += 1;
            if (m_nextModificationIdx >= ModificationList.Count)
            {
                m_nextModificationIdx = 0;
                return false;
            }
            return true;
        }

        private void RotateLeft()
        {
            m_grid = Matrix.RotateLeft(m_grid);
        }

        private void SwapHorizontal()
        {
            m_grid = Matrix.SwapHorizontal(m_grid);
        }

        internal void CopyToImageWithoutBorder(byte[,] m_image, (int X, int Y) insertCoordinate)
        {
            for (int y = 1; y < Size - 1; y++)
            {
                for (int x = 1; x < Size - 1; x++)
                {
                    m_image[x + insertCoordinate.X - 1, y + insertCoordinate.Y - 1] = m_grid[x, y];
                }
            }
        }

        public static bool TryParse(List<string> a_lines, out Tile a_tile)
        {
            a_tile = null;
            int pos;
            if (a_lines == null || 
                a_lines.Count != 11 || 
                !a_lines[0].StartsWith("Tile ") ||
                -1 == (pos = a_lines[0].IndexOf(':')))
                return false;
            a_tile = new Tile();
            if (!int.TryParse(a_lines[0].Substring(5, pos - 5), out var nummer))
            {
                a_tile = null;
                return false;
            }
            a_tile.Number = nummer;

            for(int y=0; y<Size; y++)
            {
                int x = 0;
                foreach(var chr in a_lines[y+1].ToCharArray())
                {
                    if (chr == '#')
                        a_tile.m_grid[x,y] = 1;
                    x++;
                }
            }

            return true;
        }

    }
}
