/*
	Class that automaticly downloads data from endpoint every given timeout
*/

import axios from "axios";

class DownloadManager<T, Y> {

	readonly timerId: number;
	readonly endpoint: string;
	readonly timeout: number;
	readonly action: (data: T) => void;
	params: Y | null;

	constructor(endpoint: string, timeout: number, action: (data: T) => void)
	{
		this.endpoint = endpoint;
		this.action = action;
		this.timeout = timeout;
		this.params = null;
		this.timerId = -1;
	}

	setParams(newParams: Y): DownloadManager<T, Y>{
		this.params = newParams;
		return this;
	}

	start(): void{
		window.setInterval(async (): Promise<void> => {
			const response = await axios.get<T>(this.endpoint, {
				params: this.params
			});
			if(response.status != 200){
				console.log("Coś poszło nie tak podczas wykonywania zapytania! Szczegóły:");
				console.log("Endpoint: %d. Params: ", this.endpoint);
				return;
			}
			this.action(response.data);
		}, this.timeout);
	}

	destroy(): void{
		if(this.timerId != -1)
			window.clearInterval(this.timerId);
	}
}

export default DownloadManager;