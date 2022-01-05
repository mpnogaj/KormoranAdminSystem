import React from "react";
import IMatch from "../Models/IMatch";
import axios from "axios";
import {Table} from "react-bootstrap";
import MatchesRow from "./MatchRow";
import ICollectionResponse from "../Models/Responses/ICollectionResponse";

interface IProps{
	tournamentId: number;
}

interface IState{
	matches: Array<IMatch>,
	isLoading: boolean
}

class MatchesTable extends React.Component<IProps, IState>{
	private timerID: number;
	constructor(props: IProps) {
		super(props);
		this.state = {
			matches: [],
			isLoading: true,
		}
		this.timerID = 0;
		this.loadMatches().catch(ex => console.log(ex));
	}

	toggleTimer(enable: boolean){
		if(enable){
			this.timerID = window.setInterval(this.loadMatches,5000);
		}
		else {
			window.clearTimeout(this.timerID);
		}
	}

	componentWillUnmount() {
		this.toggleTimer(false);
	}

	componentDidMount() {
		this.toggleTimer(true);
	}

	loadMatches = async () => {
		const response = await axios.get <ICollectionResponse<IMatch>>("/api/Tournaments/GetMatches", {
			params: {
				"id": this.props.tournamentId
			}
		});
		if (response.status === 200) {
			this.setState<"matches">({ matches: response.data.collection, isLoading: false });
		}
	}
	
	render() {
		return (
			<Table hover={true} bordered={true}>
				<thead>
					<tr>
						<th>Identyfikator</th>
						<th>Status</th>
						<th>Drużyna 1</th>
						<th>Drużyna 2</th>
						<th>Zwycięzca</th>
						<th>Wynik</th>
					</tr>
				</thead>
				<tbody className="align-middle">
				{
					!this.state.isLoading
					?
					this.state.matches.map((val) => {
						console.log(val);
						return <MatchesRow key={val.matchId} matchId={val.matchId} state={val.state} team1={val.team1}
						                   team2={val.team2}
						                   winner={val.winner} team1Score={val.team1Score} team2Score={val.team2Score}/>
					})
					:
					<tr>
						<td style={{textAlign: "center"}} colSpan={6}>Ładowanie...</td>
					</tr>
				}
				</tbody>
			</Table>
		);
	}
}

export default MatchesTable;