import IBasicResponse from "./IBasicResponse";

interface ILoginResponse extends IBasicResponse{
	sessionId: string;
}

export default ILoginResponse;