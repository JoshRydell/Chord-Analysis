using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chord_Analysis
{
    class ComplexNumber
    {
        public double Re;
        public double Im;
        public float Modulus
        {
            get { return (float)Math.Sqrt(Math.Pow(Re, 2) + Math.Pow(Im, 2)); }
        }
        public float Argument
        {
            get { return (float)(Math.Atan(Im / Re)); }
        }
        public ComplexNumber(double theta, double modulus)
        {
            Re = modulus * Math.Cos(theta);
            Im = modulus * Math.Sin(theta);
        }
        public ComplexNumber()
        {
            Re = 0;
            Im = 0;
        }
        public static ComplexNumber operator +(ComplexNumber z, ComplexNumber w)
        {
            ComplexNumber x = new ComplexNumber();
            x.Re = z.Re + w.Re;
            x.Im = z.Im + w.Im;
            return x;
        }
        public static ComplexNumber operator -(ComplexNumber z, ComplexNumber w)
        {
            ComplexNumber x = new ComplexNumber();
            x.Re = z.Re - w.Re;
            x.Im = z.Im - w.Im;
            return x;
        }
        public static ComplexNumber operator *(ComplexNumber z, int w)
        {
            ComplexNumber x = new ComplexNumber();
            x.Re = z.Re * w;
            x.Im = z.Im * w;
            return x;
        }

        public static ComplexNumber operator /(ComplexNumber z, int w)
        {
            ComplexNumber x = new ComplexNumber();
            x.Re = z.Re / w;
            x.Im = z.Im / w;
            return x;
        }

        public static ComplexNumber operator *(ComplexNumber z, ComplexNumber w)
        {
            ComplexNumber x = new ComplexNumber();
            x.Re = z.Re * w.Re - w.Im * z.Im;
            x.Im = z.Im * w.Re + z.Re * w.Im;
            return x;
        }
    }
}
