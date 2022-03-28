import React from "react";
import {Badge} from "react-bootstrap";
import { binsearch } from "../Helpers/Essentials";
import IMatch from "../Models/IMatch";
import IState from "../Models/IState";

interface IProps{
	match: IMatch,
	states: Array<IState>
}

class TournamentRow extends React.Component<IProps, any>{

	getStateName = () : string => {
		for (let i = 0; i < this.props.states.length; i++) {
			const element = this.props.states[i];
			if(element.id == this.props.match.stateId) return element.name;
		}
		return "NULL";
	}

	render() {
		return (
			<tr>
				<td>{this.props.match.matchId}</td>
				<td>
					<Badge bg="success">{this.getStateName()}</Badge>
				</td>
				<td>{this.props.match.team1 == undefined || null ? "-" : this.props.match.team1.name}</td>
				<td>{this.props.match.team2 == undefined || null ? "-" : this.props.match.team2.name}</td>
				<td>{this.props.match.winner == null ? "-" : this.props.match.winner.name}</td>
				<td>{this.props.match.team1Score}:{this.props.match.team2Score}</td>
			</tr>
		);
	}

}
export default TournamentRow