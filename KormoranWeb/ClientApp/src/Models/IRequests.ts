import ITeam from "./ITeam";
import IUser from "./IUser";

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
	team2Score: number;
}

export interface IAddEditUser {
	id: number,
	fullname: string,
	login: string,
	password: string,
	isAdmin: boolean;
}

export const DEFAULT_ADD_EDIT_USER: IAddEditUser = {
	id: 0,
	fullname: "",
	login: "",
	password: "",
	isAdmin: false
};

export function addEditUserFromIUser(user: IUser): IAddEditUser {
	return {
		id: user.id,
		fullname: user.fullname,
		login: user.login,
		password: "",
		isAdmin: user.isAdmin
	};
}

export interface IUpdateTournamentRequest extends IUpdateTournamentBasicRequest {
	teams: Array<ITeam>,
	matches: Array<IUpdateMatchBasicRequest>;
}

export interface IGetLeaderboardsRequest {
	tournamentId: number;
}