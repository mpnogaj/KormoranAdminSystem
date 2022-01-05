import axios from "axios";
import IBasicResponse from "../Models/Responses/IBasicResponse";

export async function validateSessionId(sessionId: string){
	try{
		const response = await axios.get<IBasicResponse>('/api/Session/Validate', {
			params: {
				sessionId: sessionId
			}
		});
		return !response.data.error;
	}
	catch{
		return false;
	}
}