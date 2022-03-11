import React from "react";
import { Button, Table } from "react-bootstrap";
import withParams from "../../../Helpers/HOC";
import IMatch from "../../../Models/IMatch";
import IState from "../../../Models/IState";
import IDiscipline from "../../../Models/IDiscipline"
import ITeam from "../../../Models/ITeam";
import axios from "axios";
import ITournament from "../../../Models/ITournament";
import { binsearch } from "../../../Helpers/Essentials";
import EditMatchTable from "../../../Components/EditMatchTable";
import { nanoid } from "nanoid";


export interface IRowData {
	id: string,
	matchId: number,
	stateId: number,
	team1: number,
	team2: number,
	team1Score: number,
	team2Score: number;
}


interface IParams {
	id: number;
}

interface ICompProps {
	params: IParams;
}

interface ICompState {
	isLoading: boolean,
	selectedIndex: number | undefined,
	teamTempName: string | undefined,
	teamModal: boolean,
	isEditModal: boolean,
	matchesData: Array<IRowData>,
	tournamentData: ITournamentData;

}

interface ITournamentData {
	tournament: ITournament | undefined
	matches: Array<IMatch>,
	teams: Array<ITeam>,
	states: Array<IState>,
	disciplines: Array<IDiscipline>;
}

class EditTournament extends React.Component<ICompProps, ICompState>{
	private isEdit: boolean;
	/*
		This serves as dummy id for new, no submitted to server teams
		After creating new team increase this value by 1 (important!)
		After saving changes server will calculate "deltaId" for first element 
		and change all dummy id's (for matches too)
	*/
	private tempId: number;

	constructor(props: ICompProps) {
		super(props);
		this.isEdit = this.props.params.id > 0;
		this.tempId = 1000000000 + 7;
		this.state = {
			isLoading: true,
			teamModal: false,
			selectedIndex: undefined,
			teamTempName: undefined,
			isEditModal: false,
			tournamentData: {
				tournament: {
					id: 0,
					name: "Nowy turniej",
					stateId: 0,
					state: undefined,
					teams: undefined,
					discipline: undefined,
					disciplineId: 0,
					tournamentType: "R",
					tournamentTypeShort: "RR"
				},
				matches: [],
				teams: [],
				states: [],
				disciplines: []
			},
			matchesData: []
		};
	}

	downloadEditTorunament = async () => {
		const res = await axios.get<ITournamentData>("http://localhost/api/tournaments/GetAllTournamentData", {
			params: {
				id: this.props.params.id
			}
		});
		const strippedMatches: Array<IRowData> = [];
		res.data.matches.forEach(match => {
			strippedMatches.push({
				id: nanoid(),
				matchId: match.matchId,
				stateId: match.stateId,
				team1: match.team1Id,
				team2: match.team2Id,
				team1Score: match.team1Score,
				team2Score: match.team2Score
			});
		});
		this.setState({tournamentData: res.data, matchesData: strippedMatches, isLoading: false});
	}

	downloadAddTournament = async () => {
		const disc = (await axios.get<Array<IDiscipline>>("api/tournaments/GetDiscipl")).data;
		const stat = (await axios.get<Array<IState>>("api/Disciplines/GetStates")).data;
		this.setState(prevState => ({
			...prevState,
			isLoading: false,
			tournamentData: {
				...prevState.tournamentData,
				disciplines: disc,
				states: stat
			}
		}));
	}

	componentDidMount() {
		if(this.isEdit) this.downloadEditTorunament().catch(ex => console.error(ex));
		else this.downloadAddTournament().catch(ex => console.error(ex));
	}

	findTeamIndex = (teamId: number): number => {
		return binsearch<ITeam>(this.state.tournamentData.teams, (x) => {
			if (x.id < teamId) return -1;
			if (x.id == teamId) return 0;
			return 1;
		});
	}

	handleShow = (show: boolean, isEdit: boolean) => {
		if (show) {
			this.setState(prevState => ({
				...prevState,
				teamModal: show,
				isEditModal: isEdit
			}));
		}
		else this.setState(prevState => ({
			...prevState,
			teamModal: show
		}));
	}

