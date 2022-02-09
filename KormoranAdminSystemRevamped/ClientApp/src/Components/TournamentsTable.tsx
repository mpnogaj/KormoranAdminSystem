import React from "react";
import TournamentRow from "./TournamentRow";
import { Button, Modal, Table } from "react-bootstrap";
import MatchesTable from "./MatchesTable";
import ITournament from "../Models/ITournament";
import IDiscipline from "../Models/IDiscipline";
import IState from "../Models/IState";
import { Callback, ElementStorage, StorageTarget } from "../Helpers/ElementStorage";
import axios from "axios";
import IBasicResponse from "../Models/Responses/IBasicResponse";
import { ThermometerSun } from "react-bootstrap-icons";

interface ICompState {
	tournaments: Array<ITournament>,
	disciplines: Array<IDiscipline>,
	states: Array<IState>,
	previewModalVisible: boolean,
	editModalVisible: boolean,
	currentTournamentId: number,
	isLoading: boolean;

	editName: string,
	editState: number;
	editDisc: number;
}

interface ICompProps {
	allowEdit: boolean;
}

class TournamentsTable extends React.Component<ICompProps, ICompState>{
	callbacks: Array<Callback>;
	readonly storage: ElementStorage;

	constructor(props: ICompProps) {
		super(props);
		this.state = {
			tournaments: [],
			previewModalVisible: false,
			editModalVisible: false,
			states: [],
			disciplines: [],
			currentTournamentId: 0,
			isLoading: true,
			editDisc: 0,
			editName: "",
			editState: 0
		};
		this.storage = ElementStorage.getInstance();
		this.callbacks = [
			new Callback((target: StorageTarget) => {
				const data =
					this.storage.getData<Array<ITournament>>(target);
				//console.log(data);
				if (!data.isError) {
					this.setState({
						isLoading: false,
						tournaments: data.data!
					});
				}
			}, StorageTarget.TOURNAMENTS),
			new Callback((target: StorageTarget) => {
				const data =
					this.storage.getData<Array<IDiscipline>>(target);
				//console.log(data);
				if (!data.isError) {
					this.setState({
						disciplines: data.data!
					});
				}
			}, StorageTarget.DISCIPLINES),
			new Callback((target: StorageTarget) => {
				const data =
					this.storage.getData<Array<IState>>(target);
				//console.log(data);
				if (!data.isError) {
					this.setState({
						states: data.data!
					});
				}
			}, StorageTarget.STATES)
		];
	}

	updateTournaments(enable: boolean) {
		console.log("Tournament update: " + enable);
		if (enable) ElementStorage.getInstance().subscribe(this.callbacks[0]);
		else ElementStorage.getInstance().unsubscribe(this.callbacks[0]);
	}

	componentWillUnmount() {
		this.callbacks.forEach(callback => ElementStorage.getInstance().unsubscribe(callback));
	}

	componentDidMount() {
		this.callbacks.forEach(callback => ElementStorage.getInstance().subscribe(callback));
	}

	handleShow = (tournamentId: number, isEdit: boolean) => {
		if (isEdit) {
			this.setState({
				editName: this.state.tournaments[tournamentId - 1].name,
				editDisc: this.state.tournaments[tournamentId - 1].disciplineId,
				editState: this.state.tournaments[tournamentId - 1].stateId,
				currentTournamentId: tournamentId,
				editModalVisible: true
			});
		}
		else {
			this.setState({
				previewModalVisible: true,
				currentTournamentId: tournamentId
			});
		}
		this.updateTournaments(false);
	}

	handleHide = (isEdit: boolean) => {
		if (isEdit) {
			this.setState({ editModalVisible: false });
		}
		else {
			this.setState({ previewModalVisible: false });
		}
		this.updateTournaments(true);
	}

	render() {
		return (
			<div>
				<div className="table-responsive">
					<Table hover bordered>
						<thead>
							<tr>
								<th>Nazwa</th>
								<th>Status</th>
								<th>Dyscyplina</th>
								<th>Typ turnieju</th>
								<th>Akcja</th>
							</tr>
						</thead>
						<tbody className="align-middle">
							{
								!this.state.isLoading
									?
									this.state.tournaments.map((val) => {
										val.discipline = this.state.disciplines[val.disciplineId - 1];
										val.state = this.state.states[val.stateId - 1];
										return (
											<TournamentRow key={val.id} tournament={val} showModalCallback={this.handleShow}
												isEdit={this.props.allowEdit} />
										);
									})
									:
									<tr>
										<td style={{ textAlign: "center" }} colSpan={5}>Ładowanie...</td>
									</tr>
							}
						</tbody>
					</Table>
				</div>
				{/* Modal podglądu */}
				<Modal show={this.state.previewModalVisible} onHide={() => this.handleHide(false)} size="xl">
					<Modal.Header closeButton>
						<Modal.Title>Podgląd wyników</Modal.Title>
					</Modal.Header>
					<Modal.Body>
						<MatchesTable isEdit={this.props.allowEdit} tournamentId={this.state.currentTournamentId} />
					</Modal.Body>
				</Modal>

				{/* Modal edytowania */}
				<Modal show={this.state.editModalVisible} onHide={() => this.handleHide(true)}>
					<Modal.Header closeButton>
						<Modal.Title>Edytuj turniej</Modal.Title>
					</Modal.Header>
					<Modal.Body>
						<table>
							<tbody>
								<tr>
									<td>
										<label htmlFor="newTournamentNameBox" className="me-3">Nazwa turnieju</label>
									</td>
									<td>
										<input value={this.state.editName} id="newTournamentNameBox" type="text"
											onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
												this.setState({ editName: e.target.value });
											}} />
									</td>
								</tr>
								<tr className="mt-3">
									<td>
										<label htmlFor="newTournamentState" className="me-3 mt-2">Stan</label>
									</td>
									<td>
										<select value={this.state.editState} id="newTournamentState"
											onChange={(event: React.ChangeEvent<HTMLSelectElement>) => {
												this.setState({
													editState: +event.target.value
												});
											}}>
											{
												this.state.states.map(state => {
													return (<option key={state.id} value={state.id}>{state.name}</option>)
												})
											}
										</select>
									</td>
								</tr>
								<tr className="mt-3">
									<td>
										<label htmlFor="newTournamentDyscipline" className="me-3">Dyscyplina</label>
									</td>
									<td>
										<select value={this.state.editDisc} id="newTournamentDyscipline"
											onChange={(event: React.ChangeEvent<HTMLSelectElement>) => {
												this.setState({
													editDisc: +event.target.value
												});
											}}>
											{
												this.state.disciplines.map(disc => {
													return (<option key={disc.id} value={disc.id}>{disc.name}</option>);
												})
											}
										</select>
									</td>
								</tr>
							</tbody>
						</table>
					</Modal.Body>
					<Modal.Footer>
						<Button onClick={async () => {
							const response = await axios.post<IBasicResponse>("/api/Tournaments/UpdateTournament", {
								tournamentId: this.state.currentTournamentId,
								newName: this.state.editName,
								newStateId: this.state.editState,
								newDisciplineId: this.state.editDisc
							});
							this.handleHide(true);
						}}>Ok</Button>
						<Button onClick={() => this.handleHide(true)}>Anuluj</Button>
					</Modal.Footer>
				</Modal>
			</div>
		)
	}
}

export default TournamentsTable