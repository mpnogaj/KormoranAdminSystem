import axios from "axios";
import { IBasicResponse } from "../Models/IResponses";
import { VALIDATE } from "./Endpoints";

export async function validateSessionId(sessionId: string): Promise<boolean> {
    try {
        const response = await axios.get<IBasicResponse>(VALIDATE, {
            params: {
                sessionId: sessionId
            }
        });
        return !response.data.error;
    }
    catch {
        return false;
    }
}