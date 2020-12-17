using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door17
{
    internal class Grid
    {
        private Dictionary<Coordinate, GridElement> m_elements;
        private int m_dimensions;

        public Grid(int a_dimensions)
        {
            m_dimensions = a_dimensions;
            m_elements = new Dictionary<Coordinate, GridElement>();
        }

        public void SetElementActive(Coordinate a_coordinate)
        {
            if (a_coordinate.Length != m_dimensions)
                throw new ArgumentOutOfRangeException();

            GridElement newElement;
            if (m_elements.TryGetValue(a_coordinate, out newElement))
            {
                if (newElement.IsActive)
                    return;
                newElement.IsActive = true;
            }
            else
            {
                newElement = new GridElement(a_coordinate, this) { IsActive = true };
                m_elements.Add(a_coordinate, newElement);
            }
            EnsureNeigboursExists(newElement);
        }

        private void EnsureElementExists(Coordinate a_coordinate)
        {
            if (a_coordinate.Length != m_dimensions)
                throw new ArgumentOutOfRangeException();

            if (m_elements.TryGetValue(a_coordinate, out _))
                return;
            var newElement = new GridElement(a_coordinate, this) { IsActive = false };
            m_elements.Add(a_coordinate, newElement);
        }

        private void EnsureNeigboursExists(GridElement a_element)
        {
            var neighbours = a_element.GetNeighbourCoordinates();
            foreach (var coordinate in neighbours)
                EnsureElementExists(coordinate);
        }

        public bool IsElementActive(Coordinate a_coordinate)
        {
            if (a_coordinate.Length != m_dimensions)
                throw new ArgumentOutOfRangeException();

            if (m_elements.TryGetValue(a_coordinate, out var element))
                return element.IsActive;

            return false;
        }

        public Grid NextCycle()
        {
            var newGrid = new Grid(m_dimensions);

            foreach(var element in m_elements)
            {
                if (element.Value.WillBeActiveInNextCycle())
                    newGrid.SetElementActive(element.Value.Coordinate);
            }

            return newGrid;
        }

        public int NumberOfActiveElements()
        {
            int result = 0;
            foreach (var element in m_elements)
                if (element.Value.IsActive)
                    result++;

            return result;
        }
    }
}
