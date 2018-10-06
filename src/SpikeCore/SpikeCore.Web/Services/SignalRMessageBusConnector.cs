﻿using System.Threading;
using System.Threading.Tasks;

using Foundatio.Messaging;

using Microsoft.AspNetCore.SignalR;

using SpikeCore.MessageBus;
using SpikeCore.Web.Hubs;

namespace SpikeCore.Web.Services
{
    public class SignalRMessageBusConnector : ISignalRMessageBusConnector, IMessageHandler<IrcReceiveMessage>
    {
        private readonly IHubContext<TestHub> _hubContext;
        private readonly IMessageBus _messageBus;

        public SignalRMessageBusConnector(IHubContext<TestHub> hubContext, IMessageBus messageBus)
        {
            _hubContext = hubContext;
            _messageBus = messageBus;
        }

        public async Task ConnectAsync()
            => await _messageBus.PublishAsync(new IrcConnectMessage());

        public async Task HandleMessageAsync(IrcReceiveMessage message, CancellationToken cancellationToken)
            => await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.Message, cancellationToken);

        // This should be "string channelName, string message" but UI work is being punted
        // until the React intergration.
        public async Task SendMessageAsync(string message)
            => await _messageBus.PublishAsync(new IrcSendChannelMessage("#spikelite", message));
    }
}