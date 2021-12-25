import React from "react";
import {Badge} from "react-bootstrap";
import IState from "../Models/IState";
import ITeam from "../Models/ITeam";

interface IProps{
	matchId: number,
	state: IState,
	team1: ITeam,
	team2: ITeam
	winner: ITeam,
	team1Score: number,
	team2Score: number;
}

class TournamentRow extends React.Component<IProps, any>{
	render() {
		console.log(this.props);
		return (
			<tr>
				<td>{this.props.matchId}</td>
				<td>
					<Badge bg="success">{this.props.state.name}</Badge>
				</td>
				<td>{this.props.team1 == undefined || null ? "-" : this.props.team1.name}</td>
				<td>{this.props.team2 == undefined || null ? "-" : this.props.team2.name}</td>
				<td>{this.props.winner == null ? "-" : this.props.winner.name}</td>
				<td>{this.props.team1Score}:{this.props.team2Score}</td>
			</tr>
		);
	}

}
export default TournamentRow