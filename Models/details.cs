using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapiEMS.Models
{
    public class details
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string IDProofType { get; set; }
        public string IDProofNumber { get; set; }
        public string Phone { get; set; }
        public string BloodGroup { get; set; }
        public string emailID { get; set; }
        public string EmpAddress { get; set; }
    }
}