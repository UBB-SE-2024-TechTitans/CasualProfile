using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace District_3_App.ProfileSocialNetworkInfoStuff.Entities
{
    public class CloseFriendProfile : IComparable<CloseFriendProfile>
    {
        public User User { get; set; }

        public DateTime CloseFriendedDate { get; set; }

        public CloseFriendProfile()
        {
        }

        public CloseFriendProfile(User user, DateTime closeFriendedDate)
        {
            this.User = user;
            this.CloseFriendedDate = closeFriendedDate;
        }

        public int CompareTo(CloseFriendProfile other)
        {
            return this.User.CompareTo(other.User);
        }
    }
}
