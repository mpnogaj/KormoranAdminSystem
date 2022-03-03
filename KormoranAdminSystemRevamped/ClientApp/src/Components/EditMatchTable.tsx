import React from "react";
import { Table } from "react-bootstrap";
import IMatch from "../Models/IMatch";
import ITeam from "../Models/ITeam";
import EditMatchRow from "./EditMatchRow";

interface IRowData{
	team1: number,
	team2: number,
	winner: number;
}

interface IProps{
	matches: Array<IMatch>,
	teams: Array<ITeam>
}

interface IState {
	matchesData: Array<IRowData>;
}

class EditMatchTable extends React.Component<IProps, IState>{
	constructor(props: IProps){
		super(props);
		this.state = {
			matchesData: []
		};
		const matches = props.matches;
		//kolejnosc powinna byc taka sama w petli nizej
		matches.forEach(match => {
			this.state.matchesData.push({
				team1: match.team1Id,
				team2: match.team2Id,
				winner: match.winnerId
			});
		})
	}
	
	render() {
		console.log(this.props.matches);
		return(
			<Table responsive={true}>
				<thead>
					<tr>
						<th>Id</th>
						<th>Drużyna 1</th>
						<th>Drużyna 2</th>
						<th>Wynik drużyny 1</th>
						<th>Wynik drużyny 2</th>
						<th>Zwyciezca</th>
					</tr>
				</thead>
				<tbody>
					{
						this.props.matches.map((match, index) => {
							return (
								<EditMatchRow teams={this.props.teams}
								 	match={match} id={index}
									onTeamUpdate={(targetId, team, newId) => {
										const newData = this.state.matchesData.slice();
										switch(team){
											case 1:
												newData[targetId].team1 = newId;
												break;
											case 2:
												newData[targetId].team2 = newId;
												break;
											case 1:
												newData[targetId].winner = newId;
												break;
										}
										this.setState({matchesData: newData});
									}}
								/>
							);
						})
					}
				</tbody>
			</Table>
		)
	}
}

export default EditMatchTable