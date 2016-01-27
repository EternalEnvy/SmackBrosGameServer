using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace SmackBrosGameServer
{
    class Vector2
    {
        private float x;
        private float y;
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float Y
        {
            get { return y; }
            set { y = value; }
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
        public float Length
        {
            get 
            { 
                return (float)Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)); 
            }
        }
        public static float Distance(Vector2 A, Vector2 B)
        {
            return (float)Math.Sqrt(Math.Pow(B.Y - A.Y, 2) + Math.Pow(B.X - A.X, 2));
        }
        public static float Dot(Vector2 A, Vector2 B)
        {
            return A.X * B.X + A.Y * B.Y;
        }
        public static Vector2 operator+(Vector2 A, Vector2 B)
        {
            return new Vector2(A.X + B.X, A.Y + B.Y);
        }
        public static Vector2 operator-(Vector2 A, Vector2 B)
        {
            return new Vector2(A.X - B.X, A.Y - B.Y);
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
        public double Dot(Vector2 other)
        {
            return Dot(this, other);
        }
        public static Vector2 operator/(Vector2 v1, double s2)
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
        public double X;
        public double Y;
        public double Z;
        public Vector3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3(double[] xyz)
        {
            if (xyz.Length == 3)
            {
                this.X = xyz[0];
                this.Y = xyz[1];
                this.Z = xyz[2];
            }
            else
            {
                throw new ArgumentException(THREE_COMPONENTS);
            }
        }

        public Vector3(Vector3 v1)
        {
            this.X = v1.X;
            this.Y = v1.Y;
            this.Z = v1.Z;
        }
        private const string THREE_COMPONENTS =
           "Array must contain exactly three components , (x,y,z)";
        public double Magnitude
        {
            get
            {
                return Math.Sqrt(SumComponentSqrs());
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
        public double SumComponentSqrs()
        {
            return Math.Pow(this.X, 2) + Math.Pow(this.Y, 2) + Math.Pow(this.Z, 2);
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
            if (other is Vector3)
            {
                // Convert object to Vector3
                Vector3 otherVector = (Vector3)other;

                // Check for equality
                return otherVector.Equals(this);
            }
            else
            {
                return false;
            }
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
        public bool Equals(object other, double tolerance)
        {
            if (other is Vector3)
            {
                return this.Equals((Vector3)other, tolerance);
            }
            return false;
        }

        public bool Equals(Vector3 other, double tolerance)
        {
            return
               AlmostEqualsWithAbsTolerance(this.X, other.X, tolerance) &&
               AlmostEqualsWithAbsTolerance(this.Y, other.Y, tolerance) &&
               AlmostEqualsWithAbsTolerance(this.Z, other.Z, tolerance);
        }

        public static bool AlmostEqualsWithAbsTolerance(double a, double b, double maxAbsoluteError)
        {
            double diff = Math.Abs(a - b);

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
                if (this < (other as Vector3))
                {
                    return -1;
                }
                else if (this > (other as Vector3))
                {
                    return 1;
                }
                return 0;
            }
            // Error condition: other is not a Vector3 object
            throw new ArgumentException(
                NON_VECTOR_COMPARISON + "\n" +
                ARGUMENT_TYPE + other.GetType().ToString(),
                "other");
        }
        public static Vector3 operator *(Vector3 v1, double s2)
        {
            return
               new Vector3
               (
                  v1.X * s2,
                  v1.Y * s2,
                  v1.Z * s2
               );
        }
        public static Vector3 operator *(double s1, Vector3 v2)
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
        public static double DotProduct(Vector3 v1, Vector3 v2)
        {
            return
            (
               v1.X * v2.X +
               v1.Y * v2.Y +
               v1.Z * v2.Z
            );
        }
        public double DotProduct(Vector3 other)
        {
            return DotProduct(this, other);
        }
        public static Vector3 operator /(Vector3 v1, double s2)
        {
            return
            (
               new Vector3
               (
                  v1.X / s2,
                  v1.Y / s2,
                  v1.Z / s2
               )
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
        public bool IsUnitVector(double tolerance)
        {
            return IsUnitVector(this, tolerance);
        }

        public static bool IsUnitVector(Vector3 v1, double tolerance)
        {
            return AlmostEqualsWithAbsTolerance(v1.Magnitude, 1, tolerance);
        }

        private const string NON_VECTOR_COMPARISON =
        "Cannot compare a Vector3 to a non-Vector3";

        private const string ARGUMENT_TYPE =
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
            if (double.IsInfinity(v1.Magnitude))
            {
                var x =
                    v1.X == 0 ? 0 :
                        v1.X == -0 ? -0 :
                            double.IsPositiveInfinity(v1.X) ? 1 :
                                double.IsNegativeInfinity(v1.X) ? -1 :
                                    double.NaN;
                var y =
                    v1.Y == 0 ? 0 :
                        v1.Y == -0 ? -0 :
                            double.IsPositiveInfinity(v1.Y) ? 1 :
                                double.IsNegativeInfinity(v1.Y) ? -1 :
                                    double.NaN;
                var z =
                    v1.Z == 0 ? 0 :
                        v1.Z == -0 ? -0 :
                            double.IsPositiveInfinity(v1.Z) ? 1 :
                                double.IsNegativeInfinity(v1.Z) ? -1 :
                                    double.NaN;

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
            double inverse = 1 / v1.Magnitude;

            return new Vector3(
                // multiply each component by the inverse of the magnitude
                v1.X * inverse,
                v1.Y * inverse,
                v1.Z * inverse);
        }

        private const string NORMALIZE_0 =
            "Cannot normalize a vector when it's magnitude is zero";

        private const string NORMALIZE_NaN =
            "Cannot normalize a vector when it's magnitude is NaN";
        public static double Distance(Vector3 v1, Vector3 v2)
        {
            return
               Math.Sqrt
               (
                   (v1.X - v2.X) * (v1.X - v2.X) +
                   (v1.Y - v2.Y) * (v1.Y - v2.Y) +
                   (v1.Z - v2.Z) * (v1.Z - v2.Z)
               );
        }

        public double Distance(Vector3 other)
        {
            return Distance(this, other);
        }
    }
}
