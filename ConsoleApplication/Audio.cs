using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using System.IO;
using NAudio.Wave;

namespace ConsoleApplication
{
    
    public class Audio 
    {

        public void Play()
        {
            MemoryStream audioData = new MemoryStream(
                File.ReadAllBytes(@"Content/"+"music.mp3"));


            /* Wave SampleRate 44100, and 2 Channels(Stereo) */
            WaveFormat waveFormat = new WaveFormat(44100, 2);
            RawSourceWaveStream waveStream = new RawSourceWaveStream(audioData,
                waveFormat);
            WaveOut waveOut = new WaveOut();
            waveOut.DeviceNumber = AudioController.getInstance()
                .GetDefaultOutputDeviceNumber();
            waveOut.Init(waveStream);
            waveOut.Play();
        }
    }

    
}
