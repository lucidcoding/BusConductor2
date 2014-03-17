using System;
using BusConductor.Application.Requests;
using BusConductor.Domain.Common;

namespace BusConductor.Application.Contracts
{
    public interface IUserService
    {
        void EnsureExists(string userName);
    }
}
