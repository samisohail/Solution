using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.CustomMappers
{
    internal static class UserMapping
    {
        public static UserDto MapToUserDto(DbEntities.User user)
        {
            var userDto = new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName ?? string.Empty,
                LastName = user.LastName,
                Email = user.Email,
                Street = user.Street ?? string.Empty,
                City = user.City ?? string.Empty,
                State = user.State ?? string.Empty,
                Country = user.Country ?? string.Empty
            };
            return userDto;
        }

        public static Domain.User.User MapToUser(DbEntities.User user)
        {
            var userDomain = new Domain.User.User
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName ?? string.Empty,
                LastName = user.LastName,
                Email = user.Email,
                Street = user.Street ?? string.Empty,
                City = user.City ?? string.Empty,
                State = user.State ?? string.Empty,
                Country = user.Country ?? string.Empty,
                PostCode= user.PostCode ?? string.Empty,
                Active = user.Active ?? false,
                CreatedOnUtc = user.CreatedOnUtc
            };
            return userDomain;
        }

        public static List<Domain.User.User> MapToUser(this List<DbEntities.User> users)
        {
            var usersDomainObject = new List<Domain.User.User>();
            foreach (var user in users)
            {
                usersDomainObject.Add(MapToUser(user));
            }
            return usersDomainObject;
        }

    }
}
