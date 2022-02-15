import React from "react";
import { Row, Col, Button, Table, Modal, ModalBody, ModalFooter } from "react-bootstrap";
import MatchesTable from "../../../Components/MatchesTable";
import withParams from "../../../Helpers/HOC";
import IMatch from "../../../Models/IMatch";
import IState from "../../../Models/IState";
import IDiscipline from "../../../Models/IDiscipline"
import { ElementStorage, StorageTarget } from "../../../Helpers/ElementStorage";
import ITeam from "../../../Models/ITeam";
import axios from "axios";
import ITournament from "../../../Models/ITournament";
import ModalHeader from "react-bootstrap/esm/ModalHeader";


interface IParams {
	id: number;
}

interface ICompProps {
	params: IParams;
}

interface ICompState {
	isLoading: boolean,
	selectedTeamId: number | undefined,
	editTeamName: string | undefined,
	editTeamModal: boolean,
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
	private storage: ElementStorage

	constructor(props: ICompProps) {
		super(props);
		this.isEdit = this.props.params.id > 0;
		this.storage = ElementStorage.getInstance();
		this.state = {
			isLoading: true,
			editTeamModal: false,
			selectedTeamId: undefined,
			editTeamName: undefined,
			tournamentData: {
				tournament: undefined,
				matches: [],
				teams: [],
				states: [],
				disciplines: []
			}
		};

		axios.get<ITournamentData>("http://localhost/api/tournaments/GetAllTournamentData", {
			params: {
				id: this.props.params.id
			}
		}).then((val) => {
			if (val.status != 200) {
				console.error(val.statusText);
				return;
			}
			this.setState({
				tournamentData: val.data,
				isLoading: false
			})
		}).catch(ex => console.error(ex));
	}

	render() {
		return (
			<div>
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
									<select value={this.state.selectedTeamId} size={7} style={{ width: "200px" }}
										onChange={(event) => {
											const teamName = this.state.tournamentData.teams[+event.target.value - 1].name
											this.setState((prevState) => ({
												...prevState,
												selectedTeamId: +event.target.value,
												editTeamName: teamName
											}))
										}}>
										{
											this.state.tournamentData.teams.map((team) => {
												return <option key={team.id} value={team.id}>{team.name}</option>
											})
										}
									</select>
									<div className="mt-2 flexbox inline-d">
										<Button className="me-2" variant="success">Dodaj nową</Button>
										<Button className="me-2" variant="success">Usuń zaznaczoną</Button>
										<Button disabled={this.state.selectedTeamId == undefined}
											className="me-2" variant="success"
											onClick={(event) => this.setState(prevState => ({
												...prevState,
												editTeamModal: true
											}))}>Edytuj zaznaczoną</Button>
									</div>
								</div>
								<div className="mt-3">
									<Table responsive={true}>

									</Table>
								</div>
								<h1>{this.props.params.id}</h1>
							</div>
							:
							<h1>Ładowanie</h1>
					}
				</div>
				<Modal show={this.state.editTeamModal} onShow={() => {
					if (this.state.editTeamName == undefined) {
						const index = this.state.selectedTeamId! - 1;
						const name = this.state.tournamentData.teams[index].name;
						this.setState(prevState => ({
							...prevState,
							editTeamName: name
						}));
					}
				}} onHide={() => {
					this.setState(prevState => ({
						...prevState,
						editTeamModal: false,
						editTeamName: undefined
					}));
				}}>
					<ModalHeader closeButton>
						<Modal.Title>Edytuj drużynę</Modal.Title>
					</ModalHeader>
					<ModalBody>
						<div className="flexbox inline-d">
							<span>Nazwa: </span>
							<input type="text" value={
								this.state.editTeamName == undefined ? "" :
									this.state.editTeamName
							}
								onChange={(event) => {
									if (this.state.selectedTeamId == undefined) return;
									this.setState(prevState => ({
										...prevState,
										editTeamName: event.target.value
									}))
								}}></input>
						</div>
					</ModalBody>
					<ModalFooter>
						<Button onClick={async () => {
							const newTeams = this.state.tournamentData.teams.slice();
							newTeams[this.state.selectedTeamId! - 1].name = this.state.editTeamName!;
							this.setState(prevState => ({
								...prevState,
								tournamentData: {
									...prevState.tournamentData,
									teams: newTeams
								},
								editTeamModal: false,
								editTeamName: undefined
							}));
						}}>Ok</Button>
						<Button onClick={() => {
							this.setState(prevState => ({
								...prevState,
								editTeamModal: false,
								editTeamName: undefined
							}));
						}}>Anuluj</Button>
					</ModalFooter>
				</Modal>
			</div>
		)
	}
}

export default withParams(EditTournament);