using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTetris3D
{
    internal class Player
    {
        private Playfield _playfield;
        private TetriminoFactory _pieceFactory;

        private InputManager _playerInput;

        private Tetrimino _currentPiece;
        // this one is for the position of the current piece.
        private int _x, _y;
        private int _ghostX, _ghostY;
        private double _dropSpeed, _dropTimer;
        private const int SDF = 6;

        public int Level = 1;

        public Player(Playfield playfield)
        {
            _playfield = playfield;
            _pieceFactory = new TetriminoFactory();
            _playerInput = new InputManager();
            _dropSpeed = CalculateDropSpeed(Level);
            _dropTimer = _dropSpeed;

            GeneratePiece();
        }

        private void GeneratePiece()
        {
            _currentPiece = _pieceFactory.GenerateRandom();
            _x = 4;
            _y = -2;
        }

        public void Draw()
        {
            _playfield.Draw();
            _playfield.DrawTetrimino(_currentPiece, _x, _y);
        }

        public void Update(GameTime gameTime) 
        {
            _dropTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            _playerInput.Update();

            CalculateGhostPiece();

            if (_playerInput.IsPressed(InputManager.Controls.Left))
            {
                if (_playfield.DoesShapeFitHere(_currentPiece, _x - 1, _y))
                {
                    _x -= 1;
                }
            }

            if (_playerInput.IsPressed(InputManager.Controls.Right))
            {
                if (_playfield.DoesShapeFitHere(_currentPiece, _x + 1, _y))
                {
                    _x += 1;
                }
            }

            if (_playerInput.IsPressed(InputManager.Controls.RotateCW))
            {
                _currentPiece.RotateLeft();
                if (!_playfield.DoesShapeFitHere(_currentPiece, _x, _y))
                {
                    _currentPiece.RotateRight();
                }
            }

            if (_playerInput.IsPressed(InputManager.Controls.RotateCCW))
            {
                _currentPiece.RotateRight();
                if (!_playfield.DoesShapeFitHere(_currentPiece, _x, _y))
                {
                    _currentPiece.RotateLeft();
                }
            }

            if (_playerInput.IsDown(InputManager.Controls.SoftDrop))
            {
                _dropTimer -= (SDF * _dropSpeed) * gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (_playerInput.IsPressed(InputManager.Controls.HardDrop))
            {
                HardDrop();
            }

            if (_dropTimer < 0)
            {
                if (_playfield.DoesShapeFitHere(_currentPiece, _x, _y + 1))
                {
                    _y++;
                }
                else
                {
                    SoftlockPiece();
                }

                _dropTimer += _dropSpeed;
            }
        }

        private void SoftlockPiece()
        {
            _playfield.LockInPlace(_currentPiece, _x, _y);

            GeneratePiece();
        }
        private void HardDrop()
        {
            _playfield.LockInPlace(_currentPiece, _ghostX, _ghostY);

            GeneratePiece();
        }

        private void CalculateGhostPiece()
        {
            _ghostX = _x;
            _ghostY = _y;

            while(_playfield.DoesShapeFitHere(_currentPiece, _ghostX, _ghostY + 1))
            {
                _ghostY++;
            }
        }

        private double CalculateDropSpeed(int level)
        {
            level = MathHelper.Clamp(level, 1, 20);
            return Math.Pow((0.8d - (level - 1) * 0.007d), (level - 1));
        }
    }
}
