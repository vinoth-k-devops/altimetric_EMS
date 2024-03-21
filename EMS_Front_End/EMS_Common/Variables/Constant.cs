
namespace EMS_Common.Variables
{
    public enum Alerts
    {
        Success,
        Danger,
        Info,
        Warning
    }
	public static class Constant
	{
        public static string TITLE = "Election Management System";

        public static string __USERNAME__ = "_uname_";
        public static string __TOKEN__ = "_token_";
        public static string __REFRESH_TOKEN__ = "_refreshToken_";

        public static string StateADD = @"MasterData/AddState";
        public static string StateUPDATE = @"MasterData/UpdateState";
        public static string StateGetAll = @"MasterData/GetStateAll";
        public static string StateGetById = @"MasterData/GetStateById/";

        public static string CityADD = @"MasterData/AddCity";
        public static string CityUPDATE = @"MasterData/UpdateCity";
        public static string CityGetAll = @"MasterData/GetCityAll";
        public static string CityGetById = @"MasterData/GetCityById/";

        public static string SymbolADD = @"MasterData/AddSymbol";
        public static string SymbolUPDATE = @"MasterData/UpdateSymbol";
        public static string SymbolGetAll = @"MasterData/GetSymbolAll";
        public static string SymbolGetById = @"MasterData/GetSymbolById/";

        public static string PartyADD = @"MasterData/AddParty";
        public static string PartyUPDATE = @"MasterData/UpdateParty";
        public static string PartyGetAll = @"MasterData/GetPartiesAll";
        public static string PartyGetById = @"MasterData/GetPartyById/";

        public static string AuthLogin = @"Auth/Login";
        public static string AuthRefreshToken = @"Auth/refresh";
        public static string AuthLogOut = @"Auth/Revoke";

        public static string AddUser = @"MasterData/AddUser";

        public static string GetDDSymbol = @"Transaction/GetActiveSymbols";
        public static string GetDDState = @"Transaction/GetActiveState";
        public static string GetDDCity = @"Transaction/GetActiveCities/";
        public static string GetDashboard = @"Transaction/GetDashboard";
        public static string ActiveUser = @"Transaction/Approve";

        public static string GetElection = @"Transaction/GetElection";
        public static string AddElection = @"Transaction/AddElection";
        public static string UpdateElection = @"Transaction/UpdateElection";
        public static string GetElectionById = @"Transaction/GetElectionById/";
        public static string GetActiveElectionList = @"Transaction/GetActiveElectionList";
        public static string GetCompletedorInProgressElectionList = @"Transaction/GetCompletedorInProgressElectionList";
        public static string GetElectionResult = @"Transaction/GetElectionResult";

        public static string GetMPSeat = @"Transaction/GetMPSeatList";
        public static string AddMPSeat = @"Transaction/AddMPSeat";
        public static string UpdateMPSeat = @"Transaction/UpdateMPSeat";
        public static string GetMPSeatyId = @"Transaction/GetMPSeatById/";

        public static string GetCanditure = @"Transaction/GetCanditureList";
        public static string AddCanditure = @"Transaction/AddCanditure";
        public static string UpdateCanditure = @"Transaction/UpdateCanditure";
        public static string GetCanditureById = @"Transaction/GetCanditureById/";

        public static string GetUserDashboard = @"Transaction/GetUserDashboard/";
        public static string RIGHT_TO_VOTE = @"Transaction/RIGHTTOVOTE";

        public static string GetActiveUsers = "Transaction/GetActiveUsers";

        public static string DateTimeFormat = @"{0:yyyy-MM-ddTHH:mm:ss}";

        public static string ShowAlert(Alerts obj, string message)
        {
            string alertDiv = "";
            string alertTemplate = @"<div id=""alertPP"" class=""alert alert-{2} alert-dismissible fade show"" role=""alert""><strong>{0}</strong> {1} ";
            alertTemplate += @"<button type=""button"" class=""close"" data-dismiss=""alert"" onclick=""ClosePP()"" aria-label=""Close""><span aria-hidden=""true"">&times;</span>";
            alertTemplate += @"</button></div>";

            switch (obj)
            {
                case Alerts.Success:
                    alertDiv = alertTemplate.Replace("{0}", "Success !").Replace("{1}", message).Replace("{2}", "success");                    
                    break;
                case Alerts.Danger:
                    alertDiv = alertTemplate.Replace("{0}", "Error !").Replace("{1}", message).Replace("{2}", "error");                    
                    break;
                case Alerts.Info:
                    alertDiv = alertTemplate.Replace("{0}", "Info !").Replace("{1}", message).Replace("{2}", "info");
                    break;
                case Alerts.Warning:
                    alertDiv = alertTemplate.Replace("{0}", "Warning !").Replace("{1}", message).Replace("{2}", "warning");
                    break;
            }
            return alertDiv;
        }

        public static string RoleClaimType = @"http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        public static string SurNameClaimType = @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
        public static string NameClaimType = @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

        public static string AType = @"A";
        public static string UType = @"U";
    }
    public class Dashboard
    {
        public string total_user_count { get; set; } = string.Empty;

        public string parties_count { get; set; } = string.Empty;

        public string candidate_count { get; set; } = string.Empty;

        public string awaiting_approval_count { get; set; } = string.Empty;

        public List<e_user>? approval_user { get; set; }

        public ApproveUser? activate { get; set; }
    }
    public class e_user
    {
        public int election_user_id { get; set; }
        public string election_voter_id { get; set; } = string.Empty;
        public string election_voter_name { get; set; } = string.Empty;
        public string election_state_name { get; set; } = string.Empty;
        public string election_city_name { get; set; } = string.Empty;
    }
    public class ApproveUser
    {
        public int election_user_id { get; set; }
    }
}

