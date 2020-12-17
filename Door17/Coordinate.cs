using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Day13_ShuttleSearch.Door17
{
    internal class Coordinate : IEquatable<Coordinate>
    {
        private int[] m_coordinate;

        public Coordinate(int[] a_coordinates)
        {
            m_coordinate = a_coordinates;
        }

        public int Length => m_coordinate.Length;

        public int this[int a_idx] => m_coordinate[a_idx];

        public override bool Equals(object a_obj)
        {
            return Equals(a_obj as Coordinate);
        }

        public override string ToString()
        {
            var sb = new StringBuilder(m_coordinate.Length * 2);
            for (var i = 0; i < m_coordinate.Length; i++)
                sb.Append(m_coordinate[i]);
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public bool Equals([AllowNull] Coordinate a_other)
        {
            return this == a_other;
        }

        public static bool operator == (Coordinate a_first, Coordinate a_second)
        {
            if (ReferenceEquals(a_first, a_second))
                return true;
            if (ReferenceEquals(a_first, null))
                return false;
            if (ReferenceEquals(a_second, null))
                return false;

            if (a_first.m_coordinate.Length != a_second.m_coordinate.Length)
                return false;

            for (var i = 0; i < a_first.m_coordinate.Length; i++)
                if (a_first.m_coordinate[i] != a_second.m_coordinate[i])
                    return false;

            return true;
        }

        public static bool operator !=(Coordinate a_first, Coordinate a_second)
        {
            return !(a_first == a_second);
        }

    }
}
