using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNetCore.Identity;

namespace BreakableLime.Repository.Models
{
    public class ApplicationIdentityUser : IdentityUser
    {
        private uint _assignedPort;
        
        public string Class { get; set; }
        public string TokenId { get; set; } = Guid.NewGuid().ToString();
        
        public IList<ContainerImage> OwnedImages { get; set; }
        
        public short ImageAllocation { get; set; }
        public short ContainerAllocation { get; set; }

        public uint AssignedPort
        {
            get => _assignedPort;
            set
            {
                if (value <= (uint)99999)
                    _assignedPort = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(AssignedPort));
            }
        }
    }
}