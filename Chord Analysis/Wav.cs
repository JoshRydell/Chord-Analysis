using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Chord_Analysis
{
    class Wav
    {
        public string fileName { get; private set; }
        public int sampleRate { get; private set; }
        public int numberOfChannels { get; private set; }
        public int[][] channelsData { get; private set; }
        public int[] averageOfChannels { get; private set; }
        public int channelDataSize { get; private set; }
        public int Size { get; private set; }
        public bool GenuineWAV { get; private set; }
        public byte[] RawData { get; private set; }

        private static int littleEndianToInt(byte[] bytes)
        {
            int value = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                value += (int)Math.Pow(256, i) * Convert.ToUInt16(bytes[i]);
            }

            return value;
        }

        public Wav(string path)
        {
            fileName = path;
            byte[] wav = File.ReadAllBytes(path);
            GenuineWAV = true;

            if(wav.Length > 44)
            {
                if (littleEndianToInt(new byte[] { wav[0], wav[1], wav[2], wav[3] }) != 1179011410 || //if file is not RIFF
                littleEndianToInt(new byte[] { wav[8], wav[9], wav[10], wav[11] }) != 1163280727 || // or if file type is not WAVE
                littleEndianToInt(new byte[] { wav[20], wav[21] }) != 1) // or if file format is not PCM
                {
                    GenuineWAV = false;
                    return;
                }
                RawData = wav;
                numberOfChannels = littleEndianToInt(new byte[] { wav[22], wav[23] });
                Size = wav.Length - 44;
                channelsData = new int[numberOfChannels][];
                sampleRate = littleEndianToInt(new byte[] { wav[24], wav[25], wav[26], wav[27] });
                channelDataSize = Size / (2 * numberOfChannels);
                averageOfChannels = new int[channelDataSize];


                for (int i = 0; i < channelsData.Length; i++)
                {
                    channelsData[i] = new int[channelDataSize];
                }

                int index = 0;
                for (int i = 0; i < channelDataSize; i++)
                {

                    for (int j = 0; j < numberOfChannels; j++)
                    {
                        channelsData[j][i] = littleEndianToInt(new byte[] { wav[index * 2 + 44], wav[index * 2 + 45] });
                        index++;
                    }
                }

                for (int i = 0; i < channelDataSize; i++)
                {
                    int total = 0;
                    for (int j = 0; j < numberOfChannels; j++)
                    {
                        total += channelsData[j][i];
                    }
                    averageOfChannels[i] = total / numberOfChannels;
                }

                
            }
            else
            {
                GenuineWAV = false;
            }



        }



        public Wav(byte[] data)
        {

            byte[] wav = data;
            if (wav.Length > 44) //if there are at leass 44 bytes in the WAV file
            {
                if (littleEndianToInt(new byte[] { wav[0], wav[1], wav[2], wav[3] }) != 1179011410 || //if file is not RIFF
                littleEndianToInt(new byte[] { wav[8], wav[9], wav[10], wav[11] }) != 1163280727 || // or if file type is not WAVE
                littleEndianToInt(new byte[] { wav[20], wav[21] }) != 1) // or if file format is not PCM
                {
                    GenuineWAV = false;
                    return;
                }
                RawData = wav;
                numberOfChannels = littleEndianToInt(new byte[] { wav[22], wav[23] });
                Size = wav.Length - 44;
                channelsData = new int[numberOfChannels][];
                sampleRate = littleEndianToInt(new byte[] { wav[24], wav[25], wav[26], wav[27] });
                channelDataSize = Size / (2 * numberOfChannels);
                averageOfChannels = new int[channelDataSize];


                for (int i = 0; i < channelsData.Length; i++)
                {
                    channelsData[i] = new int[channelDataSize];
                }

                int index = 0;
                for (int i = 0; i < channelDataSize; i++)
                {

                    for (int j = 0; j < numberOfChannels; j++)
                    {
                        channelsData[j][i] = littleEndianToInt(new byte[] { wav[index * 2 + 44], wav[index * 2 + 45] });
                        index++;
                    }
                }

                for (int i = 0; i < channelDataSize; i++)
                {
                    int total = 0;
                    for (int j = 0; j < numberOfChannels; j++)
                    {
                        total += channelsData[j][i];
                    }
                    averageOfChannels[i] = total / numberOfChannels;
                }
            }
            else
            {
                GenuineWAV = false;
            }
        }
    }
}

