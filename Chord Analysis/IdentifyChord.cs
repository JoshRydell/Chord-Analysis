using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chord_Analysis
{
    static class IdentifyChord
    {
        private static Tuple<int, string>[] CommonChords =
        {
            Tuple.Create(12, "A note"),
            Tuple.Create(13, "Bb note"),
            Tuple.Create(14, "B note"),
            Tuple.Create(15, "C note"),
            Tuple.Create(16, "C# note"),
            Tuple.Create(17, "D note"),
            Tuple.Create(18, "Eb note"),
            Tuple.Create(19, "E note"),
            Tuple.Create(20, "F note"),
            Tuple.Create(21, "F# note"),
            Tuple.Create(22, "G note"),
            Tuple.Create(23, "G# note"),
            Tuple.Create(447, "D5"),
            Tuple.Create(508, "A5"),
            Tuple.Create(509, "Eb5"),
            Tuple.Create(574, "Bb5"),
            Tuple.Create(575, "E5"),
            Tuple.Create(644, "B5"),
            Tuple.Create(645, "F5"),
            Tuple.Create(718, "C5"),
            Tuple.Create(719, "F#5"),
            Tuple.Create(796, "C#5"),
            Tuple.Create(797, "G5"),
            Tuple.Create(879, "G#5"),
            Tuple.Create(172590, "Esus4"),
            Tuple.Create(173178, "Adim"),
            Tuple.Create(193765, "Am"),
            Tuple.Create(216165, "F"),
            Tuple.Create(216823, "A"),
            Tuple.Create(216824, "Fsus4"),
            Tuple.Create(217483, "Bbdim"),
            Tuple.Create(240483, "F#dim"),
            Tuple.Create(241872, "Asus4"),
            Tuple.Create(241873, "Bbm"),
            Tuple.Create(267558, "F#m"),
            Tuple.Create(268290, "Dm"),
            Tuple.Create(268291, "F#"),
            Tuple.Create(269024, "Bb"),
            Tuple.Create(269025, "F#sus4"),
            Tuple.Create(269759, "Bdim"),
            Tuple.Create(296847, "D"),
            Tuple.Create(296848, "Gdim"),
            Tuple.Create(298391, "Bbsus4"),
            Tuple.Create(298392, "Bm"),
            Tuple.Create(327657, "Dsus4"),
            Tuple.Create(328467, "Ebdim"),
            Tuple.Create(328468, "Gm"),
            Tuple.Create(329279, "Ebm"),
            Tuple.Create(329280, "G"),
            Tuple.Create(330092, "B"),
            Tuple.Create(330093, "Gsus4"),
            Tuple.Create(330906, "Cdim"),
            Tuple.Create(362539, "Eb"),
            Tuple.Create(362540, "G#dim"),
            Tuple.Create(364245, "Bsus4"),
            Tuple.Create(364246, "Cm"),
            Tuple.Create(398291, "Ebsus4"),
            Tuple.Create(399184, "Edim"),
            Tuple.Create(399185, "G#m"),
            Tuple.Create(400079, "Em"),
            Tuple.Create(400080, "G#"),
            Tuple.Create(400975, "C"),
            Tuple.Create(400976, "G#sus4"),
            Tuple.Create(401872, "C#dim"),
            Tuple.Create(438530, "E"),
            Tuple.Create(440406, "Csus4"),
            Tuple.Create(440407, "C#m"),
            Tuple.Create(474214, "Am add9"),
            Tuple.Create(480704, "Fdim"),
            Tuple.Create(481686, "Fm"),
            Tuple.Create(482669, "C#"),
            Tuple.Create(482990, "Fadd11"),
            Tuple.Create(483653, "Ddim"),
            Tuple.Create(509908, "Aadd9"),
            Tuple.Create(520027, "Bbm maj7"),
            Tuple.Create(527894, "C#sus4"),
            Tuple.Create(559490, "Bbmaj7"),
            Tuple.Create(576591, "Am add11"),
            Tuple.Create(586266, "F#m add11"),
            Tuple.Create(587349, "Dm6"),
            Tuple.Create(607069, "Aadd11"),
            Tuple.Create(607070, "Bbm add9"),
            Tuple.Create(616995, "Dm7"),
            Tuple.Create(616996, "F#add11"),
            Tuple.Create(629244, "Bm7"),
            Tuple.Create(643882, "Gm add9"),
            Tuple.Create(648509, "Dm maj7"),
            Tuple.Create(649649, "Bbadd9"),
            Tuple.Create(659916, "D7"),
            Tuple.Create(661065, "F7"),
            Tuple.Create(661066, "Bm maj7"),
            Tuple.Create(673743, "Gadd9"),
            Tuple.Create(674904, "B7"),
            Tuple.Create(692494, "Dmaj7"),
            Tuple.Create(707845, "Fmaj7"),
            Tuple.Create(707846, "Bmaj7"),
            Tuple.Create(729476, "Bbm add11"),
            Tuple.Create(740354, "F#m6"),
            Tuple.Create(740355, "Gm add11"),
            Tuple.Create(741572, "Ebm6"),
            Tuple.Create(754996, "Cm6"),
            Tuple.Create(756225, "Am6"),
            Tuple.Create(766181, "Bbadd11"),
            Tuple.Create(766182, "Bm add9"),
            Tuple.Create(773509, "Em add11"),
            Tuple.Create(777328, "Dm add9"),
            Tuple.Create(777329, "Ebm7"),
            Tuple.Create(777330, "Gadd11"),
            Tuple.Create(791071, "F#m7"),
            Tuple.Create(791072, "Cm7"),
            Tuple.Create(807475, "Am7"),
            Tuple.Create(807476, "G#m add9"),
            Tuple.Create(815204, "Ebm maj7"),
            Tuple.Create(816482, "Badd9"),
            Tuple.Create(826618, "Eadd11"),
            Tuple.Create(827988, "Dadd9"),
            Tuple.Create(827989, "Eb7"),
            Tuple.Create(829276, "F#7"),
            Tuple.Create(829277, "Cm maj7"),
            Tuple.Create(843469, "A7"),
            Tuple.Create(843470, "G#add9"),
            Tuple.Create(844768, "F#m maj7"),
            Tuple.Create(844769, "C7"),
            Tuple.Create(861718, "Am maj7"),
            Tuple.Create(863031, "Fadd9"),
            Tuple.Create(867064, "Ebmaj7"),
            Tuple.Create(884233, "F#maj7"),
            Tuple.Create(884234, "Cmaj7"),
            Tuple.Create(898888, "Amaj7"),
            Tuple.Create(911085, "Bm add11"),
            Tuple.Create(923239, "Gm6"),
            Tuple.Create(923240, "G#m add11"),
            Tuple.Create(924599, "Em6"),
            Tuple.Create(939582, "Dm add11"),
            Tuple.Create(939583, "C#m6"),
            Tuple.Create(940954, "Bbm6"),
            Tuple.Create(954813, "Badd11"),
            Tuple.Create(954814, "Cm add9"),
            Tuple.Create(960224, "Fm add11"),
            Tuple.Create(967254, "Ebm add9"),
            Tuple.Create(967255, "Em7"),
            Tuple.Create(967256, "G#add11"),
            Tuple.Create(982579, "Gm7"),
            Tuple.Create(982580, "C#m7"),
            Tuple.Create(1000852, "Dadd11"),
            Tuple.Create(1000853, "Bbm7"),
            Tuple.Create(1012295, "Em maj7"),
            Tuple.Create(1013719, "Cadd9"),
            Tuple.Create(1022153, "F#m add9"),
            Tuple.Create(1026537, "Ebadd9"),
            Tuple.Create(1026538, "E7"),
            Tuple.Create(1027971, "G7"),
            Tuple.Create(1027972, "C#m maj7"),
            Tuple.Create(1043768, "Bb7"),
            Tuple.Create(1045213, "Gm maj7"),
            Tuple.Create(1045214, "C#7"),
            Tuple.Create(1065518, "F#add9"),
            Tuple.Create(1072922, "Emaj7"),
            Tuple.Create(1092013, "Gmaj7"),
            Tuple.Create(1092014, "C#maj7"),
            Tuple.Create(1124826, "Cm add11"),
            Tuple.Create(1138328, "G#m6"),
            Tuple.Create(1139838, "Fm6"),
            Tuple.Create(1156469, "Ebm add11"),
            Tuple.Create(1157991, "Bm6"),
            Tuple.Create(1176421, "Cadd11"),
            Tuple.Create(1176422, "C#m add9"),
            Tuple.Create(1190228, "Em add9"),
            Tuple.Create(1190229, "Fm7"),
            Tuple.Create(1207223, "G#m7"),
            Tuple.Create(1227470, "Ebadd11"),
            Tuple.Create(1243286, "Fm maj7"),
            Tuple.Create(1244864, "C#add9"),
            Tuple.Create(1259066, "Eadd9"),
            Tuple.Create(1260654, "G#7"),
            Tuple.Create(1279742, "G#m maj7"),
            Tuple.Create(1334737, "G#maj7"),
            Tuple.Create(1374299, "C#m add11"),
            Tuple.Create(1434653, "C#add11"),
            Tuple.Create(1449898, "Fm add9"),
        };
        public static string DiagnoseChord(Tone[] Tones)
        {
            int numberOfTones = 4;
            List<Tone> FoundTones = FindSignificantTones(Tones);
            List<int> MostPresentTones = new List<int>();
            int index = 0;
            while(MostPresentTones.Count < numberOfTones && index < FoundTones.Count) // find most present notes
            {
                MostPresentTones.Add(FoundTones[index].NoteIndex);
                index++;
            }

           for(int i = 0; i < MostPresentTones.Count; i++) //remove duplicate note
            {
                for(int j = i+1; j < MostPresentTones.Count; j++)
                {
                    if(MostPresentTones[i] == MostPresentTones[j])
                    {
                        MostPresentTones.RemoveAt(j);
                        j--;
                    }
                }
            }

            int[] ChordTones = MostPresentTones.ToArray(); // convert to array
            for(int i = 0; i < ChordTones.Length; i++) //increment indexes so all have a value between 12 and 23
            {
                ChordTones[i]+= 12;
            }
            if(ChordTones.Length == 0)
            {
                return "Unknown Chord";
            }

            Sort(ChordTones); //sort to remove inversions complications
            int hash = CPairing(ChordTones); //get hash
            return Search(hash);
                
        }
        public static List<Tone> FindSignificantTones(Tone[] Tones)
        {
            List<Tone> FoundTones = new List<Tone>();

            Sort(Tones);
            int index = 0;
            while(Tones[index].SpectralDensity > 0.25*Tones[0].SpectralDensity && index < Tones.Length)
            {
                if(!NoteAlreadyExists(Tones[index].Note, FoundTones) && !Harmonic(Tones[index].Frequency, FoundTones))
                {
                    FoundTones.Add(Tones[index]);
                }
                index++;
            }
            return FoundTones;
        }
        private static bool NoteAlreadyExists(string Note, List<Tone> FoundTones)
        {
            for(int i = 0; i < FoundTones.Count; i++)
            {
                if(FoundTones[i].Note == Note) //check if the note already exists in the FoundTones array
                {
                    return true;
                }
            }
            return false;
        }
        private static bool Harmonic(float frequency, List<Tone> FoundTones)
        {
            for(int i = 0; i < FoundTones.Count; i++)
            {

                if (frequency > FoundTones[i].Frequency)
                {
                    float harmonicCheck = (float)Math.Round(frequency / FoundTones[i].Frequency); //calculate closest multiple of the considered frequency
                    if (harmonicCheck >= 2) //if ratio is greater than or equal to two, it could be a harmonic
                    {
                        harmonicCheck = Math.Abs(FoundTones[i].Frequency * harmonicCheck - frequency); //calculates how many Hz the considered frequency is off by
                        if (harmonicCheck < FoundTones[i].Frequency / 5) //checks if the frequency is off within a 10th of the significant tones frequency
                        {
                                return true;
                        }
                    }
                }
            }

            return false;
        }
        private static int CPairing(int[] arr)
        {
            if (arr.Length == 1) //return if array is size one
            {
                return arr[0];
            }

            int[] left = new int[arr.Length / 2];
            int[] right = new int[arr.Length - arr.Length / 2];
            for (int i = 0; i < left.Length; i++) //split array into left and right
            {
                left[i] = arr[i];
            }

            for (int i = 0; i < right.Length; i++)
            {
                right[i] = arr[i + arr.Length / 2];
            }
            int rightHash = CPairing(right); //calculate hash values for left and right
            int leftHash = CPairing(left);

            return (int)(0.5f * (rightHash + leftHash) * (rightHash + leftHash + 1) + leftHash);
        }
        private static void Sort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int insertionPoint = i;
                int currentNum = arr[i];
                while (insertionPoint > 0 && arr[insertionPoint - 1] > currentNum)
                {
                    arr[insertionPoint] = arr[insertionPoint - 1];
                    insertionPoint--;
                }
                arr[insertionPoint] = currentNum;
            }
        }
        private static void Sort(Tone[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int insertionPoint = i;
                Tone currentNum = arr[i];
                while (insertionPoint > 0 && arr[insertionPoint - 1].SpectralDensity < currentNum.SpectralDensity)
                {
                    arr[insertionPoint] = arr[insertionPoint - 1];
                    insertionPoint--;
                }
                arr[insertionPoint] = currentNum;
            }
        }
        private static string Search( int targetHash)
        {
            int left = 0;
            int right = CommonChords.Length - 1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (CommonChords[mid].Item1 == targetHash)
                {
                    return CommonChords[mid].Item2;
                }
                else if (CommonChords[mid].Item1 > targetHash)
                {
                    right = mid - 1;
                }
                else if (CommonChords[mid].Item1 < targetHash)
                {
                    left = mid + 1;
                }
            }
            return "Unknown Chord";
        }
    }
}
