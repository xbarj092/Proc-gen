using System;
using UnityEngine;

namespace MapGenerator
{
    /// <summary>
    /// Represents a grid data structure.
    /// </summary>
    /// <typeparam name="TGridObject">The type of objects stored in the grid.</typeparam>
    internal class Grid<TGridObject>
    {
        private int _width;
        private int _height;
        private float _cellSize;
        private TGridObject[,] _gridArray;

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid{TGridObject}"/> class with the specified dimensions and cell size.
        /// </summary>
        /// <param name="width">The width of the grid in cells.</param>
        /// <param name="height">The height of the grid in cells.</param>
        /// <param name="cellSize">The size of each cell in world units.</param>
        /// <param name="createGridObject">A function to create grid objects at each cell position.</param>
        internal Grid(int width, int height, float cellSize, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;

            _gridArray = new TGridObject[width, height];

            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < _gridArray.GetLength(1); y++)
                {
                    if (createGridObject != null)
                    {
                        _gridArray[x, y] = createGridObject(this, x, y);
                        /*Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.red, 100);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.red, 100);*/
                    }
                }
                /*Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.red, 100);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.red, 100);*/
            }
        }

        /// <summary>
        /// Converts grid coordinates to world position.
        /// </summary>
        /// <param name="x">The x-coordinate of the cell.</param>
        /// <param name="y">The y-coordinate of the cell.</param>
        /// <returns>The world position of the specified cell.</returns>
        internal Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, 0, y) * _cellSize;
        }

        /// <summary>
        /// Converts world position to grid coordinates.
        /// </summary>
        /// <param name="worldPosition">The world position to convert.</param>
        /// <param name="x">The x-coordinate of the resulting grid position.</param>
        /// <param name="y">The y-coordinate of the resulting grid position.</param>
        internal void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt(worldPosition.x / _cellSize);
            y = Mathf.FloorToInt(worldPosition.z / _cellSize);
        }

        /// <summary>
        /// Sets the object at the specified grid coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the cell.</param>
        /// <param name="y">The y-coordinate of the cell.</param>
        /// <param name="value">The value to set at the specified cell.</param>
        internal void SetGridObject(int x, int y, TGridObject value)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                _gridArray[x, y] = value;
            }
        }

        /// <summary>
        /// Sets the object at the grid position corresponding to the specified world position.
        /// </summary>
        /// <param name="worldPosition">The world position of the cell.</param>
        /// <param name="value">The value to set at the specified cell.</param>
        internal void SetGridObject(Vector3 worldPosition, TGridObject value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetGridObject(x, y, value);
        }

        /// <summary>
        /// Gets the object at the specified grid coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the cell.</param>
        /// <param name="y">The y-coordinate of the cell.</param>
        /// <returns>The object at the specified grid coordinates.</returns>
        internal TGridObject GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                return _gridArray[x, y];
            }

            return default(TGridObject);
        }

        /// <summary>
        /// Gets the object at the grid position corresponding to the specified world position.
        /// </summary>
        /// <param name="worldPosition">The world position of the cell.</param>
        /// <returns>The object at the specified grid coordinates.</returns>
        internal TGridObject GetGridObject(Vector3 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetGridObject(x, y);
        }

        /// <summary>
        /// Gets the width of the grid.
        /// </summary>
        /// <returns>The width of the grid in cells.</returns>
        internal int GetWidth()
        {
            return _width;
        }

        /// <summary>
        /// Gets the height of the grid.
        /// </summary>
        /// <returns>The height of the grid in cells.</returns>
        internal int GetHeight()
        {
            return _height;
        }
    }
}
