using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chord_Analysis
{
    class Tone
    {
        public float Phase { get; private set; }
        public float SpectralDensity { get; private set; }
        public float LogSpectralDensity { get; private set; }
        public float Frequency { get; private set; }
        public string Letter { get; private set; }
        public int Octave { get; private set; }
        public string Note { get; private set; }
        public int NoteIndex { get; private set; }

        private static string[] Notes = new string[] { "A", "Bb", "B", "C", "C#", "D", "Eb", "E", "F", "F#", "G", "G#" };
        public Tone (float Phase, float SpectralDensity, float Frequency)
        {
            this.Phase = Phase; 
            this.SpectralDensity = SpectralDensity;
            if(SpectralDensity > 1)
            {
                LogSpectralDensity = (float)Math.Log10(SpectralDensity);
            }
            else
            {
                LogSpectralDensity = 0;
            }
            this.Frequency = Frequency;
            int note = (int)Math.Round(12 * Math.Log(Frequency/440,2) + 48); //calculates what the notes number is on the keyboard
            NoteIndex = Math.Abs(note % 12);
            Letter = Notes[NoteIndex];
            Octave = (note + 9) / 12;
            Note = Letter + Octave.ToString();
        }
    }
}
