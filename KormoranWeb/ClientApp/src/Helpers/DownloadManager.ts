/*
    Class that automaticly downloads data from endpoint every given timeout
*/

import axios from "axios";
import { IBasicResponse } from "../Models/IResponses";

const DEFAULT_TIMEOUT = 3000;

class DownloadManager<T extends IBasicResponse, Y> {
	private readonly timerId: number;
	private readonly endpoint: string;
	private readonly timeout: number;
	private readonly action: (data: T) => void;
	private isPaused: boolean;
	private params: Y | null;

	constructor(endpoint: string, timeout: number, action: (data: T) => void) {
		this.endpoint = endpoint;
		this.action = action;
		this.timeout = timeout;
		this.isPaused = false;
		this.params = null;
		this.timerId = -1;
	}

	public setParams(newParams: Y): DownloadManager<T, Y> {
		this.params = newParams;
		return this;
	}

	private async downloadAndCallAction(): Promise<void> {
		const response = await axios.get<T>(this.endpoint, {
			params: this.params
		});

		if (response.status != 200 || response.data.error) {
			console.log("Coś poszło nie tak podczas wykonywania zapytania! Szczegóły:");
			console.log("Endpoint: %s. Params: ", this.endpoint);
			console.log(this.params);
			return;
		}
		this.action(response.data);
	}

	public start(): void {
		this.downloadAndCallAction();
		window.setInterval(async (): Promise<void> => {
			await this.downloadAndCallAction();
		}, this.timeout);
	}

	public destroy(): void {
		if (this.timerId != -1)
			window.clearInterval(this.timerId);
	}

	public togglePause(): void {
		this.isPaused = !this.isPaused;
	}

	public getIsPaused(): boolean {
		return this.isPaused;
	}

	getParams = (): Y | null => this.params;
}

export {
	DownloadManager,
	DEFAULT_TIMEOUT
};