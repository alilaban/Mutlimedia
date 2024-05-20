using NAudio.Wave;

namespace alaaali
{
    public class AudioPlayer
    {
        private IWavePlayer wavePlayer;
        private WaveStream waveStream;

        public void Play(string filePath)
        {
            wavePlayer = new WaveOutEvent();
            waveStream = new WaveFileReader(filePath);
            wavePlayer.Init(waveStream);
            wavePlayer.Play();
        }

        public void Stop()
        {
            wavePlayer?.Stop();
            waveStream?.Dispose();
            wavePlayer?.Dispose();
        }
    }
}