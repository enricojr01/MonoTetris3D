using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MonoTetris3D
{
    internal class Playfield
    {
        private Cell[][] _cells;
        private const int COLUMNS = 10;
        private const int LINES = 20;

        private Vector3 _position;

        public Playfield(Vector3 position)
        {
            _position = position;
            _cells = new Cell[LINES][];
            for (int i = 0; i < LINES; i++)
            {
                _cells[i] = new Cell[COLUMNS];
                for (int j = 0; j < COLUMNS; j++)
                {
                    _cells[i][j] = new Cell(){ Occupied = false, Color = Color.Black };
                }
            }
        }

        public void Draw()
        {
            for (int y = 0; y < LINES; y++)
            {
                // left border
                Assets.Models.DrawCube(
                    Matrix.CreateTranslation(_position) * Matrix.CreateTranslation(-1 * 0.4f, -y * 0.4f, 0),
                    Color.Gray
                );

                // right burder;
                Assets.Models.DrawCube(
                    Matrix.CreateTranslation(_position) * Matrix.CreateTranslation(COLUMNS * 0.4f, -y * 0.4f, 0),
                    Color.Gray
                );

                // cells in this line;
                for (int x = 0; x < COLUMNS; x++)
                {
                    // if cell is empty, skip
                    if (!_cells[y][x].Occupied)
                    {
                        continue;
                    }
                    Assets.Models.DrawCube(
                        Matrix.CreateTranslation(_position) * Matrix.CreateTranslation(x * 0.4f, -y * 0.4f, 0),
                        _cells[y][x].Color
                    );
                }
            }

            // Draw grid bottom
            for (int x = -1; x < COLUMNS + 1; x++)
            {
                Assets.Models.DrawCube(
                    Matrix.CreateTranslation(_position) * Matrix.CreateTranslation(x * 0.4f, -20 * 0.4f, 0), 
                    Color.Gray
                );
            }
        }

        public bool LockInPlace(Tetrimino shape, int leftcolumn, int topline)
        {
            if (topline < 0)
            {
                return false;
            }

            for (int y = 0; y < shape.CurrentShape.shapeBit.Length; y++)
            {
                for (int x = 0; x < shape.CurrentShape.shapeBit[y].Length; x++)
                {
                    if (shape.CurrentShape.shapeBit[y][x])
                    {
                        _cells[topline + y][leftcolumn + x].Occupied = true;
                        _cells[topline + y][leftcolumn + x].Color = shape.Color;
                    }
                }
            }

            return true;
        }

        public bool DoesShapeFitHere(Tetrimino shape, int leftcolumn, int topline)
        {
            for (int y = 0; y < shape.CurrentShape.shapeBit.Length; y++)
            {
                for (int x = 0; x < shape.CurrentShape.shapeBit[y].Length; x++)
                {
                    if (shape.CurrentShape.shapeBit[y][x])
                    {
                        // shape isn't allowed to fit in the border
                        if (topline + y >= LINES) { return false; }

                        // not allowed in the left wall either
                        if (leftcolumn + x < 0) { return false; }

                        // not alllowed in the right wall
                        if (leftcolumn + x >= COLUMNS) { return false; }

                        // no need to block the top because pieces
                        // spawn above the playfield.
                        if (topline + y < 0) { continue; }

                        // if the space is already occupied then no
                        if (_cells[topline + y][leftcolumn + x].Occupied) { return false; }
                    }
                }
            }

            // all checks came out clear
            return true;
        }
    }
}
