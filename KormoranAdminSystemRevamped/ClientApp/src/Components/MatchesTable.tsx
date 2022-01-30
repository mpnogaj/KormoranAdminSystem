import React from "react";
import IMatch from "../Models/IMatch";
import { Table } from "react-bootstrap";
import MatchesRow from "./MatchRow";
import { Callback, ElementStorage, StorageTarget } from "../Helpers/ElementStorage";
import ITeam from "../Models/ITeam";
import IState from "../Models/IState";

interface ICompProps {
	states: Array<IState>
	tournamentId: number;
}

interface ICompState {
	matches: Array<IMatch>,
	teams: Array<ITeam>,
	isLoading: boolean
}

class MatchesTable extends React.Component<ICompProps, ICompState>{
	callbacks: Array<Callback> = [];
	readonly storage: ElementStorage;
	constructor(props: ICompProps) {
		super(props);
		this.state = {
			matches: [],
			teams: [],
			isLoading: true,
		};
		this.storage = ElementStorage.getInstance();
	}

	componentWillUnmount() {
		this.callbacks.forEach(callback => 
			this.storage.unsubscribe(callback)
		);
	}

	componentDidMount() {
		this.storage.updateParams({
			id: this.props.tournamentId
		}, StorageTarget.MATCHES);
		this.storage.updateParams({
			id: this.props.tournamentId
		}, StorageTarget.TEAMS);
		this.callbacks = [
			new Callback((target: StorageTarget) => {
				const data = this.storage.getData<Array<IMatch>>(target);
				console.log(data);
				if (!data.isError) {
					this.setState({
						matches: data.data!,
						isLoading: false
					});
				}
			}, StorageTarget.MATCHES),
			new Callback((target: StorageTarget) => {
				const data = this.storage.getData<Array<ITeam>>(target);
				if (!data.isError) {
					this.setState({
						teams: data.data!,
						isLoading: false
					});
				}
			}, StorageTarget.TEAMS)
		]
		this.callbacks.forEach(callback => this.storage.subscribe(callback));
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
									const teams = this.state.teams;
									val.team1 = teams[val.team1Id - 1];
									val.team2 = teams[val.team2Id - 1];
									val.winner = teams[val.winnerId - 1];
									val.state = this.props.states[val.stateId - 1];
									console.log(val);
									return <MatchesRow key={val.matchId} match={val} />
								})
							:
								<tr>
									<td style={{ textAlign: "center" }} colSpan={6}>Ładowanie...</td>
								</tr>
					}
				</tbody>
			</Table>
		);
	}
}

export default MatchesTable;