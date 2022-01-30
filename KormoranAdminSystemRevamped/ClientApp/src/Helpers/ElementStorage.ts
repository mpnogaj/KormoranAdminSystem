import axios from "axios";
import { TypeOfTag } from "typescript";
import ICollectionResponse from "../Models/Responses/ICollectionResponse";
import IObjectResponse from "../Models/Responses/IObjectResponse";

class StorageElement {
	callbacks: Array<Callback> = [];
	activeCallbacks: number = 0;

	plainData: any;
	isError: boolean = true;
	private apiGetUrl: string = "";
	readonly dataIsArray: boolean;

	constructor(apiUrl: string, dataIsArray: boolean = true, urlParams: object | undefined = undefined) {
		this.apiGetUrl = apiUrl;
		if(urlParams != undefined) this.updateParams(urlParams)
		this.dataIsArray = dataIsArray;
	}

	public getData<T>(): DataContainer<T> {
		return new DataContainer<T>(
			this.plainData as T,
			this.isError
		);
	}

	public updateParams(newParams: object){
		if(this.apiGetUrl.indexOf('?') != -1) 
			this.apiGetUrl = this.apiGetUrl.slice(0, this.apiGetUrl.indexOf('?'));
		if(Object.entries(newParams).length == 0) return;
		this.apiGetUrl += "?";
		Object.entries(newParams).forEach(keyVal => {
			var key = keyVal.at(0);
			var val = keyVal.at(1);
			this.apiGetUrl += key + "=" + val + "&";
		})
		this.apiGetUrl = 
			this.apiGetUrl.slice(0, this.apiGetUrl.length - 1);
	}

	public getApiUrl() : string{
		return this.apiGetUrl;
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
			new StorageElement("/api/Logs/GetLogs")
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

	public updateParams(newParams: object, target: StorageTarget){
		const element = this.elements.get(target);
		if (element == undefined) {
			console.error("Element with key: " + target + " hasn't been found");
			return;
		}
		element.updateParams(newParams);
	}

	public async subscribe(callback: Callback) {
		const element = this.elements.get(callback.targetKey)
		if (element == undefined) {
			console.error("Element with key: " + callback.targetKey + " hasn't been found");
			return;
		}
		element.activeCallbacks++;
		element.callbacks.push(callback);
		await this.fetchAndSetData(element);
		callback.run();
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

	private async fetchAndSetData(element: StorageElement) {
		var data: any;
		if (element.dataIsArray) {
			const response = await axios.get<ICollectionResponse<any>>(element.getApiUrl());
			data = response.data.collection;
		}
		else {
			const response = await axios.get<IObjectResponse<any>>(element.getApiUrl());
			data = response.data.item;
		}
		if (data != element.plainData) {
			element.plainData = data;
			element.isError = false;
			element.callbacks.forEach(callback => callback.run());
		}
	}

	private async updateData() {
		this.elements.forEach(async (element) => {
			if (element.activeCallbacks == 0) return;
			await this.fetchAndSetData(element);
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
