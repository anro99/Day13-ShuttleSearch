using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door17
{
    internal class GridElement
    {
        public GridElement(Coordinate a_coordinate, Grid a_grid)
        {
            Coordinate = a_coordinate;
            Grid = a_grid;
            IsActive = false;
        }

        public Coordinate Coordinate { get; private set; }
        public Grid Grid { get; private set; }
        public bool IsActive { get; set; }

        public List<Coordinate> GetNeighbourCoordinates()
        {
            var nextNeibourCoordinats = new List<int>();
            var result = new List<Coordinate>();
            EnumerateNeigbour(0, result, nextNeibourCoordinats);
            return result;
        }

        private void EnumerateNeigbour(int a_dimension, List<Coordinate> a_neigbours, List<int> a_nextCoordinats)
        {
            if (a_dimension >= Coordinate.Length)
            {
                var neigbour = new Coordinate(a_nextCoordinats.ToArray());
                if (neigbour == Coordinate)
                    return;
                a_neigbours.Add(neigbour);
                return;
            }

            for( var i = Coordinate[a_dimension] - 1; i <= Coordinate[a_dimension] + 1; i++)
            {
                a_nextCoordinats.Add(i);
                EnumerateNeigbour(a_dimension + 1, a_neigbours, a_nextCoordinats);
                a_nextCoordinats.RemoveAt(a_nextCoordinats.Count - 1);
            }
        }

        public bool WillBeActiveInNextCycle()
        {
            var neigbours = GetNeighbourCoordinates();
            int nbOfActiveNeibours = 0;
            foreach (var coordinate in neigbours)
                if (Grid.IsElementActive(coordinate))
                    nbOfActiveNeibours++;
            if (IsActive)
            {
                if (nbOfActiveNeibours == 2 || nbOfActiveNeibours == 3)
                    return true;
                return false;
            }

            return nbOfActiveNeibours == 3;
        }
    }
}
