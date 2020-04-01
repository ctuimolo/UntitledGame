using System;
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using NAudio.Vorbis;

namespace NAudioPipelineLoader
{
    [ContentImporter(".ogg", DisplayName = "VorbisWaveReader Importer", DefaultProcessor = "VorbisWaveReaderProcessor")]

    public class VorbisWaveReaderImporter : ContentImporter<string>
    {
        public override string Import(string filename, ContentImporterContext context)
        {
            try
            {
                return filename;
            }
            catch (Exception ex)
            {
                context.Logger.LogMessage("Error : {0}", ex);
                throw;
            }
        }
    }
}
