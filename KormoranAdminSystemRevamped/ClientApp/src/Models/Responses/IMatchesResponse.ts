import IMatch from "../IMatch";

interface IMatchesResponse{
	isError: boolean,
	matches: Array<IMatch>;
}

export default IMatchesResponse;