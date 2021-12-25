interface ILog {
	id: number,
	level: number,
	date: Date,
	author: string,
	action: string;
}

export default ILog;