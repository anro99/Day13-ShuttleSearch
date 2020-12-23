using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door20
{
    internal class Picture
    {
        private List<Tile> m_tiles = new List<Tile>();
        private Tile[,] m_orderedTiles = null;
        private int m_nbTilesPerSide = -1;

        private byte[,] m_image = null;

        private static string[] m_strMonster =
        {
        "                  # ",
        "#    ##    ##    ###",
        " #  #  #  #  #  #   ",
        };

        private static byte[,] m_monster = null;

        static Picture()
        {
            m_monster = new byte[m_strMonster[0].Length, m_strMonster.Length];
            for (int y = 0; y < m_strMonster.Length; y++)
            {
                int x = 0;
                foreach (var chr in m_strMonster[y].ToCharArray())
                {
                    if (chr == '#')
                        m_monster[x, y] = 1;
                    x++;
                }
            }
        }

        public Picture()
        {
        }

        public int NumberOfTiles => m_tiles.Count;
        public int Length => m_nbTilesPerSide; 


        internal void Add(Tile tile)
        {
            tile.Picture = this;
            m_tiles.Add(tile);
        }

        internal bool ArrangeTiles()
        {
            m_nbTilesPerSide = (int)Math.Sqrt(m_tiles.Count);
            if (m_nbTilesPerSide * m_nbTilesPerSide != m_tiles.Count)
            {
                m_nbTilesPerSide = -1;
                return false;
            }
            m_orderedTiles = new Tile[m_nbTilesPerSide, m_nbTilesPerSide];

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
            long result = m_orderedTiles[0, 0].Number;
            result = result * m_orderedTiles[0, Length - 1].Number;
            result = result * m_orderedTiles[Length - 1, Length - 1].Number;
            result = result * m_orderedTiles[Length - 1, 0].Number;
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

            m_orderedTiles[a_coordinate.X, a_coordinate.Y] = a_tile;
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
                m_orderedTiles[a_coordinate.X, a_coordinate.Y] != null)
                return true;
            return false;
        }

        internal void RemoveTileFromPicture(Tile a_tile)
        {
            if (!a_tile.Used)
                return;
            m_orderedTiles[a_tile.Coordinate.X, a_tile.Coordinate.Y] = null;
            a_tile.Coordinate = Tile.UnusedCoordinate;
        }

        internal Tile GetNextTileToPlaceNeigbours(Tile a_tile)
        {
            if (!a_tile.Used)
                return null;

            var nextTile = GetRight(a_tile);
            if (null == nextTile && a_tile.Coordinate.Y < Length - 1)
                return m_orderedTiles[0, a_tile.Coordinate.Y + 1];
            return null;
        }


        internal Tile GetAbove(Tile a_tile)
        {
            if (!a_tile.Used || Length == -1)
                return null;
            if (a_tile.Coordinate.Y == 0)
                return null;
            return m_orderedTiles[a_tile.Coordinate.X, a_tile.Coordinate.Y - 1];
        }
        internal Tile GetUnder(Tile a_tile)
        {
            if (!a_tile.Used || Length == -1)
                return null;
            if (a_tile.Coordinate.Y == Length - 1)
                return null;
            return m_orderedTiles[a_tile.Coordinate.X, a_tile.Coordinate.Y + 1];
        }
        internal Tile GetLeft(Tile a_tile)
        {
            if (!a_tile.Used || Length == -1)
                return null;
            if (a_tile.Coordinate.X == 0)
                return null;
            return m_orderedTiles[a_tile.Coordinate.X - 1, a_tile.Coordinate.Y];
        }
        internal Tile GetRight(Tile a_tile)
        {
            if (!a_tile.Used || Length == -1)
                return null;
            if (a_tile.Coordinate.X == Length - 1)
                return null;
            return m_orderedTiles[a_tile.Coordinate.X + 1, a_tile.Coordinate.Y];
        }


        public bool CreateImage()
        {
            if (!ArrangeTiles())
                return false;

            m_image = new byte[Length * (Tile.Size - 2), Length * (Tile.Size - 2)];

            for (var y = 0; y < Length; y++)
            {
                for (var x = 0; x < Length; x++)
                {
                    var insertCoordinate = (x * (Tile.Size - 2), y * (Tile.Size - 2));

                    m_orderedTiles[x,y].CopyToImageWithoutBorder(m_image, insertCoordinate);
                }
            }

            return true;
        }

        public bool MarkMonster()
        {
            var imageForSearch = Matrix.Copy(m_image);
            for (var modificationIdx = 0; modificationIdx < Tile.ModificationList.Count; modificationIdx++ )
            {
                var position = (0, 0);
                var monsterFound = false;
                while(Matrix.FindNext(ref position, imageForSearch, m_monster))
                {
                    monsterFound = true;
                    Matrix.StampShape(position, imageForSearch, m_monster, 2);
                }
                if (monsterFound)
                {
                    m_image = imageForSearch;
                    return true;
                }

                switch(Tile.ModificationList[modificationIdx])
                {
                    case Tile.Modifications.RotateLeft:
                        imageForSearch = Matrix.RotateLeft(imageForSearch);
                        break;
                    default:
                        imageForSearch = Matrix.SwapHorizontal(imageForSearch);
                        break;
                }
            }
            return false;
        }

        public int Roughness()
        {
            int result = 0;
            for (int x = 0; x < m_image.GetLength(0); x++)
            {
                for (int y = 0; y < m_image.GetLength(1); y++)
                {
                    if (m_image[x, y] == 1)
                        result++;
                }

            }
            return result;
        }


    }
}
