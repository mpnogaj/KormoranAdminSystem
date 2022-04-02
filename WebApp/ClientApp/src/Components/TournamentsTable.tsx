import React from "react";
import TournamentRow from "./TournamentRow";
import { Button, Modal, Table } from "react-bootstrap";
import ITournament from "../Models/ITournament";
import IDiscipline from "../Models/IDiscipline";
import IState from "../Models/IState";
import axios from "axios";
import { IBasicResponse, ICollectionResponse } from "../Models/IResponses";
import { DownloadManager, DEFAULT_TIMEOUT } from "../Helpers/DownloadManager";
import { GET_DISCIPLINES, GET_STATES, GET_TOURNAMENTS, UPDATE_TOURNAMENT_BASIC } from "../Helpers/Endpoints";
import { binsearch } from "../Helpers/Essentials";
import IMatch from "../Models/IMatch";
import MatchesTable from "./MatchesTable";
import SelectBox, { ISelectElement } from "./SelectBox";

interface ICompState {
	tournaments: Array<ITournament>,
	disciplines: Array<IDiscipline>,
	matches: Array<IMatch>,
	states: Array<IState>,
	previewModalVisible: boolean,
	editModalVisible: boolean,
	currentTournamentId: number,
	isLoading: boolean,

	editName: string,
	editState: number,
	editDisc: number,
	editEnabled: boolean
}

interface ICompProps {
	allowEdit: boolean
}

class TournamentsTable extends React.Component<ICompProps, ICompState>{
	readonly tournamentDownloader: DownloadManager<ICollectionResponse<ITournament>, null>;
	readonly statesDownloader: DownloadManager<ICollectionResponse<IState>, null>;
	readonly disciplinesDownloader: DownloadManager<ICollectionResponse<IDiscipline>, null>;

	constructor(props: ICompProps) {
		super(props);
		this.state = {
			tournaments: [],
			matches: [],
			previewModalVisible: false,
			editModalVisible: false,
			states: [],
			disciplines: [],
			currentTournamentId: 0,
			isLoading: true,
			editDisc: 0,
			editName: "",
			editState: 0,
			editEnabled: true
		};
		this.tournamentDownloader = new DownloadManager<ICollectionResponse<ITournament>, null>(
			GET_TOURNAMENTS, DEFAULT_TIMEOUT, (data: ICollectionResponse<ITournament>) => {
				const tournament = this.getTournament(this.state.currentTournamentId, data.collection);
				const matches = this.state.previewModalVisible && tournament != undefined
					?
					tournament.matches
					:
					this.state.matches;

				this.setState((prevState) => ({
					...prevState,
					isLoading: false,
					matches: matches,
					tournaments: data.collection
				}));
			}
		);
		this.statesDownloader = new DownloadManager<ICollectionResponse<IState>, null>(
			GET_STATES, DEFAULT_TIMEOUT, (data: ICollectionResponse<IState>) => {
				this.setState((prevState) => ({
					...prevState,
					states: data.collection
				}));
			}
		);
		this.disciplinesDownloader = new DownloadManager<ICollectionResponse<IDiscipline>, null>(
			GET_DISCIPLINES, DEFAULT_TIMEOUT, (data: ICollectionResponse<IDiscipline>) => {
				this.setState((prevState) => ({
					...prevState,
					disciplines: data.collection
				}));
			}
		);
	}

	componentWillUnmount(): void {
		this.tournamentDownloader.destroy();
		this.statesDownloader.destroy();
		this.disciplinesDownloader.destroy();
	}

	componentDidMount(): void {
		this.tournamentDownloader.start();
		this.statesDownloader.start();
		this.disciplinesDownloader.start();
	}

	getTournament = (id: number, array: Array<ITournament>): ITournament | undefined =>
		binsearch(array, (x) => x.tournamentId - id);

	handleShowEdit = (tournamentId: number, isEdit: boolean): void => {
		const tournament = this.getTournament(tournamentId, this.state.tournaments);
		if (tournament == undefined) {
			console.error("Nie znaleziono turnieju o takim id");
			return;
		}
		if (isEdit) {
			this.setState({
				editName: tournament.name,
				editDisc: tournament.discipline.id,
				editState: tournament.state.id,
				currentTournamentId: tournamentId,
				editModalVisible: true
			});
		}
		else {
			this.setState({
				previewModalVisible: true,
				matches: tournament.matches,
				currentTournamentId: tournamentId
			});
		}
	};

	handleAddNew = (): void => {
		this.setState({
			editModalVisible: true,
			editName: "",
			editState: 0,
			editDisc: 0,
			editEnabled: false,
			currentTournamentId: 0
		});
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
				<Table hover bordered responsive>
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
									return (
										<TournamentRow key={val.tournamentId} tournament={val} showModalCallback={this.handleShowEdit}
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
				<Button onClick={(): void => this.handleAddNew()}>Dodaj nowy turniej</Button>
				{/* Modal podglądu */}
				<Modal show={this.state.previewModalVisible} onHide={(): void => this.handleHide(false)} size="xl">
					<Modal.Header closeButton>
						<Modal.Title>Podgląd wyników</Modal.Title>
					</Modal.Header>
					<Modal.Body>
						<MatchesTable matches={this.state.matches} />
					</Modal.Body>
				</Modal>

				{/* Modal edytowania */}
				<Modal show={this.state.editModalVisible} onHide={(): void => this.handleHide(true)}>
					<Modal.Header closeButton>
						<Modal.Title>Edytuj turniej</Modal.Title>
					</Modal.Header>
					<Modal.Body>
						<label htmlFor="newTournamentNameBox" className="me-3">Nazwa turnieju</label>
						<input value={this.state.editName} id="newTournamentNameBox" type="text"
							onChange={(e): void => {
								const newName = e.target.value;
								this.setState({
									editName: newName,
									editEnabled: newName != ""
								});
							}} />
						<SelectBox
							header="Stan"
							items={this.state.states as Array<ISelectElement>}
							addNullElement={true}
							selection={this.state.editState}
							onSelectionChanged={(newId): void => {
								this.setState({
									editState: newId,
									editEnabled: newId != 0
								});
							}}
						/>
						<SelectBox
							header="Dyscyplina"
							items={this.state.disciplines as Array<ISelectElement>}
							addNullElement={true}
							selection={this.state.editDisc}
							onSelectionChanged={(newId): void => {
								this.setState({
									editDisc: newId,
									editEnabled: newId != 0
								});
							}}
						/>
					</Modal.Body>
					<Modal.Footer>
						<Button disabled={!this.state.editEnabled} onClick={async (): Promise<void> => {
							const response = await axios.post<IBasicResponse>(UPDATE_TOURNAMENT_BASIC, {
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