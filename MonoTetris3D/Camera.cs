using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoTetris3D
{
    public class Camera
    {
        private int _screenWidth, _screenHeight;
        private float _fieldOfView;
        private Vector3 _position, _target;
        private Matrix _view, _projection;

        // Wow 3D is complicated.
        // nearPlane = the distance the closest object we can render is
        // farPlane = the furthest distance the camera can see.
        private const float nearPlane = 0.01f;
        private const float farPlane = 100f;

        public Matrix View { get { return _view; } }
        public Matrix Projection { get { return _projection; } }

        public Camera(Vector3 position, Vector3 target, int screenWidth, int screenHeight, float fieldOfView)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _fieldOfView = fieldOfView;
            _position = position;
            _target = target;

            CalculateMatrices();
        }
        public void SetCameraPosition(Vector3 position)
        {
            _position = position;
            CalculateMatrices();
        }

        private void CalculateMatrices()
        {
            _view = Matrix.CreateLookAt(_position, _target, Vector3.Up);
            float aspect = (float)_screenWidth / (float)_screenHeight;
            _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOfView, aspect, nearPlane, farPlane);
        }

    }
}
