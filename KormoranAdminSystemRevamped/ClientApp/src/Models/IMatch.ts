import IState from "./IState";
import ITeam from "./ITeam";

interface IMatch {
	matchId: number,
	state: IState,
	stateId: number,
	team1: ITeam,
	team1Id: number,
	team2: ITeam,
	team2Id: number,
	winner: ITeam,
	winnerId: number,
	team1Score: number,
	team2Score: number;
}

export default IMatch;