namespace PAL
{
    using System;
    using System.Runtime.CompilerServices;

    public class SalesAdminPAL
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public string ImageName { get; set; }
        public DateTime CreatedOn { get; set; }        
    }
}

