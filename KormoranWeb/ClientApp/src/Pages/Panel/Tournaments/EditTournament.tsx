import React from "react";
import { Button } from "react-bootstrap";
import IState from "../../../Models/IState";
import IDiscipline from "../../../Models/IDiscipline";
import ITeam from "../../../Models/ITeam";
import axios, { AxiosResponse } from "axios";
import ITournament from "../../../Models/ITournament";
import { binsearch } from "../../../Helpers/Essentials";
import { IBasicResponse, ICollectionResponse, ISingleItemResponse } from "../../../Models/IResponses";
import { Params } from "react-router";
import { GET_DISCIPLINES, GET_STATES, GET_TOURNAMENTS, UPDATE_TOURNAMENT } from "../../../Helpers/Endpoints";
import { WithParams } from "../../../Helpers/HOC";
import EditMatchTable from "../../../Components/EditMatchTable";
import { IUpdateMatchBasicRequest, IUpdateTournamentRequest } from "../../../Models/IRequests";

export interface IRowData {
	id: string,
	matchId: number,
	stateId: number,
	team1: number,
	team2: number,
	team1Score: number,
	team2Score: number;
}

interface ICompProps {
	params: Params<string>;
}

interface ICompState {
	isLoading: boolean,
	selectedIndex: number | undefined,
	teamModal: boolean,
	isEditModal: boolean,
	saveEnabled: boolean,
	tournament: ITournament,
	states: Array<IState>,
	disciplines: Array<IDiscipline>
}

class EditTournament extends React.Component<ICompProps, ICompState>{
	constructor(props: ICompProps) {
		super(props);
		this.state = {
			isLoading: true,
			teamModal: false,
			selectedIndex: undefined,
			isEditModal: false,
			saveEnabled: true,
			tournament: {
				tournamentId: 0,
				name: "Nowy turniej",
				matches: [],
				teams: [],
				discipline: {
					id: 0,
					name: "-"
				},
				state: {
					id: 0,
					name: "-"
				}
			},
			disciplines: [],
			states: []
		};
	}

	componentDidMount(): void {
		this.downloadOtherData()
			.then(() => this.downloadTournamentData().catch(ex => console.error(ex)))
			.catch(ex => console.error(ex));
	}

	downloadTournamentData = async (): Promise<void> => {
		console.log(this.props.params);
		const res = await axios.get<ISingleItemResponse<ITournament>>(GET_TOURNAMENTS, {
			params: {
				id: this.props.params.id
			}
		});
		console.log(res.data);
		this.setState({ tournament: res.data.data, isLoading: false });
	};

	downloadOtherData = async (): Promise<void> => {
		const disc = (await axios.get<ICollectionResponse<IDiscipline>>(GET_DISCIPLINES)).data.collection;
		const stat = (await axios.get<ICollectionResponse<IState>>(GET_STATES)).data.collection;
		this.setState(prevState => ({
			...prevState,
			disciplines: disc,
			states: stat
		}));
	};

	checkIfTeamExists = (teamId: number, teams: Array<ITeam>): boolean => {
		if (teamId == 0) return false;
		return binsearch<ITeam>(teams, (x) => {
			if (x.id < teamId) return -1;
			if (x.id == teamId) return 0;
			return 1;
		}) != undefined;
	};

	handleShow = (show: boolean, isEdit: boolean): void => {
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
	};

	renderStates = (): Array<JSX.Element> => {
		return (
			this.state.states.map((state) => {
				return (
					<option key={state.id} value={state.id}>
						{state.name}
					</option>
				);
			})
		);
	};

	renderDisciplines = (): Array<JSX.Element> => {
		return (
			this.state.disciplines.map((discipline) => {
				return (
					<option key={discipline.id} value={discipline.id}>
						{discipline.name}
					</option>
				);
			})
		);
	};

	updateTeams = async (operation: number): Promise<void> => {
		const newTeams = this.state.tournament.teams.slice();
		const newMatchesData = this.state.tournament.matches.slice();
		let shoudlBeDisabled = false;

		if (operation == -1) {
			if (this.state.selectedIndex == undefined) return;
			const index = this.state.selectedIndex;
			if (window.confirm("Czy na pewno chcesz usunąć tę drużynę")) {
				const deletedId = newTeams[index].id;
				await axios.delete("/api/teams/DeleteTeam", {
					params: {
						teamId: newTeams[index].id
					}
				});
				newTeams.splice(index, 1);
				newMatchesData.forEach(x => {
					if (x.team1.id != deletedId && x.team2.id != deletedId) return;
					if (x.team1.id == deletedId) x.team1 = {
						id: 0,
						name: "UNUSED",
						tournamentId: parseInt(this.props.params.id!)
					};
					if (x.team2.id == deletedId) x.team2 = {
						id: 0,
						name: "UNUSED",
						tournamentId: parseInt(this.props.params.id!)
					};
					shoudlBeDisabled = true;
				});
			}
			this.setState(prevState => ({
				...prevState,
				selectedIndex: undefined
			}));
		} else if (operation == 0) {
			if (this.state.selectedIndex == undefined) return;
			const index = this.state.selectedIndex;
			const newName = prompt("Nowa nazwa drużyny", newTeams[index].name);
			if (newName == null || newName == "") return;
			newTeams[index].name = newName;
			await axios.post<
				ISingleItemResponse<number>,
				AxiosResponse<ISingleItemResponse<number>, ITeam>,
				ITeam
			>("/api/teams/UpdateTeams", newTeams[index]);
		} else {
			const newName = prompt("Nazwa drużyny");
			if (newName == null || newName == "") return;

			const newTeam: ITeam = {
				id: 0,
				tournamentId: parseInt(this.props.params.id!),
				name: newName
			};
			const res = await axios.post<
				ISingleItemResponse<number>,
				AxiosResponse<ISingleItemResponse<number>, ITeam>,
				ITeam
			>("/api/teams/UpdateTeams", newTeam);
			if (!res.data.error) {
				newTeam.id = res.data.data;
				newTeams.push(newTeam);
			}
		}
		this.setState(prevState => ({
			...prevState,
			matchesData: newMatchesData,
			saveEnabled: !shoudlBeDisabled,
			tournament: {
				...prevState.tournament,
				teams: newTeams
			}
		}));
	};

