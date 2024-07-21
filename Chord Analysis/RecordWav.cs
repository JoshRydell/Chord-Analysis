using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.IO;

namespace Chord_Analysis
{
    static class RecordWav
    {
        static List<WaveInCapabilities> sources = new List<WaveInCapabilities>();
        static WaveFileWriter waveFileWrite;
        static WaveIn sourceStream;
        static MemoryStream file;
        public static void Start()
        {
            file = new MemoryStream();
            for (int i = 0; i < WaveIn.DeviceCount; i++) //Find microphones
            {
                sources.Add(WaveIn.GetCapabilities(i));
            }

            sourceStream = new WaveIn(); 
            sourceStream.DeviceNumber = 0; //device 0 is the current microhpne selected by user
            sourceStream.WaveFormat = new WaveFormat(44100, WaveIn.GetCapabilities(0).Channels);

            sourceStream.DataAvailable += new EventHandler<WaveInEventArgs>(sourceStream_DataAvailable);
            waveFileWrite = new WaveFileWriter(file, sourceStream.WaveFormat);

            sourceStream.StartRecording();
        }

        public static byte[] Stop()
        {
            if (sourceStream != null) //if there is an instance of source stream
            {
                sourceStream.StopRecording();
                sourceStream = null;
            }

            return file.ToArray();
        }

        private static void sourceStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFileWrite == null) return;

            waveFileWrite.Write(e.Buffer, 0, e.BytesRecorded); //write data
            waveFileWrite.Flush(); //update WAV header

        }
    }
}
