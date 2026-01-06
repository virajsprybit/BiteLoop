namespace PAL
{
    using System;
    using System.Runtime.CompilerServices;

    public class UsersPAL
    {
        public long ID { get; set; }
        public string Name { get; set; }        
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string FacebookID{ get; set; }
        public string DeviceType { get; set; }
        public bool Status { get; set; }        
        public DateTime CreatedOn { get; set; }
        public string AuthToken { get; set; }

        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string StreetAddress { get; set; }
        public string Location { get; set; }

        public string VersionNumber { get; set; }
        public string AppType { get; set; }
        public string FirebaseKey { get; set; }
        public int? IsFacebookGoogleApple { get; set; }
        public string SocialMediaKey { get; set; }

    }
}

