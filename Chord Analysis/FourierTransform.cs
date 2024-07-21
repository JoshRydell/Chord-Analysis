using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chord_Analysis
{
    class FourierTransform
    {
        public static Tone[] DiscreteTransform(int[] input, int sampleRate)
        {
            ComplexNumber[] transform = new ComplexNumber[input.Length];
            for (int k = 0; k < transform.Length; k++)
            {
                ComplexNumber sum = new ComplexNumber();
                for (int n = 0; n < transform.Length; n++)
                {
                    sum += new ComplexNumber(2 * (float)Math.PI * k * n / transform.Length, input[n]);
                }
                transform[k] = sum;

            }
            return FormatTransform(transform, sampleRate);

        }

        private static Tone[] FormatTransform(ComplexNumber[] transform, int sampleRate)
        {
            
            float frequencyResolution = (float)(sampleRate) / transform.Length;
            Tone[] halfSize = new Tone[transform.Length / 2];
            float largestMagnitude = (transform[0] * 2 / transform.Length).Modulus;

            for (int i = 0; i < halfSize.Length; i++)
            {
                ComplexNumber formattedNum = (transform[i] * 2 / transform.Length);
                if (formattedNum.Modulus > largestMagnitude)
                {
                    largestMagnitude = formattedNum.Modulus;
                }
            }

            for (int i = 0; i < halfSize.Length; i++)
            {
                ComplexNumber formattedNum = (transform[i] * 2 / transform.Length);
                if(formattedNum.Modulus < largestMagnitude*0.01 || i*frequencyResolution < 20)
                {
                    halfSize[i] = new Tone(0, 0, i * frequencyResolution);
                }
                else
                {
                    halfSize[i] = new Tone(formattedNum.Argument, formattedNum.Modulus, (i+1) * frequencyResolution);
                }
            }
            return halfSize;
        }

        public static Tone[] FastTransform(int[] input, int sampleRate)
        {
            //ComplexNumber[] samples = new ComplexNumber[(int)Math.Pow(2, Math.Ceiling(Math.Log(input.Length, 2)))];
            ComplexNumber[] samples = new ComplexNumber[input.Length];
            for (int i = 0; i < samples.Length; i++)
            {
                samples[i] = new ComplexNumber
                {
                    Re = input[i]
                };
            }
            ComplexNumber[] transform = recursionSum(samples);

            return FormatTransform(transform, sampleRate);
        }

        private static ComplexNumber[] recursionSum(ComplexNumber[] input)
        {
            if (input.Length == 1)
            {
                return input;
            }
            ComplexNumber[] odd = new ComplexNumber[input.Length/2];
            ComplexNumber[] even = new ComplexNumber[input.Length / 2];
            for (int i = 0; i < input.Length / 2; i++)
            {
                odd[i] = input[2 * i + 1];
                even[i] = input[2 * i];
            }

            ComplexNumber[] FEven = recursionSum(even);
            ComplexNumber[] Fodd = recursionSum(odd);


            ComplexNumber[] output = new ComplexNumber[input.Length];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = new ComplexNumber();
            }
            for (int k = 0; k < input.Length / 2; k++)
            {
                ComplexNumber complexExponential = new ComplexNumber(-2 * (float)Math.PI * k / input.Length, 1) * Fodd[k];
                output[k] = FEven[k] + complexExponential;
                output[k + input.Length / 2] = FEven[k] - complexExponential;
            }

            return output;
        }
    }

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
            get { return (float)Math.Atan2(Im, Re); }
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
