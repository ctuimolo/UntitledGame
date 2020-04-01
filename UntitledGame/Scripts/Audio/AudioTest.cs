using System;

using NAudio.Wave;
using NAudio.Vorbis;

using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace UntitledGame.Audio
{
    public class AudioTest
    {
        public VorbisWaveReader oggStream;
        public WaveOutEvent     wavPlayer;
        
        public AudioTest()
        {
            //using (oggStream = new VorbisWaveReader(@"C:/Projects/UntitledGame/UntitledGame/Content/Audio/sample.ogg"))
            //using (wavPlayer = new WaveOutEvent())
            //{
            //    wavPlayer.Init(oggStream);
            //    wavPlayer.Volume = 0.5f;
            //    wavPlayer.Play();
            //}

            //MemoryStream oggFileMemory = new MemoryStream();
            //FileStream file = new FileStream(@"C:/Projects/UntitledGame/UntitledGame/Content/Audio/sample.ogg", FileMode.Open);
            //file.CopyTo(oggFileMemory);
            //file.Close();

            //String Sample2sFilename = Game.Assets.Load<string>("Audio/Sample2");

            //SoundEffect test = Game.Assets.Load<SoundEffect>("Audio/sample");
            //SoundEffectInstance testInstace = test.CreateInstance();
            //testInstace.IsLooped = true;
            //testInstace.Play();
        }
    }
}