	updateTeams = async (operation: number) => {

		const newTeams = this.state.tournamentData.teams.slice();
		const index = this.state.selectedIndex!

		if (operation == -1) {
			if (window.confirm("Czy na pewno chcesz usunąć tę drużynę"))
				newTeams.splice(index, 1);
			this.setState(prevState => ({
				...prevState,
				selectedIndex: undefined
			}));
		} else if (operation == 0) {
			const newName = prompt("Nowa nazwa drużyny", newTeams[index].name);
			if (newName == null) return;
			newTeams[index].name = newName
		} else {
			const newName = prompt("Nazwa drużyny");
			if (newName == null) return;
			newTeams.push({
				id: this.tempId++,
				name: newName
			});
		}

		this.setState(prevState => ({
			...prevState,
			tournamentData: {
				...prevState.tournamentData,
				teams: newTeams
			}
		}));
	}

	render() {
		return (
			<div className="container mt-3">
				<div className="logo-container">
					<p>{this.isEdit ? "Edytuj" : "Dodaj"} turniej</p>
				</div>
				{
					!this.state.isLoading
						?
						<div>
							<div className="flexbox inline-d">
								<span>Nazwa: </span>
								<input type="text" value={this.state.tournamentData.tournament!.name}
									onChange={(event) => {
										{/*iks de*/ }
										this.setState(prevState => ({
											...prevState,
											tournamentData: {
												...prevState.tournamentData,
												tournament: {
													...prevState.tournamentData.tournament!,
													name: event.target.value
												}
											}
										}))
									}}></input>
							</div>
							<div className="mt-3 flexbox inline-d">
								<span>Dyscyplina: </span>
								<select value={this.state.tournamentData.tournament!.disciplineId}
									onChange={(event) => {
										this.setState(prevState => ({
											...prevState,
											tournamentData: {
												...prevState.tournamentData,
												tournament: {
													...prevState.tournamentData.tournament!,
													disciplineId: +event.target.value
												}
											}
										}));
									}}>
									{
										this.state.tournamentData.disciplines.map((disc) => {
											return <option key={disc.id} value={disc.id}>{disc.name}</option>
										})
									}
								</select>
							</div>
							<div className="mt-3 flexbox inline-d">
								<span>Stan: </span>
								<select value={this.state.tournamentData.tournament!.stateId}
									onChange={(event) => {
										this.setState(prevState => ({
											...prevState,
											tournamentData: {
												...prevState.tournamentData,
												tournament: {
													...prevState.tournamentData.tournament!,
													stateId: +event.target.value
												}
											}
										}));
									}}>
									{
										this.state.tournamentData.states.map((state) => {
											return <option key={state.id} value={state.id}>{state.name}</option>
										})
									}
								</select>
							</div>
							<div className="mt-3">
								<span>Drużyny biorące udział: </span><br />
								<form>
									<select value={this.state.selectedIndex} size={7} style={{ width: "200px" }}
										onChange={(event) => {
											this.setState((prevState) => ({
												...prevState,
												selectedIndex: +event.target.value,
											}))
										}}>
										{
											this.state.tournamentData.teams.map((team, index) => {
												return <option key={team.id} value={index}>{team.name}</option>
											})
										}
									</select>
									<div>
										<input className="btn btn-secondary" type="reset" value="Wyczyść zaznaczenie"
											style={{ width: "200px" }}
											onClick={() => this.setState(prevState => ({
												...prevState,
												selectedIndex: undefined
											}))} />
									</div>
								</form>
								<div className="mt-3 flexbox inline-d">
									<Button className="me-2" variant="success"
										onClick={() => this.updateTeams(1)}>Dodaj nową</Button>
									<Button disabled={this.state.selectedIndex == undefined}
										className="me-2" variant="success"
										onClick={() => this.updateTeams(-1)}>Usuń zaznaczoną</Button>
									<Button disabled={this.state.selectedIndex == undefined}
										className="me-2" variant="success"
										onClick={() => this.updateTeams(0)}>Edytuj zaznaczoną</Button>
								</div>
							</div>
							<div className="mt-3">
								<EditMatchTable
									teams={this.state.tournamentData.teams}
									states={this.state.tournamentData.states}
									matchesData={this.state.matchesData}
									updateMatches={(newData) => {
										this.setState((prevState) => ({
											...prevState,
											matchesData: newData
										}));
									}} />
							</div>
						</div>
						:
						<h1>Ładowanie</h1>
				}
			</div>
		)
	}
}

export default withParams(EditTournament);