﻿using Emgu.CV;
using System;
using System.Diagnostics;
using System.IO;
using EmguFFmpeg.EmguCV;
using FFmpeg.AutoGen;
using System.Runtime.InteropServices;

namespace EmguFFmpeg.Example
{
    internal class Program
    {
        private unsafe static void Main(string[] args)
        {
            Process.Start(Environment.CurrentDirectory);
            FFmpegHelper.RegisterBinaries(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "ffmpeg"));
            FFmpegHelper.SetupLogging(logWrite: _ => Trace.Write(_));
            Console.WriteLine("Hello FFmpeg!");

            //PngChromekeyFilter filter = new PngChromekeyFilter(@"C:\Users\IOL0ol1\Desktop\input.png", @"output.png");
            //Remuxing filter = new Remuxing(@"C:\Users\Admin\Videos\Desktop\input.mp4");
            //DecodeAudioToMat decodeAudio = new DecodeAudioToMat(@"C:\Users\Admin\Desktop\input.mp3");
            //BitmapConverter bitmap = new BitmapConverter();
            //EncodeAudioByMat encodeAudio = new EncodeAudioByMat("output.mp3");
            //EncodeVideoByMat video = new EncodeVideoByMat("output.mp4", 800, 600, 1);
            //DecodeVideoToMat videoToImage = new DecodeVideoToMat(@"C:\Users\Admin\Videos\Desktop\input.mp4", "image");
            DecodeVideoWithCustomCodecScaledToMat videoScaledToMat =
                new DecodeVideoWithCustomCodecScaledToMat(@"C:\t\x.mp4", "image");            //DecodeVideoToMat videoToImage = new DecodeVideoToMat(@"C:\Users\Admin\Desktop\out9.webm", "image");

            Console.ReadKey();
        }
    }
}