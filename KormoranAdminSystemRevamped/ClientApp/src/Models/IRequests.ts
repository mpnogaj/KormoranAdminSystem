import IMatch from "./IMatch";
import ITeam from "./ITeam";

interface IGetMatchesRequest {
	tornamentId: number;
}

interface ILogsParams {
	sessionId: string;
}

interface IUpdateTournamentBasicRequest {
	tournamentId: number,
	newName: string,
	newStateId: number,
	newDisciplineId: number
}

interface IUpdateTournamentRequest extends IUpdateTournamentBasicRequest{
	teams: Array<ITeam>,
	matches: Array<IMatch>
}

export type {
	IGetMatchesRequest,
	ILogsParams,
	IUpdateTournamentBasicRequest,
	IUpdateTournamentRequest
};