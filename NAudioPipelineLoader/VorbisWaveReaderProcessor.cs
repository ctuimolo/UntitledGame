using System;
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using NAudio.Vorbis;

namespace NAudioPipelineLoader
{
    [ContentProcessor(DisplayName = "VorbisWaveReader Processor")]
    public class VorbisWaveReaderProcessor : ContentProcessor<string, string>
    {
        public override string Process(string oggFilePath, ContentProcessorContext context)
        {
            try
            {
                return oggFilePath;
            }
            catch (Exception ex)
            {
                context.Logger.LogMessage("Error : {0}", ex);
                throw;
            }
        }
    }
}