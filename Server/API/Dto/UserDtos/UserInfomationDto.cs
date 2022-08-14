using System;

namespace API.Dto.UserDtos
{
    public class UserInfomationDto
    {
        public UserInfomationDto()
        {
        }

        public UserInfomationDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id {set; get;}
        public string Name {set; get;}
    }
}