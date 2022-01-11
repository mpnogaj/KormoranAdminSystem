import axios from "axios";
import IDiscipline from "../Models/IDiscipline";
import ILog from "../Models/ILog";
import IMatch from "../Models/IMatch";
import IState from "../Models/IState";
import ITeam from "../Models/ITeam";
import ITournament from "../Models/ITournament";
import ICollectionResponse from "../Models/Responses/ICollectionResponse";

class StorageElement<T>{
	data: Array<T> = []
	callbacks: Array<Function> = []
}

class Storage{

	disciplines: StorageElement<IDiscipline> = new StorageElement();
	states: StorageElement<IState> = new StorageElement();
	teams: StorageElement<ITeam> = new StorageElement();
	tournaments: StorageElement<ITournament> = new StorageElement();
	matches: StorageElement<IMatch> = new StorageElement();
	logs: StorageElement<ILog> = new StorageElement();
	private static instance: Storage;

	public GetInstance() {
		if(!Storage.instance){
			Storage.instance = new Storage();
		}
		return Storage.instance;
	}

	public Subscribe(element: Storage.StorageElementType, callback: Function) {
		var callbacks: Array<Function>;
		switch(element){
			case Storage.StorageElementType.States:
				callbacks = this.states.callbacks;
				break;
			case Storage.StorageElementType.Disciplines:			
				callbacks = this.disciplines.callbacks;
				break;
			case Storage.StorageElementType.Tournaments:
				callbacks = this.tournaments.callbacks;
				break;
			case Storage.StorageElementType.Logs:
				callbacks = this.logs.callbacks;
				break;
			case Storage.StorageElementType.Matches:
				callbacks = this.matches.callbacks;
				break;
			case Storage.StorageElementType.Teams:
				callbacks = this.teams.callbacks;
				break;
			default:
				callbacks = [];
				break;
		}
		callbacks.push(callback);
	}

	public Unsubscribe(element: Storage.StorageElementType, callback: Function) {
		var callbacks: Array<Function>;
		switch(element){
			case Storage.StorageElementType.States:
				callbacks = this.states.callbacks;
				break;
			case Storage.StorageElementType.Disciplines:			
				callbacks = this.disciplines.callbacks;
				break;
			case Storage.StorageElementType.Tournaments:
				callbacks = this.tournaments.callbacks;
				break;
			case Storage.StorageElementType.Logs:
				callbacks = this.logs.callbacks;
				break;
			case Storage.StorageElementType.Matches:
				callbacks = this.matches.callbacks;
				break;
			case Storage.StorageElementType.Teams:
				callbacks = this.teams.callbacks;
				break;
			default:
				callbacks = [];
				break;
		}
		callbacks.splice(this.states.callbacks.indexOf(callback), 1);
	}

	private constructor(){}

	private async updateData(){
		if(this.states.callbacks.length > 0){
			const response = await axios.get<ICollectionResponse<IState>>("/api/...");
			if(response.data.collection != this.states.data){
				this.states.data = response.data.collection;
				this.states.callbacks.forEach(callback => callback());
			}
		}
		if(this.disciplines.callbacks.length > 0){

		}
	}
}

module Storage{
	export enum StorageElementType{
		States,
		Disciplines,
		Teams,
		Tournaments,
		Matches,
		Logs,
	}
}

export default Storage;