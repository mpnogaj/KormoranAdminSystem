import IState from "./IState";
import ITeam from "./ITeam";

interface IMatch {
	matchId: number,
	state: IState,
	team1: ITeam,
	team2: ITeam
	winner: ITeam,
	team1Score: number,
	team2Score: number;
}

export default IMatch;