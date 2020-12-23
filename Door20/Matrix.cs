using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door20
{
    class Matrix
    {

        public static byte[,] Copy(byte [,] a_matrix)
        {
            byte[,] newMatrix = new byte[a_matrix.GetLength(0), a_matrix.GetLength(1)];
            for (int y = 0; y < a_matrix.GetLength(1); y++)
            {
                for (int x = 0; x < a_matrix.GetLength(0); x++)
                {
                    newMatrix[x, y] = a_matrix[x, y];
                }
            }
            return newMatrix;
        }

        public static byte[,] RotateLeft(byte[,] a_matrix)
        {
            byte[,] newMatrix = new byte[a_matrix.GetLength(1), a_matrix.GetLength(0)];
            int newColumn, newRow = 0;
            for (int oldColumn = a_matrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
            {
                newColumn = 0;
                for (int oldRow = 0; oldRow < a_matrix.GetLength(0); oldRow++)
                {
                    newMatrix[newRow, newColumn] = a_matrix[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }
            return newMatrix;
        }

        public static byte[,] SwapHorizontal(byte[,] a_matrix)
        {
            byte[,] newMatrix = new byte[a_matrix.GetLength(0), a_matrix.GetLength(1)];
            for (int y = 0; y < a_matrix.GetLength(1); y++)
            {
                for (int x = 0; x < a_matrix.GetLength(0); x++)
                {
                    newMatrix[x, y] = a_matrix[x, a_matrix.GetLength(1) - y - 1];
                }
            }
            return newMatrix;
        }

        public static bool MatchShape((int X, int Y) a_upperLeft, byte[,] a_matrix, byte[,] a_shape)
        {
            int shapeWidth = a_shape.GetLength(0);
            int shapeHight = a_shape.GetLength(1);
            int matrixWidth = a_matrix.GetLength(0);
            int matrixHight = a_matrix.GetLength(1);

            if ((a_upperLeft.X + shapeWidth >= matrixWidth) ||
                 (a_upperLeft.Y + shapeHight >= matrixHight))
                return false;


            for (int x = 0; x < shapeWidth; x++)
            {
                for (int y = 0; y < shapeHight; y++)
                {
                    if (a_shape[x, y] == 0)
                        continue;

                    if (a_shape[x, y] != a_matrix[a_upperLeft.X + x, a_upperLeft.Y + y])
                        return false;
                }

            }
            return true;
        }

        public static bool FindNext(ref (int X, int Y) a_upperLeft, byte[,] a_matrix, byte[,] a_shape)
        {
            int shapeWidth = a_shape.GetLength(0);
            int shapeHight = a_shape.GetLength(1);
            int matrixWidth = a_matrix.GetLength(0);
            int matrixHight = a_matrix.GetLength(1);

            int x = a_upperLeft.X;
            int y = a_upperLeft.Y;
            for (; x < matrixWidth - shapeWidth; x++)
            {
                for (; y < matrixHight -shapeHight; y++)
                {
                    if (MatchShape((x,y), a_matrix, a_shape))
                    {
                        a_upperLeft = (x, y);
                        return true;
                    }
                }
                y = 0;
            }

            return false;
        }

        public static void StampShape((int X, int Y) a_upperLeft, byte[,] a_matrix, byte[,] a_shape, byte a_stampValue )
        {
            int shapeWidth = a_shape.GetLength(0);
            int shapeHight = a_shape.GetLength(1);
            int matrixWidth = a_matrix.GetLength(0);
            int matrixHight = a_matrix.GetLength(1);

            if ((a_upperLeft.X + shapeWidth >= matrixWidth) ||
                 (a_upperLeft.Y + shapeHight >= matrixHight))
                return;

            for (int x = 0; x < shapeWidth; x++)
            {
                for (int y = 0; y < shapeHight; y++)
                {
                    if (a_shape[x, y] == 0)
                        continue;

                    a_matrix[a_upperLeft.X + x, a_upperLeft.Y + y] = a_stampValue;
                }
            }
        }

    }
}
