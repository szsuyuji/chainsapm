﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainsAPM.Commands.Agent
{
    public class FunctionTailQuick : Interfaces.ICommand<byte>
    {
        public long FunctionID { get; set; }
        public long ThreadID { get; set; }

        public DateTime TimeStamp { get; set; }

        public FunctionTailQuick(long functionid, long threadid, long timestamp)
        {
            FunctionID = functionid;
            ThreadID = threadid;
            TimeStamp = DateTime.FromFileTimeUtc(timestamp);
        }
        public string Name
        {
            get { return "Function Tail Quick"; }
        }
        public ushort Code
        {
            get { return 0x001A; }
        }
        public string Description
        {
            get { return "Event that represents a quick function enter--meaning there were no paramters captured by the agent."; }
        }
        public Type CommandType
        {
            get { return typeof(string); }
        }
        public Interfaces.ICommand<byte> Decode(ArraySegment<byte> input)
        {
            if (input.Count != 0)
            {
                Helpers.ArraySegmentStream segstream = new Helpers.ArraySegmentStream(input);
                int size = segstream.GetInt32();
                if (input.Count == size)
                {
                    short code = segstream.GetInt16();
                    if (code == Code)
                    {
                        var timestamp = segstream.GetInt64();
                        var function = segstream.GetInt64();
                        var thread = segstream.GetInt64();
                        var term = segstream.GetInt16();
                        if (term != 0)
                        {
                            throw new System.Runtime.Serialization.SerializationException("Terminator is a non zero value. Please check the incoming byte stream for possible errors.");
                        }
                        return new FunctionTailQuick(function, thread, timestamp);
                    }
                    else
                    {
                        throw new System.Runtime.Serialization.SerializationException("Invalid command code detected. Please check the incoming byte stream for possible errors.");
                    }
                }
                else
                {
                    throw new System.Runtime.Serialization.SerializationException("Size of message does not match size of byte stream. Please check the incoming byte stream for possible errors.");
                }
            }
            else
            {
                throw new System.Runtime.Serialization.SerializationException("Size of message is zero. Please check the incoming byte stream for possible errors. ");
            }
        }
        public byte[] Encode()
        {
            var buffer = new List<byte>(23);
            buffer.AddRange(BitConverter.GetBytes(23)); // 4 bytes for size, 2 byte for code, 8 bytes for data, 8 bytes for data, 2 bytes for term
            buffer.AddRange(BitConverter.GetBytes(Code));
            buffer.AddRange(BitConverter.GetBytes(FunctionID));
            buffer.AddRange(BitConverter.GetBytes(ThreadID));// 4 bytes for size, 1 byte for code, 8 bytes for data, 2 bytes for term
            buffer.AddRange(BitConverter.GetBytes((short)0));
            return buffer.ToArray();

        }
    }
}
