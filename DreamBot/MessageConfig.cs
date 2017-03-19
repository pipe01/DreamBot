using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DreamBot
{
    class MessageConfig
    {
        public string NoPermission { get; set; }
        public string KickMsg { get; set; }
        public string BanMsg { get; set; }
        public string PruneErr { get; set; }
        public string VerifyErr { get; set; }
        public string VerifySuc { get; set; }
        public string UnverifyErr { get; set; }
        public string UnverifySuc { get; set; }
        public string VerificationsOff { get; set; }
        public string InviteMsg { get; set; }
        public string VerificationsEnabled { get; set; }
        public string VerificationsDisabled { get; set; }
        public string AddAdminErr { get; set; }
        public string AddAdminSuc { get; set; }
        public string RemAdminErr { get; set; }
        public string RemAdminSuc { get; set; }
    }
}
