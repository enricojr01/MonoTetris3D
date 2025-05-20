using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTetris3D
{
    internal class Player
    {
        // The main game board
        private Playfield _playfield;

        // A class responsible for generating a random Tetrimino
        private TetriminoFactory _pieceFactory;

        // Controller class for managing the input from the player
        private InputManager _playerInput;

        // The current piece - i.e the one that's on the board.
        private Tetrimino _currentPiece;

        // This one is for the position of the current piece.
        private int _x, _y;

        // These two are for the position of the ghost piece
        private int _ghostX, _ghostY;

        // These vars control the drop speed.
        private double _dropSpeed, _dropTimer;

        // Soft Drop Factor - affects how quickly a piece drops while
        // the soft drop button is held down
        private const int SDF = 6;

        // Used to track whether the player is in control of the 
        // pieces or not.
        private PlayerStates _state;

        // Current level - affects the gravity, and how fast pieces
        // drop while playing in general.
        public int Level = 1;

        private enum PlayerStates
        {
            Playing,
            WaitingForClearComplete
        }

        public Player(Playfield playfield)
        {
            _playfield = playfield;
            _pieceFactory = new TetriminoFactory();
            _playerInput = new InputManager();
            _dropSpeed = CalculateDropSpeed(Level);
            _dropTimer = _dropSpeed;

            GeneratePiece();

            _state = PlayerStates.Playing;
            _playfield.LinesClearedCompleteEvent += PlayfieldLinesClearedCompleteEvent;
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
            if (_state == PlayerStates.Playing)
            {
                _playfield.DrawTetrimino(_currentPiece, _x, _y);
                _playfield.DrawGhostTetrimino(_currentPiece, _ghostX, _ghostY);
            }
        }

        public void Update(GameTime gameTime) 
        {
            _dropTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            _playerInput.Update();
            _playfield.Update(gameTime);

            CalculateGhostPiece();

            switch (_state)
            {
                case PlayerStates.WaitingForClearComplete:
                    {
                        break;
                    }
                case PlayerStates.Playing:
                    {
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
                        break;
                    }
            }

        }

        private void SoftlockPiece()
        {
            _playfield.LockInPlace(_currentPiece, _x, _y);
            if (_playfield.ValidateField() > 0)
            {
                _state = PlayerStates.WaitingForClearComplete;
            }
            _playfield.ClearLines();
            GeneratePiece();
        }
        private void HardDrop()
        {
            _playfield.LockInPlace(_currentPiece, _ghostX, _ghostY);
            if (_playfield.ValidateField() > 0)
            {
                // _playfield.ClearLines();
                _state = PlayerStates.WaitingForClearComplete;
            }
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

        private void PlayfieldLinesClearedCompleteEvent(object sender, LinesClearedEventArgs e)
        {
            _state = PlayerStates.Playing;
        }
    }
}
