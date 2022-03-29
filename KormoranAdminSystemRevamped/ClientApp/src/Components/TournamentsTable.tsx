import React from "react";
import TournamentRow from "./TournamentRow";
import { Button, Modal, Table } from "react-bootstrap";
import MatchesTable from "./MatchesTable";
import ITournament from "../Models/ITournament";
import IDiscipline from "../Models/IDiscipline";
import IState from "../Models/IState";
import axios from "axios";
import { IBasicResponse, ICollectionResponse } from "../Models/IResponses";
import DownloadManager from "../Helpers/DownloadManager";
import { GET_TOURNAMENTS } from "../Helpers/Endpoints";
import IMatch from "../Models/IMatch";
import { binsearch } from "../Helpers/Essentials";

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
	readonly tournamentDownloader: DownloadManager<ICollectionResponse<ITournament>, null>;


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
		this.tournamentDownloader = new DownloadManager<ICollectionResponse<ITournament>, null>(
			GET_TOURNAMENTS, 5000, (data) => {
				if (data.error) {
					return;
				}
				this.setState({ tournaments: data.collection });
			}
		);
	}

	componentWillUnmount(): void {
		this.tournamentDownloader.destroy();
	}

	componentDidMount(): void {
		this.tournamentDownloader.start();
	}

	getMatches = (id: number): Array<IMatch> => {
		const res = binsearch(this.state.tournaments, (x) => x.id - id);
		return this.state.tournaments[res].matches;
	};

	handleShow = (tournamentId: number, isEdit: boolean): void => {
		if (isEdit) {
			this.setState({
				editName: this.state.tournaments[tournamentId - 1].name,
				editDisc: this.state.tournaments[tournamentId - 1].discipline.id,
				editState: this.state.tournaments[tournamentId - 1].state.id,
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
	};

	handleHide = (isEdit: boolean): void => {
		if (isEdit) {
			this.setState({ editModalVisible: false });
		}
		else {
			this.setState({ previewModalVisible: false });
		}
	};

	render(): JSX.Element {
		return (
			<div>
				<div className="table-responsive">
					<Table hover bordered>
						<thead>
							<tr>
								<th>Nazwa</th>
								<th>Status</th>
								<th>Dyscyplina</th>
								<th>Akcja</th>
							</tr>
						</thead>
						<tbody className="align-middle">
							{
								!this.state.isLoading
									?
									this.state.tournaments.map((val) => {
										val.discipline = this.state.disciplines[val.discipline.id - 1];
										val.state = this.state.states[val.state.id - 1];
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
				<Modal show={this.state.previewModalVisible} onHide={(): void => this.handleHide(false)} size="xl">
					<Modal.Header closeButton>
						<Modal.Title>Podgląd wyników</Modal.Title>
					</Modal.Header>
					<Modal.Body>
						<MatchesTable matches={this.getMatches(this.state.currentTournamentId)} />
					</Modal.Body>
				</Modal>

				{/* Modal edytowania */}
				<Modal show={this.state.editModalVisible} onHide={(): void => this.handleHide(true)}>
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
											onChange={(e): void => {
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
											onChange={(e): void => {
												this.setState({
													editState: +e.target.value
												});
											}}>
											{
												this.state.states.map(state => {
													return (<option key={state.id} value={state.id}>{state.name}</option>);
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
											onChange={(e): void => {
												this.setState({
													editDisc: +e.target.value
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
						<Button onClick={async (): Promise<void> => {
							const response = await axios.post<IBasicResponse>("/api/Tournaments/UpdateTournament", {
								tournamentId: this.state.currentTournamentId,
								newName: this.state.editName,
								newStateId: this.state.editState,
								newDisciplineId: this.state.editDisc
							});
							if (response.data.error) {
								alert("Wystaplil blad");
							}
							this.handleHide(true);
						}}>Ok</Button>
						<Button onClick={(): void => this.handleHide(true)}>Anuluj</Button>
					</Modal.Footer>
				</Modal>
			</div>
		);
	}
}

export default TournamentsTable;