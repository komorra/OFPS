using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;

namespace OFPSEngine.Rendering
{
    public class Camera
    {
        public Matrix Orientation;
        public Matrix View;
        public Matrix Projection;
        public Vector3 Position;
        public Vector2 Angle;

        public void UpdateOrientation()
        {
            Orientation = Matrix.RotationY(Angle.X);
            Orientation *= Matrix.RotationAxis(Orientation.Left, Angle.Y);
        }

        public void UpdateViewProjection(float w, float h)
        {
            View = Matrix.LookAtRH(Position, Position + Orientation.Forward, Orientation.Up);
            Projection = Matrix.PerspectiveFovRH(1, w/h, 0.1f, 1000);
        }
    }
}
