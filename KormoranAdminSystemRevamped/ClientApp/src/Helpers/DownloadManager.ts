/*
	Class that automaticly downloads data from endpoint every given timeout
*/

import axios from "axios";

const DEFAULT_TIMEOUT = 3000;

class DownloadManager<T, Y> {

	readonly timerId: number;
	readonly endpoint: string;
	readonly timeout: number;
	readonly action: (data: object) => void;
	params: Y | null;

	constructor(endpoint: string, timeout: number, action: (data: object) => void) {
		this.endpoint = endpoint;
		this.action = action;
		this.timeout = timeout;
		this.params = null;
		this.timerId = -1;
	}

	setParams(newParams: Y): DownloadManager<T, Y> {
		this.params = newParams;
		return this;
	}

	downloadAndCallAction = async (): Promise<void> => {
		const response = await axios.get<object>(this.endpoint, {
			params: this.params
		});
		
		if (response.status != 200) {
			console.log("Coś poszło nie tak podczas wykonywania zapytania! Szczegóły:");
			console.log("Endpoint: %s. Params: ", this.endpoint);
			console.log(this.params);
			return;
		}
		this.action(response.data as object);
	};

	start(): void {
		this.downloadAndCallAction();
		window.setInterval(async (): Promise<void> => {
			await this.downloadAndCallAction();
		}, this.timeout);
	}

	destroy(): void {
		if (this.timerId != -1)
			window.clearInterval(this.timerId);
	}
}

export {
	DownloadManager,
	DEFAULT_TIMEOUT
};