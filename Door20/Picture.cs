using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door20
{
    internal class Picture
    {
        private List<Tile> m_tiles = new List<Tile>();
        private Tile[,] m_picture = null;
        private int m_length = -1;

        public Picture()
        {

        }

        public int NumberOfTiles => m_tiles.Count;
        public int Length => m_length; 


        internal void Add(Tile tile)
        {
            tile.Picture = this;
            m_tiles.Add(tile);
        }

        internal bool ArrangeTiles()
        {
            m_length = (int)Math.Sqrt(m_tiles.Count);
            if (m_length * m_length != m_tiles.Count)
            {
                m_length = -1;
                return false;
            }
            m_picture = new Tile[m_length, m_length];

            var unused = GetAllUnusedTiles();
            foreach( Tile tile in unused)
            {
                if (PlaceTile(tile, (0,0)))
                {
                    if (tile.CheckFit())
                        return true;
                }
                RemoveTileFromPicture(tile);
            }
            return false;
        }

        public long GetResultNumber()
        {
            long result = m_picture[0, 0].Number;
            result = result * m_picture[0, Length - 1].Number;
            result = result * m_picture[Length - 1, Length - 1].Number;
            result = result * m_picture[Length - 1, 0].Number;
            return result;
        }


        internal Tile GetNextUnusedTile()
        {
            return m_tiles.Find((a_tile) => !a_tile.Used);
        }

        internal List<Tile> GetAllUnusedTiles()
        {
            return m_tiles.FindAll((a_tile) => !a_tile.Used);
        }

        internal bool PlaceTile(Tile a_tile, (int X, int Y) a_coordinate)
        {
            if (IsATileInPlace(a_coordinate))
                return false;

            m_picture[a_coordinate.Y, a_coordinate.X] = a_tile;
            a_tile.Coordinate = a_coordinate;
            return true;
        }

        internal bool IsValidCoordinate((int X, int Y) a_coordinate)
        {
            if (a_coordinate.X < 0 || a_coordinate.Y < 0 ||
                a_coordinate.X >= Length || a_coordinate.Y >= Length)
                return false;
            return true;
        }

        internal bool IsATileInPlace((int X, int Y) a_coordinate)
        {
            if (IsValidCoordinate(a_coordinate) &&
                m_picture[a_coordinate.Y, a_coordinate.X] != null)
                return true;
            return false;
        }


        internal bool PlaceTileInNextFreePicturePosition(Tile a_tile)
        {
            bool found = !a_tile.Used;
            for (var y = 0; y < Length; y++)
            {
                for (var x = 0; x < Length; x++)
                {
                    if (!found)
                    {
                        found = m_picture[y,x] == a_tile;
                    }
                    else if (m_picture[y,x] == null)
                    {
                        RemoveTileFromPicture(a_tile);
                        a_tile.Coordinate = (x, y);
                        m_picture[y, x] = a_tile;
                        return true;
                    }
                }
            }
            RemoveTileFromPicture(a_tile);
            return false;
        }

        internal void RemoveTileFromPicture(Tile a_tile)
        {
            if (!a_tile.Used)
                return;
            m_picture[a_tile.Coordinate.Y,a_tile.Coordinate.X] = null;
            a_tile.Coordinate = Tile.UnusedCoordinate;
        }

        internal Tile GetNextTileToPlaceNeigbours(Tile a_tile)
        {
            if (!a_tile.Used)
                return null;

            var nextTile = GetRight(a_tile);
            if (null == nextTile && a_tile.Coordinate.Y < Length - 1)
                return m_picture[a_tile.Coordinate.Y + 1,0];
            return null;
        }


        internal Tile GetAbove(Tile a_tile)
        {
            if (!a_tile.Used || Length == -1)
                return null;
            if (a_tile.Coordinate.Y == 0)
                return null;
            return m_picture[a_tile.Coordinate.Y - 1, a_tile.Coordinate.X];
        }
        internal Tile GetUnder(Tile a_tile)
        {
            if (!a_tile.Used || Length == -1)
                return null;
            if (a_tile.Coordinate.Y == Length - 1)
                return null;
            return m_picture[a_tile.Coordinate.Y + 1, a_tile.Coordinate.X];
        }
        internal Tile GetLeft(Tile a_tile)
        {
            if (!a_tile.Used || Length == -1)
                return null;
            if (a_tile.Coordinate.X == 0)
                return null;
            return m_picture[a_tile.Coordinate.Y, a_tile.Coordinate.X - 1];
        }
        internal Tile GetRight(Tile a_tile)
        {
            if (!a_tile.Used || Length == -1)
                return null;
            if (a_tile.Coordinate.X == Length - 1)
                return null;
            return m_picture[a_tile.Coordinate.Y, a_tile.Coordinate.X + 1];
        }


    }
}
