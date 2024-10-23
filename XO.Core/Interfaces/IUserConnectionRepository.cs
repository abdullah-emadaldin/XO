﻿
using ReposatoryPatternWithUOW.Core.Interfaces;
using ReposatoryPatternWithUOW.Core.Models;

namespace XO.EF.Reposatories
{
    public interface IUserConnectionRepository : IBaseRepository<UserConnection>
    {
        Task<int> SetIsPlaying(string connectionid);
        Task<int> SetIsPlayingFalse(string connectionid);
        //Task<int> SetWaiting(string connectionid);
        //Task<int> SetIsWaitingFalse(string connectionid);
    }
}