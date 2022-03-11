import IDiscipline from "./IDiscipline";
import IState from "./IState";
import ITeam from "./ITeam";

interface ITournament{
	id: number,
	name: string,
	disciplineId: number,
	discipline: IDiscipline | undefined,
	stateId: number,
	state: IState | undefined,
	teams: Array<ITeam> | undefined,
	tournamentType: string,
	tournamentTypeShort: string;
}

export default ITournament;