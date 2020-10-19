using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSoftTest.Models
{
    public class NeoViewModel
    {
        public int Row_Id { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> CityId { get; set; }
        public string PanNumebr { get; set; }
        public string PassportNumber { get; set; }
        public byte[] ProfileImage { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
    }
}