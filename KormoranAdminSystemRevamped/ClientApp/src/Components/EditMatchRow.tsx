import React from "react";
import { binsearch } from "../Helpers/Essentials";
import ITeam from "../Models/ITeam";
import {IRowData} from "../Components/EditMatchTable";
import { Trash } from "react-bootstrap-icons";
import IconButton from "./IconButton";
interface IProps{
	match: IRowData,
	teams: Array<ITeam>,
	id: number
	onUpdate: (targetId: number, target: number, data: number) => void;
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
			team1: props.match.team1,
			team2: props.match.team2,
			team1Pts: props.match.team1Score,
			team2Pts: props.match.team2Score,
			winner: 0
		};
	}

	componentDidMount(){
		if(!this.checkIfTeamExists(this.state.team1))
			this.setState({team1: 0});
		if(!this.checkIfTeamExists(this.state.team2))
			this.setState({team2: 0});
		this.recalculateWinner();
	}

	getTeamName = (teamId: number) : string => {
		for (let i = 0; i < this.props.teams.length; i++) {
			const element = this.props.teams[i];
			if(element.id == teamId) return element.name;
		}
		return "-";
	}

	checkIfTeamExists = (teamId: number) : boolean => {
		//default team
		if(teamId == 0) return true;
		const index = binsearch(this.props.teams, t => {
			if(t.id == teamId) return 0;
			if(t.id < teamId) return -1;
			return 1;
		});
		return index != -1;
	}

	recalculateWinner = (): number => {
		if(this.state.team1Pts >= this.state.team2Pts) 
			return this.state.team1;
		return this.state.team2;
	}

	updateTeam = (newId: number, target: number) => {
		if(target == 1) this.setState({team1: newId});
		else this.setState({team2: newId});
		this.props.onUpdate(this.props.id, target, newId);
	}

	renderTeams = (teams: Array<ITeam>) : Array<JSX.Element> => { 
		return (
			teams.map((team) => {
				return (
					<option key={team.id} value={team.id}>
						{this.getTeamName(team.id)}
					</option>
				);
			})
		);
	}

	render() {
		return (
			<tr className="align-middle">
				<th>{this.props.match.matchId == 0 ? '-' : this.props.match.matchId}</th>
				<th>
					<select onChange={(e) => {
						this.updateTeam(parseInt(e.target.value), 1);
					}} value={this.state.team1}>
						<option value={0}>-</option>
						{this.renderTeams(this.props.teams)}
					</select>
				</th>
				<th>
					<select onChange={(e) => {
						this.updateTeam(parseInt(e.target.value), 2);
					}} value={this.state.team2}>
						<option value={0}>-</option>
						{this.renderTeams(this.props.teams)}
					</select>
				</th>
				<th>
					<input type="number" value={this.state.team1Pts}
						onChange={(event) => {
							this.setState({ team1Pts: parseInt(event.target.value) });
						}}
					/>
				</th>
				<th>
					<input type="number" value={this.state.team2Pts}
						onChange={(event) => {
							this.setState({ team2Pts: parseInt(event.target.value) });
						}}
					/>
				</th>
				<th>
					{this.getTeamName(this.recalculateWinner())}
				</th>
				<th>
					<IconButton icon={<Trash height={24} width={24}/>} onClick={() => {
						this.props.onUpdate(this.props.id, 5, -1);
					}}/>
				</th>
			</tr>
		);
	}
}

export default EditMatchRow;