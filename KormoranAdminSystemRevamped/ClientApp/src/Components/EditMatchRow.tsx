import React from "react";
import { binsearch } from "../Helpers/Essentials";
import ITeam from "../Models/ITeam";
import { Trash } from "react-bootstrap-icons";
import IconButton from "./IconButton";
import IState from "../Models/IState";
import { IRowData } from "../Pages/Panel/Tournaments/EditTournament";

interface ICompProps {
	match: IRowData,
	teams: Array<ITeam>,
	states: Array<IState>,
	id: number
	state: number
	team1: number,
	team2: number,
	team1Pts: number,
	team2Pts: number,
	onUpdate: (targetId: number, target: number, data: number) => void;
}

class EditMatchRow extends React.Component<ICompProps>{

	componentDidMount(): void {
		this.recalculateWinner();
	}

	getTeamName = (teamId: number): string => {
		for (let i = 0; i < this.props.teams.length; i++) {
			const element = this.props.teams[i];
			if (element.id == teamId) return element.name;
		}
		return "-";
	};

	getStateName = (stateId: number): string => {
		const res = binsearch(this.props.states, (state) => {
			if (state.id < stateId) return -1;
			if (state.id == stateId) return 0;
			return 1;
		});
		if (res == -1) return "-";
		else return this.props.states[res].name;
	};

	recalculateWinner = (): number => {
		if (this.props.team1Pts >= this.props.team2Pts)
			return this.props.team1;
		return this.props.team2;
	};

	updateTeam = (newId: number, target: number): void => {
		this.props.onUpdate(this.props.id, target, newId);
	};

	updateScore = (newScore: number, target: number): void => {
		this.props.onUpdate(this.props.id, target + 2, newScore);
	};

	updateState = (newId: number): void => {
		this.props.onUpdate(this.props.id, 6, newId);
	};

	renderTeams = (): Array<JSX.Element> => {
		return (
			this.props.teams.map((team) => {
				return (
					<option key={team.id} value={team.id}>
						{this.getTeamName(team.id)}
					</option>
				);
			})
		);
	};

	renderStates = (): Array<JSX.Element> => {
		return (
			this.props.states.map((state) => {
				return (
					<option key={state.id} value={state.id}>
						{this.getStateName(state.id)}
					</option>
				);
			})
		);
	};

	render(): JSX.Element {
		return (
			<tr className="align-middle">
				<th>{this.props.match.matchId == 0 ? "-" : this.props.match.matchId}</th>
				<th>
					<select onChange={(e): void => {
						this.updateState(parseInt(e.target.value));
					}} value={this.props.state}>
						<option value={0}>-</option>
						{this.renderStates()}
					</select>
				</th>
				<th>
					<select onChange={(e): void => {
						this.updateTeam(parseInt(e.target.value), 1);
					}} value={this.props.team1}>
						<option value={0}>-</option>
						{this.renderTeams()}
					</select>
				</th>
				<th>
					<select onChange={(e): void => {
						this.updateTeam(parseInt(e.target.value), 2);
					}} value={this.props.team2}>
						<option value={0}>-</option>
						{this.renderTeams()}
					</select>
				</th>
				<th>
					<input type="number" value={this.props.team1Pts}
						onChange={(e): void => {
							const val = parseInt(e.target.value);
							e.target.value = val.toString();
							this.updateScore(isNaN(val) ? 0 : val, 1);
						}}
					/>
				</th>
				<th>
					<input type="number" value={this.props.team2Pts}
						onChange={(e): void => {
							const val = parseInt(e.target.value);
							e.target.value = val.toString();
							this.updateScore(isNaN(val) ? 0 : val, 2);
						}}
					/>
				</th>
				<th>
					{this.getTeamName(this.recalculateWinner())}
				</th>
				<th>
					<IconButton icon={<Trash height={24} width={24} />} onClick={(): void => {
						this.props.onUpdate(this.props.id, 5, -1);
					}} />
				</th>
			</tr>
		);
	}
}

export default EditMatchRow;