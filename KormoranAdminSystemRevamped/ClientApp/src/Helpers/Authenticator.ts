import axios from "axios";
import IValidateResponse from "../Models/Responses/IValidateResponse";

export async function validateSessionId(sessionId: string){
	try{
		const response = await axios.get<IValidateResponse>('api/Session/Validate', {
			params: {
				sessionId: sessionId
			}
		});
		return response.data.valid;
	}
	catch{
		return false;
	}
}