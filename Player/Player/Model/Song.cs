﻿using Player.Controller;
using Player.Infrastructure;
using Player.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Player.Domain
{
    class Song
    {
        public string Singer { get; private set; }
        public string Title { get; private set; }
        public string Lyrics { get; private set; }

        public Song(string path)
        {
            TagLib.File audioFile = null;
            try
            {
                audioFile = TagLib.File.Create(path);
            }
            catch (TagLib.CorruptFileException)
            {
                MainWindowController.ShowSystemMessage("Файл поврежден");
                return;
            }
            string temp_singer = String.Join(", ", audioFile.Tag.Performers);
            string temp_title = audioFile.Tag.Title;
            if (temp_singer == "" || temp_singer == null ||
                temp_title == "" || temp_title == null)
            {
                RepairFile.WriteSongData(path, audioFile);
            }
            Singer = String.Join(", ", audioFile.Tag.Performers);
            Title = audioFile.Tag.Title;
            string temp_lyrics = audioFile.Tag.Lyrics;
            if (temp_lyrics == null || temp_lyrics == "")
            {
                RepairFile.WriteLyrics(Singer, Title, audioFile);
            }
            Lyrics = audioFile.Tag.Lyrics;
        }
    }
}
