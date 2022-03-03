import React from "react";
import IMatch from "../Models/IMatch";
import ITeam from "../Models/ITeam";

interface IProps{
	match: IMatch,
	teams: Array<ITeam>,
	id: number
	onTeamUpdate: (targetId: number, targetTeam: number, newId: number) => void;
}

interface IState {
	team1: number,
	team2: number,
	team1Pts: number,
	team2Pts: number,
	winner: number;
}

class EditMatchRow extends React.Component<IProps, IState>{

	constructor(props: IProps){
		super(props);
		this.state = {
			team1: props.match.team1Id,
			team2: props.match.team2Id,
			team1Pts: props.match.team1Score,
			team2Pts: props.match.team2Score,
			winner: props.match.winnerId
		};
	}

	getTeamName = (teamId: number) : string => {
		for (let i = 0; i < this.props.teams.length; i++) {
			const element = this.props.teams[i];
			if(element.id == teamId) return element.name;
		}
		return "NULL";
	}

	recalculateWinner = (): number => {
		if(this.state.team1Pts >= this.state.team2Pts) 
			return this.state.team1;
		return this.state.team2;
	}

	render() {
		return (
			<tr>
				<th>{this.props.match.matchId}</th>
				<th>
					<select onChange={(e) => {
						this.setState({team1: +e.target.value});
						this.props.onTeamUpdate(this.props.id, 1, +e.target.value);
					}} value={this.state.team1}>
						{
							this.props.teams.map((team) => {
								return (
									<option key={team.id} value={team.id}>
										{this.getTeamName(team.id)}
									</option>
								);
							})
						}
					</select>
				</th>
				<th>
					<select onChange={(e) => {
						this.setState({team2: +e.target.value});
						this.props.onTeamUpdate(this.props.id, 2, +e.target.value);
					}} value={this.state.team2}>
						{
							this.props.teams.map((team) => {
								return (
									<option key={team.id} value={team.id}>
										{this.getTeamName(team.id)}
									</option>
								);
							})
						}
					</select>
				</th>
				<th>
					<input type="text" value={this.state.team1Pts}
						onChange={(event) => {
							this.setState({ team1Pts: +event.target.value });
						}}
					/>
				</th>
				<th>
					<input type="text" value={this.state.team2Pts}
						onChange={(event) => {
							this.setState({ team2Pts: +event.target.value });
						}}
					/>
				</th>
				<th>
					{this.getTeamName(this.recalculateWinner())}
				</th>
			</tr>
		);
	}
}

export default EditMatchRow;