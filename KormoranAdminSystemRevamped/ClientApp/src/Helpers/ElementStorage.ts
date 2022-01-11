import axios from "axios";
import IDiscipline from "../Models/IDiscipline";
import ILog from "../Models/ILog";
import IMatch from "../Models/IMatch";
import IState from "../Models/IState";
import ITeam from "../Models/ITeam";
import ITournament from "../Models/ITournament";
import ICollectionResponse from "../Models/Responses/ICollectionResponse";

class StorageElementBasic{
	callbacks: Array<Callback> = [];
	activeCallbacks: number = 0;
	itemKey: string = "";
	apiGetUrl: string = ""; 
	plainData: Array<any> = [];
	constructor(key: string, apiUrl: string){
		this.itemKey = key;
		this.apiGetUrl = apiUrl;
	}
}

class StorageElement<T> extends StorageElementBasic{
	public getData() : Array<T> {
		return this.plainData as Array<T>;
	}
}

class Callback{
	private callback: Function;
	private isPaused: boolean = false;
	readonly targetKey: string;

	constructor(callback: Function, targetKey: string){
		this.callback = callback;
		this.targetKey = targetKey;
	}

	public getIsPaused() : boolean{
		return this.isPaused;
	}

	public togglePause(){
		this.isPaused = !this.isPaused;
		const storage = ElementStorage.getInstance();
		storage.elements.filter(x => x.itemKey == this.targetKey);
		storage.elements[0].activeCallbacks += this.isPaused ? 1 : -1;
	}

	public run() {
		this.callback();
	}
}

class ElementStorage{

	elements: Array<StorageElementBasic> = [
		new StorageElement<IDiscipline>("DISCIPLINES", "/api/tournaments/GetDisciplines"),
		new StorageElement<IState>("STATES", "/api/tournaments/GetDisciplines"),
		new StorageElement<ITournament>("TOURNAMENTS", "/api/tournaments/GetDisciplines"),
		new StorageElement<IMatch>("MATCHES", "/api/tournaments/GetDisciplines"),
		new StorageElement<ILog>("LOGS", "/api/tournaments/GetDisciplines"),
		new StorageElement<ITeam>("TEAMS", "/api/tournaments/GetDisciplines"),
	]
	private readonly interval: number = 10;
	private static instance: ElementStorage;
	private timerId: number = 0;

	public static getInstance() {
		if(!ElementStorage.instance){
			ElementStorage.instance = new ElementStorage();
		}
		return ElementStorage.instance;
	}

	public subscribe(callback: Callback) {
		var element = this.elements.filter(x => x.itemKey.toUpperCase() == callback.targetKey.toUpperCase());
		if(element.length == 0){
			console.error("Element with key: " + callback.targetKey + " hasn't been found");
			return;
		}
		element[0].callbacks.push(callback);
	}

	public unsubscribe(callback: Callback) {
		var element = this.elements.filter(x => x.itemKey.toUpperCase() == callback.targetKey.toUpperCase());
		if(element.length == 0){
			console.error("Element with key: " + callback.targetKey + " hasn't been found");
			return;
		}
		element[0].callbacks.splice(element[0].callbacks.indexOf(callback), 1);
	}

	private constructor(){}

	public startUpdating(){
		this.timerId = window.setInterval(async () => this.updateData, this.interval * 100);
	}

	public stopUpdating(){
		window.clearInterval(this.timerId);
	}

	private async updateData(){
		this.elements.forEach(async (element) =>  {
			if(element.activeCallbacks == 0) return;
			const response = await axios.get<ICollectionResponse<any>>(element.apiGetUrl);
			if(response.data.collection != element.plainData){
				element.plainData = response.data.collection;
				element.callbacks.forEach(callback => callback.run());
			}
		})
	}
}

export
{
	ElementStorage,
	Callback
};
