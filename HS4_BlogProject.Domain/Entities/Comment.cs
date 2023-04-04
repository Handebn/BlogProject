using HS4_BlogProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Domain.Entities
{
    public class Comment:IBaseEntity 
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        //Navigation
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

    }
}
