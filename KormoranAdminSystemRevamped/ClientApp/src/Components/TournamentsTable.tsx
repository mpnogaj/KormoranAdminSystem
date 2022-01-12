import React from "react";
import TournamentRow from "./TournamentRow";
import {Button, Modal, Table} from "react-bootstrap";
import MatchesTable from "./MatchesTable";
import axios from "axios";
import ITournament from "../Models/ITournament";
import IDiscipline from "../Models/IDiscipline";
import IState from "../Models/IState";
import ICollectionResponse from "../Models/Responses/ICollectionResponse";
import {Callback, ElementStorage, StorageTarget} from "../Helpers/ElementStorage";

interface ICompState{
	tournaments: Array<ITournament>,
	disciplines: Array<IDiscipline>,
	states: Array<IState>,
	previewModalVisible: boolean,
	editModalVisible: boolean,
	currentTournamentId : number,
	isLoading: boolean;
}

interface ICompProps{
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
			states: [],
			disciplines: [],
			editModalVisible: true,
			currentTournamentId: 0,
			isLoading: true
		};
		this.storage = ElementStorage.getInstance();
		this.callbacks = [
			new Callback((target: StorageTarget) => {
				const data = 
					this.storage.getData<Array<ITournament>>(target);
				console.log(data);
				if(!data.isError){
					this.setState({
						isLoading: false,
						tournaments: data.data!
					});
				}
			}, StorageTarget.TOURNAMENTS),
			new Callback((target: StorageTarget) => {
				const data = 
					this.storage.getData<Array<IDiscipline>>(target);
				console.log(data);
				if(!data.isError){
					this.setState({
						disciplines: data.data!
					});
				}
			}, StorageTarget.DISCIPLINES),
			new Callback((target: StorageTarget) => {
				const data = 
					this.storage.getData<Array<IState>>(target);
				console.log(data);
				if(!data.isError){
					this.setState({
						states: data.data!
					});
				}
			}, StorageTarget.STATES)
		];
	}

	updateTournaments(enable: boolean){
		if(enable) ElementStorage.getInstance().subscribe(this.callbacks[0]);
		else ElementStorage.getInstance().unsubscribe(this.callbacks[0]);
	}

	componentWillUnmount() {
		this.callbacks.forEach(callback => ElementStorage.getInstance().subscribe(callback));
	}

	componentDidMount() {
		this.callbacks.forEach(callback => ElementStorage.getInstance().subscribe(callback));
	}

	handleShow = (tournamentId: number, isEdit: boolean) => {
		if(isEdit){
			this.setState({
				editModalVisible: true
			})
		}
		else{
			this.setState({
				previewModalVisible: true,
				currentTournamentId: tournamentId
			});
		}
		this.updateTournaments(false);
	}
	
	render(){
		return(
			<div>
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
											           allowEdit={this.props.allowEdit}/>
									);
								})
							:
								<tr>
									<td style={{textAlign: "center"}} colSpan={5}>Ładowanie...</td>
								</tr>
					}
					</tbody>
				</Table>
				{/* Modal podglądu */}
				<Modal show={this.state.previewModalVisible} onHide={() => {
					this.setState({
						previewModalVisible: false
					});
					this.updateTournaments(true);
				}} size="xl">
					<Modal.Header closeButton>
						<Modal.Title>Podgląd wyników</Modal.Title>
					</Modal.Header>
					<Modal.Body>
						<MatchesTable tournamentId={this.state.currentTournamentId}/>
					</Modal.Body>
				</Modal>

				{/* Modal edytowania */}
				<Modal show={this.state.editModalVisible} onHide={() => {
					this.setState({
						editModalVisible: false
					});
					this.updateTournaments(true);
				}}>
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
										<input id="newTournamentNameBox" type="text"/>
									</td>
								</tr>
								<tr className="mt-3">
									<td>
										<label htmlFor="newTournamentNameBox" className="me-3 mt-2">Stan</label>
									</td>
									<td>
										<select id="newTournamentDyscipline">
											<option>Koniec</option>
											<option>Trwa</option>
											<option>20 pompek!</option>
										</select> 
									</td>
								</tr>
								<tr className="mt-3">
									<td>
										<label htmlFor="newTournamentDyscipline" className="me-3">Dyscyplina</label>
									</td>
									<td>
										<select id="newTournamentDyscipline">
											<option>Dobra chłopaki dzisiaj siata</option>
											<option>Dupa321</option>
											<option>Gargamel</option>
										</select> 
									</td>
								</tr>
							</tbody>
						</table>
					</Modal.Body>
					<Modal.Footer>
						<Button>Ok</Button>
						<Button onClick={() => this.setState({editModalVisible: false})}>Anuluj</Button>
					</Modal.Footer>
				</Modal>
			</div>
		)
	}
}

export default TournamentsTable