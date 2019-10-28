﻿using FFmpeg.AutoGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmguFFmpeg.Example
{
    public class Remuxing : IExample
    {
        private string input;
        private string output;

        public Remuxing(string inputMedia)
        {
            input = inputMedia;
            output = Path.GetFileNameWithoutExtension(input) + "_remuxing" + Path.GetExtension(input);
        }

        public unsafe void Start()
        {
            using (MediaReader reader = new MediaReader(input))
            using (MediaWriter writer = new MediaWriter(output))
            {
                // add stream with reader's codec_id
                for (int i = 0; i < reader.Count; i++)
                {
                    writer.AddStream(MediaEncode.CreateEncode(reader[i].Codec.Id, writer.Format.Flags));
                    // TODO: remove unsafe code
                    ffmpeg.avcodec_parameters_copy(writer[i].Stream.codecpar, reader[i].Stream.codecpar);
                    writer[i].Stream.codecpar->codec_tag = 0;
                }
                writer.Initialize();

                // read and write packet
                foreach (var packet in reader.Packets)
                {
                    int index = packet.StreamIndex;
                    AVRounding rounding = AVRounding.AV_ROUND_NEAR_INF | AVRounding.AV_ROUND_PASS_MINMAX;
                    AVRational inTimeBase = reader[index].TimeBase;
                    AVRational outTimeBase = writer[index].TimeBase;
                    packet.Pts = ffmpeg.av_rescale_q_rnd(packet.Pts, inTimeBase, outTimeBase, rounding);
                    packet.Dts = ffmpeg.av_rescale_q_rnd(packet.Dts, inTimeBase, outTimeBase, rounding);
                    packet.Duration = ffmpeg.av_rescale_q(packet.Duration, inTimeBase, outTimeBase);
                    packet.Pos = -1;
                    writer.WritePacket(packet);
                }
            }
        }
    }
}