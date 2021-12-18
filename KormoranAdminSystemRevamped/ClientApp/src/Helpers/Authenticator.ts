import axios from "axios";
import IValidateResponse from "../Models/Responses/IValidateResponse";

export async function validateSessionId(sessionId: string){
	const response = await axios.get<IValidateResponse>('api/Session/Validate', {
		params: {
			sessionId: sessionId
		}
	});
	console.log(response);
	return response.data.valid;
}