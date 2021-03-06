﻿using System;
using System.Collections.Generic;

namespace SpikeCore.Irc
{
    public interface IIrcClient
    {
        Action<PrivMessage> PrivMessageReceived { get; set; }
        Action<string> MessageReceived { get; set; }

        void Connect(string host, int port, string nickname, IEnumerable<string> channelsToJoin, bool authenticate, string password);
        void SendChannelMessage(string channelName, string message);
        void SendPrivateMessage(string nick, string message);
        void JoinChannel(string channelName);
        void PartChannel(string channelName, string reason);
        void Quit(string quitMessage);
    }
}
