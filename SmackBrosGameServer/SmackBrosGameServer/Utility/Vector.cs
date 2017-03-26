using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace SmackBrosGameServer
{
    public class Vector2
    {
        protected bool Equals(Vector2 other)
        {
            return _x.Equals(other._x) && _y.Equals(other._y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vector2) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_x.GetHashCode()*397) ^ _y.GetHashCode();
            }
        }

        private float _x;
        private float _y;
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public Vector2(float a, float b)
        {
            X = a;
            Y = b;
        }
        public Vector2(Vector2 v)
        {
            this.X = v.X;
            this.Y = v.Y;
        }
        public void Reverse()
        {
            this.Y = -this.Y;
        }
        public float Length
        {
            get 
            { 
                return (float)Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)); 
            }
        }
        public static float Distance(Vector2 a, Vector2 b)
        {
            return (float)Math.Sqrt(Math.Pow(b.Y - a.Y, 2) + Math.Pow(b.X - a.X, 2));
        }
        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }
        public static Vector2 operator+(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2 operator-(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2 operator-(Vector2 v)
        {
            return new Vector2(-v.X, -v.Y);
        }
        public static bool operator==(Vector2 a, Vector2 b)
        {
            return (a.X == b.X) && (a.Y == b.Y);
        }
        public static bool operator!=(Vector2 a, Vector2 b)
        {
            return !(a == b);
        }
        public static Vector2 operator*(Vector2 a, float scalar)
        {
            return new Vector2(a.X * scalar, a.Y * scalar);
        }
        public float Dot(Vector2 other)
        {
            return Dot(this, other);
        }
        public static Vector2 operator/(Vector2 v1, float s2)
        {
            return
            (
               new Vector2
               (
                  (float)(v1.X / s2),
                  (float)(v1.Y / s2)
               )
            );
        }
        public static bool IsUnitVector(Vector2 v1)
        {
            return v1.Length == 1;
        }
        public bool IsUnitVector()
        {
            return IsUnitVector(this);
        }
        public Vector2 Normalize()
        {
            var magnitude = this.Length;
            if(magnitude == 0)
            {
                return new Vector2(0,0);
            }
            var inverse = 1 / magnitude;
            return new Vector2(this.X * inverse, this.Y * inverse);
        }
        public static Vector2 Projection(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v2 * ((float)(v1.Dot(v2) / Math.Pow(v2.Length, 2))));
        }

        public Vector2 Projection(Vector2 direction)
        {
            return Projection(this, direction);
        }
        public static Vector2 Zero
        {
            get { return new Vector2(0, 0); }
            set { }
        }
    }
    class Vector3 : IComparable
    {
        public float X;
        public float Y;
        public float Z;
        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3(float[] xyz)
        {
            if (xyz.Length == 3)
            {
                this.X = xyz[0];
                this.Y = xyz[1];
                this.Z = xyz[2];
            }
            else
            {
                throw new ArgumentException(ThreeComponents);
            }
        }

        public Vector3(Vector3 v1)
        {
            this.X = v1.X;
            this.Y = v1.Y;
            this.Z = v1.Z;
        }
        private const string ThreeComponents =
           "Array must contain exactly three components , (x,y,z)";
        public float Magnitude
        {
            get
            {
                return Convert.ToSingle(Math.Sqrt((float)SumComponentSqrs()));
            }
        }
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(
               v1.X + v2.X,
               v1.Y + v2.Y,
               v1.Z + v2.Z);
        }
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(
               v1.X - v2.X,
               v1.Y - v2.Y,
               v1.Z - v2.Z);
        }
        public static Vector3 operator -(Vector3 v1)
        {
            return new Vector3(
               -v1.X,
               -v1.Y,
               -v1.Z);
        }
        public static Vector3 operator +(Vector3 v1)
        {
            return new Vector3(
               +v1.X,
               +v1.Y,
               +v1.Z);
        }
        public static bool operator <(Vector3 v1, Vector3 v2)
        {
            return v1.SumComponentSqrs() < v2.SumComponentSqrs();
        }
        public static bool operator <=(Vector3 v1, Vector3 v2)
        {
            return v1.SumComponentSqrs() <= v2.SumComponentSqrs();
        }
        public static bool operator >=(Vector3 v1, Vector3 v2)
        {
            return v1.SumComponentSqrs() >= v2.SumComponentSqrs();
        }
        public static bool operator >(Vector3 v1, Vector3 v2)
        {
            return v1.SumComponentSqrs() > v2.SumComponentSqrs();
        }
        public float SumComponentSqrs()
        {
            return Convert.ToSingle(Math.Pow((double)this.X, 2) + Math.Pow((double)this.Y, 2) + Math.Pow((double)this.Z, 2));
        }
        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return
               v1.X == v2.X &&
               v1.Y == v2.Y &&
               v1.Z == v2.Z;
        }
        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !(v1 == v2);
        }
        public override bool Equals(object other)
        {
            // Check object other is a Vector3 object
            var vector = other as Vector3;
            if (vector != null)
            {
                // Convert object to Vector3
                Vector3 otherVector = vector;

                // Check for equality
                return otherVector.Equals(this);
            }
            return false;
        }
        public static Vector3 Transform(Vector3 position, Matrix4 matrix)
        {
            Transform(ref position, ref matrix, out position);
            return position;
        }

        public static void Transform(ref Vector3 position, ref Matrix4 matrix, out Vector3 result)
        {
            result = new Vector3((position.X * matrix.M11) + (position.Y * matrix.M21) + (position.Z * matrix.M31) + matrix.M41,
                                 (position.X * matrix.M12) + (position.Y * matrix.M22) + (position.Z * matrix.M32) + matrix.M42,
                                 (position.X * matrix.M13) + (position.Y * matrix.M23) + (position.Z * matrix.M33) + matrix.M43);
        }
        public static Vector3 Lerp(Vector3 v1, Vector3 v2, float weight)
        {
            return v1 + (v2 - v1) * weight;
        }
        public bool Equals(Vector3 other)
        {
            return
               this.X.Equals(other.X) &&
               this.Y.Equals(other.Y) &&
               this.Z.Equals(other.Z);
        }
        public bool Equals(object other, float tolerance)
        {
            if (other is Vector3)
            {
                return this.Equals((Vector3)other, tolerance);
            }
            return false;
        }

        public bool Equals(Vector3 other, float tolerance)
        {
            return
               AlmostEqualsWithAbsTolerance(this.X, other.X, tolerance) &&
               AlmostEqualsWithAbsTolerance(this.Y, other.Y, tolerance) &&
               AlmostEqualsWithAbsTolerance(this.Z, other.Z, tolerance);
        }

        public static bool AlmostEqualsWithAbsTolerance(float a, float b, float maxAbsoluteError)
        {
            float diff = Math.Abs(a - b);

            if (a.Equals(b))
            {
                // shortcut, handles infinities
                return true;
            }

            return diff <= maxAbsoluteError;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.X.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Y.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Z.GetHashCode();
                return hashCode;
            }
        }
        public int CompareTo(object other)
        {
            if (other is Vector3)
            {
                if (this < (Vector3) other)
                {
                    return -1;
                }
                else if (this > (Vector3) other)
                {
                    return 1;
                }
                return 0;
            }
            // Error condition: other is not a Vector3 object
            throw new ArgumentException(
                NonVectorComparison + "\n" +
                ArgumentType + other.GetType(),
                "other");
        }
        public static Vector3 operator *(Vector3 v1, float s2)
        {
            return
               new Vector3
               (
                  v1.X * s2,
                  v1.Y * s2,
                  v1.Z * s2
               );
        }
        public static Vector3 operator *(float s1, Vector3 v2)
        {
            return v2 * s1;
        }
        public static Vector3 CrossProduct(Vector3 v1, Vector3 v2)
        {
            return
               new Vector3
               (
                  v1.Y * v2.Z - v1.Z * v2.Y,
                  v1.Z * v2.X - v1.X * v2.Z,
                  v1.X * v2.Y - v1.Y * v2.X
               );
        }
        public Vector3 CrossProduct(Vector3 other)
        {
            return CrossProduct(this, other);
        }
        public static float DotProduct(Vector3 v1, Vector3 v2)
        {
            return
            v1.X * v2.X +
            v1.Y * v2.Y +
            v1.Z * v2.Z;
        }
        public float DotProduct(Vector3 other)
        {
            return DotProduct(this, other);
        }
        public static Vector3 operator /(Vector3 v1, float s2)
        {
            return
            new Vector3
            (
                v1.X / s2,
                v1.Y / s2,
                v1.Z / s2
            );
        }
        public static bool IsUnitVector(Vector3 v1)
        {
            return v1.Magnitude == 1;
        }

        public bool IsUnitVector()
        {
            return IsUnitVector(this);
        }
        public bool IsUnitVector(float tolerance)
        {
            return IsUnitVector(this, tolerance);
        }

        public static bool IsUnitVector(Vector3 v1, float tolerance)
        {
            return AlmostEqualsWithAbsTolerance(v1.Magnitude, 1, tolerance);
        }

        private const string NonVectorComparison =
        "Cannot compare a Vector3 to a non-Vector3";

        private const string ArgumentType =
        "The argument provided is a type of ";
        public static Vector3 Normalize(Vector3 v1)
        {
            var magnitude = v1.Magnitude;

            // Check that we are not attempting to normalize a vector of magnitude 0
            if (magnitude == 0)
            {
                return new Vector3(0, 0, 0);
            }

            // Special Cases
            if (float.IsInfinity(v1.Magnitude))
            {
                var x =
                    v1.X == 0 ? 0 :
                        v1.X == -0 ? -0 :
                            float.IsPositiveInfinity(v1.X) ? 1 :
                                float.IsNegativeInfinity(v1.X) ? -1 :
                                    float.NaN;
                var y =
                    v1.Y == 0 ? 0 :
                        v1.Y == -0 ? -0 :
                            float.IsPositiveInfinity(v1.Y) ? 1 :
                                float.IsNegativeInfinity(v1.Y) ? -1 :
                                    float.NaN;
                var z =
                    v1.Z == 0 ? 0 :
                        v1.Z == -0 ? -0 :
                            float.IsPositiveInfinity(v1.Z) ? 1 :
                                float.IsNegativeInfinity(v1.Z) ? -1 :
                                    float.NaN;

                var result = new Vector3(x, y, z);

                // If this was a special case return the special case result
                return result;
            }

            // Run the normalization as usual
            return NormalizeOrNaN(v1);
        }

        public Vector3 Normalize()
        {
            return Normalize(this);
        }


        private static Vector3 NormalizeOrNaN(Vector3 v1)
        {
            // find the inverse of the vectors magnitude
            float inverse = 1 / v1.Magnitude;

            return new Vector3(
                // multiply each component by the inverse of the magnitude
                v1.X * inverse,
                v1.Y * inverse,
                v1.Z * inverse);
        }

        private const string Normalize0 =
            "Cannot normalize a vector when it's magnitude is zero";

        private const string NormalizeNaN =
            "Cannot normalize a vector when it's magnitude is NaN";
        public static float Distance(Vector3 v1, Vector3 v2)
        {
            return
               Convert.ToSingle(Math.Sqrt((float)(
                   (v1.X - v2.X) * (v1.X - v2.X) +
                   (v1.Y - v2.Y) * (v1.Y - v2.Y) +
                   (v1.Z - v2.Z) * (v1.Z - v2.Z))));
        }

        public float Distance(Vector3 other)
        {
            return Distance(this, other);
        }
    }
}
