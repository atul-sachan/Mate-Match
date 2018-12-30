using AutoMapper;
using Mate_Match_API.DTOs;
using Mate_Match_API.Data.Models;

namespace Mate_Match_API.Helpers
{
    public class AgeResolver<T> : IValueResolver<User, T, int> where T: class
    {
        public int Resolve(User source, T destination, int destMember, ResolutionContext context)
        {
            return source.DateOfBirth.CalculateAge();
        }
    }
}