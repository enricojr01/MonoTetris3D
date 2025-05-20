using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoTetris3D
{
    internal class TetriminoFactory
    {
        private Random _random = new Random();

        public TetriminoFactory() { }

        public Tetrimino Generate(Tetriminoes shapeType)
        {
            Shape[] shapes = new Shape[4];
            Color color = Color.White;

            switch (shapeType) 
            {
                case Tetriminoes.I:
                    {
                        color = Color.Cyan;
                        shapes[0] = new Shape(new string[]
                        {
                            "0000",
                            "1111",
                            "0000",
                            "0000"
                        });
                        shapes[1] = new Shape(new string[]
                        {
                            "0010",
                            "0010",
                            "0010",
                            "0010"
                        });
                        shapes[2] = new Shape(new string[]
                        {
                            "0000",
                            "0000",
                            "1111",
                            "0000"
                        });
                        shapes[3] = new Shape(new string[]
                        {
                            "0100",
                            "0100",
                            "0100",
                            "0100"
                        });
                        break;
                    }
                case Tetriminoes.O:
                    {
                        color = Color.Yellow;
                        shapes = new Shape[1];
                        shapes[0] = new Shape(new string[]
                        {
                            "11",
                            "11",
                        });
                        break;
                    }
                case Tetriminoes.T:
                    {
                        color = Color.DeepPink;
                        shapes[0] = new Shape(new string[]
                        {
                            "010",
                            "111",
                            "000",
                        });
                        shapes[1] = new Shape(new string[]
                        {
                            "010",
                            "011",
                            "010",
                        });
                        shapes[2] = new Shape(new string[]
                        {
                            "000",
                            "111",
                            "010",
                        });
                        shapes[3] = new Shape(new string[]
                        {
                            "010",
                            "110",
                            "010",
                        });
                        break;
                    }
                case Tetriminoes.J:
                    {
                        color = Color.Blue;
                        shapes[0] = new Shape(new string[]
                        {
                            "100",
                            "111",
                            "000"
                        });
                        shapes[1] = new Shape(new string[]
                        {
                            "011",
                            "010",
                            "010",
                        });
                        shapes[2] = new Shape(new string[]
                        {
                            "000",
                            "111",
                            "001",
                        });
                        shapes[3] = new Shape(new string[]
                        {
                            "010",
                            "010",
                            "110",
                        });
                        break;
                    }
                case Tetriminoes.L:
                    {
                        color = Color.Orange;
                        shapes[0] = new Shape(new string[]
                        {
                            "001",
                            "111",
                            "000",
                        });
                        shapes[1] = new Shape(new string[]
                        {
                            "010",
                            "010",
                            "011",
                        });
                        shapes[2] = new Shape(new string[]
                        {
                            "000",
                            "111",
                            "100",
                        });
                        shapes[3] = new Shape(new string[]
                        {
                            "110",
                            "010",
                            "010",
                        });
                        break;
                    }
                case Tetriminoes.S:
                    {
                        color = Color.Green;
                        shapes[0] = new Shape(new string[]
                        {
                            "011",
                            "110",
                            "000",
                        });
                        shapes[1] = new Shape(new string[]
                        {
                            "010",
                            "011",
                            "001"
                        });
                        shapes[2] = new Shape(new string[]
                        {
                            "000",
                            "011",
                            "110",
                        });
                        shapes[3] = new Shape(new string[]
                        {
                            "010",
                            "110",
                            "100",
                        });
                        break;
                    }
                case Tetriminoes.Z:
                    {
                        color = Color.Red;
                        shapes[0] = new Shape(new string[]
                        {
                            "110",
                            "011",
                            "000",
                        });
                        shapes[1] = new Shape(new string[]
                        {
                            "001",
                            "011",
                            "010",
                        });
                        shapes[2] = new Shape(new string[]
                        {
                            "000",
                            "110",
                            "011",
                        });
                        shapes[3] = new Shape(new string[]
                        {
                            "010",
                            "110",
                            "100",
                        });
                        break;
                    }
            }

            Tetrimino t = new Tetrimino();
            t.SetShape(shapes, shapeType, color);

            return t;
        }

        public Tetrimino GenerateRandom()
        {
            return Generate((Tetriminoes)_random.Next(7));
        }
    }
}
