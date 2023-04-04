using HS4_BlogProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Domain.Entities
{
    public class Genre : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Description { get; set; }

        // IBaseEntity 'den geliyor.
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        public List<Post> Posts { get; set; }
    }
}
