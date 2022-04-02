import IDiscipline from "./IDiscipline";
import IMatch from "./IMatch";
import IState from "./IState";
import ITeam from "./ITeam";

interface ITournament {
	id: number,
	name: string,
	discipline: IDiscipline,
	state: IState,
	matches: Array<IMatch>
	teams: Array<ITeam>,
}

export default ITournament;