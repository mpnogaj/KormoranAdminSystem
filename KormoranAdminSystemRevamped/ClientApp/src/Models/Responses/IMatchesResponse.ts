import IMatch from "../IMatch";

interface IMatchesResponse{
	error: boolean,
	matches: Array<IMatch>;
}

export default IMatchesResponse;