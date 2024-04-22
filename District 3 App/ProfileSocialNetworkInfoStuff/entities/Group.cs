using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using District_3_App.ExtraInfo;

namespace District_3_App.ProfileSocialNetworkInfoStuff.Entities
{
    public class Group : IComparable<Group>
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }

        public List<User> GroupMembers { get; set; }

        public Group()
        {
        }

        public Group(Guid id, string groupName, List<User> groupMembers)
        {
            this.Id = id;
            this.GroupName = groupName;
            this.GroupMembers = groupMembers;
        }

        public int CompareTo(Group other)
        {
            return this.GroupName.CompareTo(other.GroupName);
        }
    }
}
