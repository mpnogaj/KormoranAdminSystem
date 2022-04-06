using KormoranShared.Models.Requests.Matches;
using KormoranShared.Models.Responses;
using System.Collections.Generic;

namespace KormoranShared.Models.Requests.Tournaments
{

    public class UpdateTournamentRequestModel
    {
        public int TournamentId { get; set; }
        public string NewName { get; set; }
        public int NewStateId { get; set; }
        public int NewDisciplineId { get; set; }
    }

    public class TournamentFullUpdateRequestModel : UpdateTournamentRequestModel
    {
        public List<Team> Teams { get; set; }
        public List<UpdateMatchRequestModel> Matches { get; set; }
    }

    public class GetFullTournamentDataResponseModel : BasicResponse
    {
        public Tournament Tournament { get; set; }
        public List<Match> Matches { get; set; }
        public List<Team> Teams { get; set; }
        public List<State> States { get; set; }
        public List<Discipline> Disciplines { get; set; }
    }
}