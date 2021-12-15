import IDiscipline from "./IDiscipline";
import IState from "./IState";
import ITeam from "./ITeam";

interface ITournament{
	id: number,
	name: string,
	discipline: IDiscipline,
	state: IState,
	teams: Array<ITeam>,
	tournamentType: string,
	tournamentTypeShort: string;
}

export default ITournament;