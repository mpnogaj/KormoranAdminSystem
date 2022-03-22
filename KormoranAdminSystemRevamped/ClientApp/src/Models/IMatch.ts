import IState from "./IState";
import ITeam from "./ITeam";

interface IMatch {
	matchId: number,
	state: IState | undefined,
	stateId: number,
	team1: ITeam | undefined,
	team1Id: number,
	team2: ITeam | undefined,
	team2Id: number,
	winner: ITeam | undefined,
	winnerId: number,
	team1Score: number,
	team2Score: number;
}

export default IMatch;