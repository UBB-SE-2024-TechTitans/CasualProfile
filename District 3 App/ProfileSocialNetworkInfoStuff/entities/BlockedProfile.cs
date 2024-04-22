using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using District_3_App.ExtraInfo;

namespace District_3_App.ProfileSocialNetworkInfoStuff.Entities
{
    public class BlockedProfile : IComparable<BlockedProfile>
    {
        public User User { get; set; }
        public DateTime BlockDate { get; set; }

        public BlockedProfile()
        {
        }

        public BlockedProfile(User user, DateTime date)
        {
            this.User = user;
            this.BlockDate = date;
        }

        public string DateToString()
        {
            return BlockDate.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public int CompareTo(BlockedProfile other)
        {
            return this.BlockDate.CompareTo(other.BlockDate);
        }
    }
}
