import axios from "axios";
import IValidateResponse from "../Models/Responses/IValidateResponse";

export function validateSessionId(sessionId: string){
	try{
		axios.get<IValidateResponse>('api/Session/Validate', {
			params: {
				sessionId: sessionId
			}
		}).then(response => {
			return response.data.valid;
		}).catch(() => {
			return false;
		});
	}
	catch{
		return false;
	}
}