	render(): JSX.Element {
		return (
			<div className="container mt-3">
				<div className="logo-container">
					<p>Edytuj turniej</p>
				</div>
				{
					!this.state.isLoading
						?
						<div>
							<div className="flexbox inline-d">
								<span>Nazwa: </span>
								<input type="text" value={this.state.tournament.name}
									onChange={(event): void => {
										this.setState(prevState => ({
											...prevState,
											saveEnabled: event.target.value != "",
											tournament: {
												...prevState.tournament,
												name: event.target.value
											}
										}));
									}}></input>
							</div>
							<div className="mt-3 flexbox inline-d">
								<span>Dyscyplina: </span>
								<select value={this.state.tournament.discipline.id}
									onChange={(event): void => {
										this.setState(prevState => ({
											...prevState,
											saveEnabled: event.target.value != "0",
											tournament: {
												...prevState.tournament,
												discipline: {
													name: "UNUSED",
													id: +event.target.value
												}
											}
										}));
									}}>
									<option value={0}>-</option>
									{this.renderDisciplines()}
								</select>
							</div>
							<div className="mt-3 flexbox inline-d">
								<span>Stan: </span>
								<select value={this.state.tournament.state.id}
									onChange={(event): void => {
										this.setState(prevState => ({
											...prevState,
											saveEnabled: event.target.value != "0",
											tournament: {
												...prevState.tournament,
												state: {
													name: "UNUSED",
													id: +event.target.value
												}
											}
										}));
									}}>
									<option value={0}>-</option>
									{this.renderStates()}
								</select>
							</div>
							<div className="mt-3">
								<span>Drużyny biorące udział: </span><br />
								<form>
									<select value={this.state.selectedIndex} size={7} style={{ width: "200px" }}
										onChange={(event): void => {
											this.setState((prevState) => ({
												...prevState,
												selectedIndex: +event.target.value,
											}));
										}}>
										{
											this.state.tournament.teams.map((team, index) => {
												return <option key={team.id} value={index}>{team.name}</option>;
											})
										}
									</select>
									<div>
										<input className="btn btn-secondary" type="reset" value="Wyczyść zaznaczenie"
											style={{ width: "200px" }}
											onClick={(): void => this.setState(prevState => ({
												...prevState,
												selectedIndex: undefined
											}))} />
									</div>
								</form>
								<div className="mt-3 flexbox inline-d">
									<Button className="me-2" variant="success"
										onClick={(): void => {
											this.updateTeams(1);
										}}>Dodaj nową</Button>
									<Button disabled={this.state.selectedIndex == undefined}
										className="me-2" variant="success"
										onClick={(): Promise<void> => this.updateTeams(-1)}>Usuń zaznaczoną</Button>
									<Button disabled={this.state.selectedIndex == undefined}
										className="me-2" variant="success"
										onClick={(): Promise<void> => this.updateTeams(0)}>Edytuj zaznaczoną</Button>
								</div>
							</div>
							{
								<div>
									<div className="mt-3">
										<EditMatchTable
											teams={this.state.tournament.teams}
											states={this.state.states}
											matches={this.state.tournament.matches}
											tournamentId={parseInt(this.props.params.id!)}
											updateMatches={(newData): void => {
												let saveEnabled = true;
												newData.forEach(row => {
													if (row.state.id == 0 || row.team1.id == 0 || row.team2.id == 0)
														saveEnabled = false;
												});
												this.setState((prevState) => ({
													...prevState,
													saveEnabled: saveEnabled,
													tournament: {
														...prevState.tournament,
														matches: newData
													}
												}));
											}} />
									</div>
									<Button className="mt-3" variant="success" disabled={!this.state.saveEnabled}
										onClick={async (): Promise<void> => {
											const matchesData: Array<IUpdateMatchBasicRequest> = [];
											this.state.tournament.matches.forEach(x => {
												matchesData.push({
													matchId: x.matchId,
													tournamentId: parseInt(this.props.params.id!),
													stateId: x.state.id,
													team1: x.team1.id,
													team2: x.team2.id,
													team1Score: x.team1Score,
													team2Score: x.team2Score
												});
											});
											console.log(this.props.params.id!);
											const body: IUpdateTournamentRequest = {
												tournamentId: parseInt(this.props.params.id!),
												newName: this.state.tournament.name,
												newStateId: this.state.tournament.state.id,
												newDisciplineId: this.state.tournament.discipline.id,
												teams: this.state.tournament.teams,
												matches: matchesData
											};
											console.log(body);
											const res = await axios.post<IBasicResponse>(UPDATE_TOURNAMENT, body);
											if (res.status != 200 || res.data.error) {
												alert("Cos poszlo nie tak! Zobacz konsole po wiecej szczegolow");
												console.log(res);
												console.log(res.statusText);
												console.log(res.data.message);
											}
											else {
												alert("Zapisano pomyslnie");
											}
										}}>ZAPISZ</Button>
								</div>
							}
						</div>
						:
						<h1>Ładowanie</h1>
				}
			</div>
		);
	}
}

export default WithParams(EditTournament);