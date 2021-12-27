import ILog from "../ILog";

interface ILogsResponse {
	error: boolean,
	message: string,
	logEntries: Array<ILog>;
}

export default ILogsResponse;