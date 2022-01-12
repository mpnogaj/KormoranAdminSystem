import axios from "axios";
import ICollectionResponse from "../Models/Responses/ICollectionResponse";
import IObjectResponse from "../Models/Responses/IObjectResponse";

class StorageElement {
	callbacks: Array<Callback> = [];
	activeCallbacks: number = 0;

	plainData: any;
	isError: boolean = true;
	readonly apiGetUrl: string = "";
	readonly dataIsArray: boolean;

	constructor(apiUrl: string, dataIsArray: boolean = true) {
		this.apiGetUrl = apiUrl;
		this.dataIsArray = dataIsArray;
	}

	public getData<T>(): DataContainer<T> {
		return new DataContainer<T>(
			this.plainData as T,
			this.isError
		);
	}
}

class Callback {
	private callback: Function;
	private isPaused: boolean = false;
	readonly targetKey: StorageTarget;

	constructor(callback: Function, targetKey: StorageTarget) {
		this.callback = callback;
		this.targetKey = targetKey;
	}

	public getIsPaused(): boolean {
		return this.isPaused;
	}

	public togglePause() {
		this.isPaused = !this.isPaused;
		ElementStorage.getInstance().markPaused(this);
	}

	public run() {
		this.callback(this.targetKey);
	}
}

enum StorageTarget {
	TOURNAMENTS,
	MATCHES,
	TEAMS,
	LOGS,
	STATES,
	DISCIPLINES,
}

class DataContainer<Y>{
	readonly isError: boolean;
	readonly data: Y | undefined;

	constructor(data: Y, isError: boolean) {
		this.data = data;
		this.isError = isError;
	}
}

class ElementStorage {
	/*
		elements: Map<StorageTarget, StorageElement> = new Map([...])
		This doesn't work ^
		https://stackoverflow.com/questions/45223857/using-generics-in-es6-map-with-typescript
	*/
	private elements = new Map<StorageTarget, StorageElement>([
		[
			StorageTarget.TOURNAMENTS,
			new StorageElement("/api/tournaments/GetTournaments")
		], [
			StorageTarget.MATCHES,
			new StorageElement("/api/tournaments/GetMatches")
		], [
			StorageTarget.TEAMS,
			new StorageElement("/api/tournaments/GetTeams")
		], [
			StorageTarget.LOGS,
			new StorageElement("/api/tournaments/GetLogs")
		], [
			StorageTarget.DISCIPLINES,
			new StorageElement("/api/tournaments/GetDisciplines")
		], [
			StorageTarget.STATES,
			new StorageElement("/api/tournaments/GetStates")
		]
	]);
	/*
		jak polonez xd
		raz, dwa, TRZY, raz, dwa, TRZY...
	*/
	private readonly interval: number = 3;
	private static instance: ElementStorage;
	private timerId: number = -1;

	public static getInstance() {
		if (!ElementStorage.instance) {
			ElementStorage.instance = new ElementStorage();
		}
		return ElementStorage.instance;
	}

	public markPaused(callback: Callback) {
		const element = this.elements.get(callback.targetKey);
		if (element == undefined) {
			console.error("Element with key: " + callback.targetKey + " hasn't been found");
			return;
		}
		element.activeCallbacks += callback.getIsPaused() ? 1 : -1;
	}

	public getData<T>(target: StorageTarget): DataContainer<T> {
		const element = this.elements.get(target);
		return element!.getData<T>();
	}

	public subscribe(callback: Callback) {
		const element = this.elements.get(callback.targetKey)
		if (element == undefined) {
			console.error("Element with key: " + callback.targetKey + " hasn't been found");
			return;
		}
		element.activeCallbacks++;
		element.callbacks.push(callback);
	}

	public unsubscribe(callback: Callback) {
		const element = this.elements.get(callback.targetKey)
		if (element == undefined) {
			console.error("Element with key: " + callback.targetKey + " hasn't been found");
			return;
		}
		element.activeCallbacks--;
		element.callbacks.splice(element.callbacks.indexOf(callback), 1);
	}

	private constructor() {
		this.startUpdating();
	}

	public startUpdating() {
		if (this.timerId != -1) return;
		this.timerId = window.setInterval(() => this.updateData(), this.interval * 1000);
		console.log("Clock started: " + this.timerId);
	}

	public stopUpdating() {
		window.clearInterval(this.timerId);
		this.timerId = -1;
		console.log("Clock stopped: " + this.timerId);
	}

	private async updateData() {
		console.log("Storage update!")
		this.elements.forEach(async (element) => {
			if (element.activeCallbacks == 0) return;
			var data: any;
			if (element.dataIsArray) {
				const response = await axios.get<ICollectionResponse<any>>(element.apiGetUrl);
				data = response.data.collection;
			}
			else {
				const response = await axios.get<IObjectResponse<any>>(element.apiGetUrl);
				data = response.data.item;
			}
			if (data != element.plainData) {
				element.plainData = data;
				element.isError = false;
				element.callbacks.forEach(callback => callback.run());
			}
		})
	}
}

export {
	ElementStorage,
	Callback,
	DataContainer,
	StorageElement,
	StorageTarget
};
