using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace test.Models
{
    [Keyless]
    public partial class Action
    {
        public Action(string actionId, string userlogin, string userdetails,
            string application, DateTime dateTime, string actionType, string actionObjectId, string actionObjectType, string extraInformation) {
            ActionId = actionId;
            UserLogin = userlogin;
            UserDetails = userdetails;
            //IPSite = ipsite;
            Application = application;
            Date_Time = dateTime;
            ActionType = actionType;
            ActionObjectID = actionObjectId;
            ActionObjectType = actionObjectType;
            ExtraInformation = extraInformation;
        }
        public Action() { }

        
        public string? ActionId { get; set; }
        public string? UserLogin { get; set; }
        public string? UserDetails { get; set; }
        //public string? IPSite { get; set; }
        public string? Application { get; set; }
        public DateTime? Date_Time { get; set; }
        public string? ActionType { get; set; }
        public string? ActionObjectID { get; set; }
        public string? ActionObjectType { get; set; }
        public string? ExtraInformation { get; set; }
        
    }
}
