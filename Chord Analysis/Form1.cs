using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace Chord_Analysis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Wav wav;
        Tone[] transform;
        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Fourier Transform";
            NoteNamesListBox.Text = "Notes";
            WindowState = FormWindowState.Maximized;
            chart1.Location = new Point(0, 0);
            chart2.Location = new Point(0, 0);
            chart3.Location = new Point(0, 0);
            NoteNamesListBox.Size = new Size(50, 200);
            chart1.Show();
            chart2.Hide();
            chart3.Hide();
            groupBox1.Text = "";
            groupBox2.Text = "";
            wavNameLabel.Text = "WAV File: ";
            chordNameLabel.Text = "Chord: ";
            MinimumSize = new Size(653, 361);
            button4.Enabled = false;
            button1.Size = new Size(79, 41);
            button2.Size = button1.Size;
            button3.Size = button1.Size;
            button4.Size = button1.Size;
            button5.Size = button1.Size;

            resize();

            radioButton1.Checked = true;
            radioButton3.Checked = true;

            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].CursorX.AutoScroll = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].CursorY.AutoScroll = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

            chart2.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart2.ChartAreas[0].CursorX.AutoScroll = true;
            chart2.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart2.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart2.ChartAreas[0].CursorY.AutoScroll = true;
            chart2.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

            chart3.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart3.ChartAreas[0].CursorX.AutoScroll = true;
            chart3.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart3.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart3.ChartAreas[0].CursorY.AutoScroll = true;
            chart3.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

            FormatCharts();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Title = "Open WAV",
                Filter = "WAV Files (*.wav)|*.wav"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                
                FormatCharts();

                string fileName = dlg.FileName;

                wavNameLabel.Text = "WAV File: " + fileName.Split('\\', '.')[fileName.Split('\\', '.').Length - 2];
                dlg.Dispose();

                wav = new Wav(fileName);
                if(wav.GenuineWAV)
                {
                    transform = FourierTransform.FastTransform(wav.averageOfChannels, wav.sampleRate);
                    DrawToGraphs();
                }
                else
                {
                    NoteNamesListBox.Items.Clear();
                    wavNameLabel.Text = "WAV File";
                    chordNameLabel.Text = "Chord: ";
                    MessageBox.Show("WAV selected is not a compatible type, please select another.");
                    wav = null;

                }

            }
        } //Load WAV
        private void button2_Click(object sender, EventArgs e) //Play WAV
        {
            if (wav != null)
            {
                try
                {
                    MemoryStream ms = new MemoryStream(wav.RawData);
                    SoundPlayer playWav = new SoundPlayer(ms);
                    playWav.Play();
                }
                catch
                {
                    MessageBox.Show("File is not a valid WAV file");
                }
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            chart3.Hide();
            if (radioButton3.Checked)
            {
                chart1.Show();
                chart2.Hide();
            }
            else if (radioButton4.Checked)
            {
                chart1.Hide();
                chart2.Show();
            }

            groupBox2.Enabled = true;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            chart3.Show();
            chart1.Hide();
            chart2.Hide();
            groupBox2.Enabled = false;
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Show();
            chart2.Hide();
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            chart2.Show();
            chart1.Hide();
        }
        private void button4_Click(object sender, EventArgs e) //Stop Record
        {
            
            FormatCharts();
            wavNameLabel.Text = "WAV File: RECORD";
            wav = new Wav(RecordWav.Stop());
            transform = FourierTransform.FastTransform(wav.averageOfChannels, wav.sampleRate);
            DrawToGraphs();

            button1.Enabled = true; ;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = false;
            button5.Enabled = true;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
        }
        private void button3_Click(object sender, EventArgs e) //Start Record
        {
            RecordWav.Start();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = true;
            button5.Enabled = false;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            resize();
        }
        private void FormatCharts()
        {
            chart1.Series[0].Dispose();
            chart2.Series[0].Dispose();
            chart3.Series[0].Dispose();
            chart1.Series[0] = new System.Windows.Forms.DataVisualization.Charting.Series();
            chart2.Series[0] = new System.Windows.Forms.DataVisualization.Charting.Series();
            chart3.Series[0] = new System.Windows.Forms.DataVisualization.Charting.Series();

            chart1.Series[0].Name = "Power Spectral Density (W/Hz)";
            chart2.Series[0].Name = "Power Spectral Density (W/Hz)";
            chart3.Series[0].Name = "Amplitude (W)";
            chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        }
        private void resize()
        {
            if (Size.Width > 0 && Size.Height > 110)
            {
                chart1.Size = new Size(Size.Width-80, Size.Height - 110);
                chart2.Size = chart1.Size;
                chart3.Size = chart1.Size;
                NoteNamesListBox.Location = new Point(Size.Width - 70, 30);
                Point start = new Point(12, Size.Height - 100);
                Button[] buttons = new Button[] {button2, button1, button3, button4, button5 };
                
                for(int i = 0; i < buttons.Length;i++)
                {
                    buttons[i].Location = new Point(start.X + i * 85, start.Y);
                }
                groupBox1.Location = new Point(442, Size.Height - 110);
                groupBox2.Location = new Point(537, Size.Height - 110);
                wavNameLabel.Location = new Point(12, Size.Height - 55);
                chordNameLabel.Location = new Point(buttons[3].Location.X, Size.Height - 55);
            }

        }
        private void DrawToGraphs()
        {
            float largestNumInTransform = transform[1].SpectralDensity;
            float smallestNumInTransform = transform[1].SpectralDensity;

            for (int i = 2; i < transform.Length - 2; i++)
            {
                if (smallestNumInTransform > transform[i].SpectralDensity)
                {
                    smallestNumInTransform = transform[i].SpectralDensity;
                }
                if (largestNumInTransform < transform[i].SpectralDensity)
                {
                    largestNumInTransform = transform[i].SpectralDensity;
                }
            }
            chart1.ChartAreas[0].AxisY.Maximum = (largestNumInTransform * 1.1);
            //chart1.ChartAreas[0].AxisY.Minimum = (smallestNumInTransform) > 0 ? (smallestNumInTransform) : 0;
            //chart2.ChartAreas[0].AxisY.Minimum = Math.Log(smallestNumInTransform) > 0 ? (smallestNumInTransform) : 0;
            chart2.ChartAreas[0].AxisY.Maximum = 1.1*Math.Log10(largestNumInTransform) < 0 ? 100 : 1.1* Math.Log10(largestNumInTransform );

            for (int i = 0; i < transform.Length; i++)
            {
                chart1.Series[0].Points.AddXY(transform[i].Frequency, transform[i].SpectralDensity);
                chart2.Series[0].Points.AddXY(transform[i].Frequency, transform[i].LogSpectralDensity);
            }

            //AMPLITUDE
            for (int i = 0; i < wav.channelDataSize; i++)
            {
                chart3.Series[0].Points.AddXY(i / (float)wav.sampleRate, wav.averageOfChannels[i]);
            }

            //Diagnose Chord
            Tone[] Notes = IdentifyChord.FindSignificantTones(transform).ToArray();
            NoteNamesListBox.Items.Clear();
            for (int i = 0; i < Notes.Length; i++)
            {
                NoteNamesListBox.Items.Add(Notes[i].Note);
            }
            chordNameLabel.Text = "Chord: " + IdentifyChord.DiagnoseChord(transform);
            
        }
        private void button5_Click(object sender, EventArgs e) //Save WAV
        {
            if(wav != null)
            {
                SaveFileDialog slg = new SaveFileDialog //create a dialogue for saving
                {
                    Title = "Save WAV",
                    Filter = "WAV Files (*.wav)|*.wav"
                };

                if (slg.ShowDialog() == DialogResult.OK) //if dialogue result OK, save file.
                {
                    File.WriteAllBytes(slg.FileName, wav.RawData);
                    slg.Dispose();
                }
            }
            
            
        }
    }
}
