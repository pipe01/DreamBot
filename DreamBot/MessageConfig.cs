using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DreamBot
{
    public class MessageConfig
    {
        public string NoPermission { get; set; }
        public string KickMsg { get; set; }
        public string KickErrArgs { get; set; }
        public string KickErrNoUser { get; set; }
        public string BanMsg { get; set; }
        public string BanErrArgs { get; set; }
        public string BanErrNoUser { get; set; }
        public string BanErrUserBanned { get; set; }
        public string PruneErr { get; set; }
        public string PruneSuc { get; set; }
        public string UPruneErr { get; set; }
        public string UPruneErrUser { get; set; }
        public string UPruneSuc { get; set; }
        public string VerifyErr { get; set; }
        public string VerifySuc { get; set; }
        public string UnverifyErr { get; set; }
        public string UnverifySuc { get; set; }
        public string VerificationsOff { get; set; }
        public string InviteMsg { get; set; }
        public string IdMsg { get; set; }
        public string VerificationsEnabled { get; set; }
        public string VerificationsDisabled { get; set; }
        public string ErrSelf { get; set; }
    }
}
