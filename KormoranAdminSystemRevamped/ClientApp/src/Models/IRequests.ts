import ITeam from "./ITeam";

export interface IGetMatchesRequest {
	tornamentId: number;
}

export interface ILogsParams {
	sessionId: string;
}

export interface IUpdateTournamentBasicRequest {
	tournamentId: number,
	newName: string,
	newStateId: number,
	newDisciplineId: number
}

export interface IUpdateMatchBasicRequest {
	matchId: number,
	tournamentId: number,
	stateId: number,
	team1: number,
	team2: number,
	team1Score: number,
	team2Score: number, 
}

export interface IUpdateTournamentRequest extends IUpdateTournamentBasicRequest{
	teams: Array<ITeam>,
	matches: Array<IUpdateMatchBasicRequest>
}