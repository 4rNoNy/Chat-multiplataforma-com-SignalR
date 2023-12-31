﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ReactMXHApi6.Core.Interfaces;
using ReactMXHApi6.Dtos;
using ReactMXHApi6.Extensions;

namespace ReactMXHApi6.SignalR
{
    [Authorize]
    public class PresenceHub : Hub
    {
        private readonly PresenceTracker _tracker;
        private readonly IUnitOfWork _unitOfWork;

        public PresenceHub(PresenceTracker tracker, IUnitOfWork unitOfWork)
        {
            _tracker = tracker;
            _unitOfWork = unitOfWork;
        }

        // Chamado quando um cliente se conecta ao hub
        public override async Task OnConnectedAsync()
        {
            var username = Context.User.GetUsername();
            var isOnline = await _tracker.UserConnected(username, Context.ConnectionId);
            if (isOnline)
            {
                var user = await _unitOfWork.UserRepository.GetMemberAsync(username);
                await Clients.Others.SendAsync("UserIsOnline", user);
            }

            var currentUsers = await _tracker.GetOnlineUsers();
            var usersOnline = await _unitOfWork.UserRepository.GetUsersOnlineAsync(username, currentUsers);
            await Clients.Caller.SendAsync("GetOnlineUsers", usersOnline);
        }

        // Chamado quando um cliente é desconectado do hub
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var username = Context.User.GetUsername();
            var isOffline = await _tracker.UserDisconnected(username, Context.ConnectionId);
            if (isOffline)
            {
                await Clients.Others.SendAsync("UserIsOffline", username);
            }

            await base.OnDisconnectedAsync(exception);
        }

        // Atualiza as informações do usuário associadas ao objeto UserPeer
        public async Task UpdateUserPeer(UserPeer userPeer)
        {
            var user = await _unitOfWork.UserRepository.GetMemberAsync(Context.User.GetUsername());
            userPeer.Member = user;
            await Clients.All.SendAsync("OnUpdateUserPeer", userPeer);
        }

        // Chama um usuário específico através do seu nome de usuário e exibe informações para o chamador
        public async Task CallToUsername(string otherUsername, string channelName)
        {
            var currentUsername = Context.User.GetUsername();

            var connections = await _tracker.GetConnectionsForUser(otherUsername);
            if (connections != null)
            {
                var userCalling = await _unitOfWork.UserRepository.GetMemberAsync(currentUsername);
                await Clients.Clients(connections).SendAsync("DisplayInformationCaller", userCalling, channelName);
            }
        }
    }
}
