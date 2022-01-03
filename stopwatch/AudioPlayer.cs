using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace stopwatch
{
    public class AudioPlayer
    {

        private bool selfdestruct;

        private SoundPlayer soundPlayer;

        public AudioPlayer(bool selfdestruct)
        {
            this.selfdestruct = selfdestruct;
            soundPlayer = new SoundPlayer();
        }

        /// <summary>
        /// Gets the wav file path
        /// </summary>
        /// <returns>wav file path</returns>
        string GetWavFilePath()
        {
            string projectFolder = Directory.GetCurrentDirectory();

            string resourceFolder = @"\Resources";

            string songPath;
            if (selfdestruct)
            {
                songPath = @"\Curse-you-Perry-the-Platypus.wav";
            }
            else
            {
                songPath = @"\Doofenshmirtz Evil Inc.wav";
            }

            return projectFolder + resourceFolder + songPath;
        }

        /// <summary>
        /// Plays a wav file
        /// </summary>
        public void PlaySound()
        {
            soundPlayer.SoundLocation = GetWavFilePath();
            soundPlayer.PlaySync();
        }
    }
}
