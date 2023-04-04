using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Application.Models.VMs
{
    /// <summary>
    /// Member arae için tüm makaleleri getirmek üzere hazırladık.
    /// </summary>
    public class GetPostsVM
    {
        #region Post Info
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreateDate { get; set; }

        #endregion

        #region Author Info // ctrl + K + S

        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string FullName => $"{AuthorFirstName} {AuthorLastName}";
        public string AuthorImagePath { get; set; }

        #endregion
    }
}
