import React from "react";
import TournamentRow from "./TournamentRow";
import {Button, Modal, Table} from "react-bootstrap";
import MatchesTable from "./MatchesTable";
import axios from "axios";
import ITournamentsResponse from "../Models/Responses/ITournamentsResponse";
import ITournament from "../Models/ITournament";

interface IState{
	tournaments: Array<ITournament>,
	modalVisible: boolean,
	editModalVisible: boolean,
	currentTournamentId : number,
	isLoading: boolean;
}

interface IProps{
	allowEdit: boolean;
}

class TournamentsTable extends React.Component<IProps, IState>{

	private timerID: number;
	constructor(props: IProps) {
		super(props);
		this.state = {
			tournaments: [],
			modalVisible: false,
			editModalVisible: true,
			currentTournamentId: 0,
			isLoading: true
		};
		this.timerID = 0;
		this.loadTournaments = this.loadTournaments.bind(this);
		this.loadTournaments().catch(ex => console.log(ex));
	}

	toggleTimer(enable: boolean){
		if(enable){
			this.timerID = window.setInterval(this.loadTournaments,5000);
		}
		else {
			window.clearTimeout(this.timerID);
		}
	}

	componentWillUnmount() {
		this.toggleTimer(false);
	}

	componentDidMount() {
		this.toggleTimer(true);
	}

	async loadTournaments(){
		const response = await axios.get<ITournamentsResponse>("/api/Tournaments/GetTournaments", {
			params: []
		});
		console.log(response);
		if(response.status === 200){
			this.setState({
				tournaments: response.data.tournaments,
				isLoading: false
			});
		}
		else{
			this.setState({
				tournaments: [],
				isLoading: false
			})
		}
	}

	handleShow = (tournamentId: number, isEdit: boolean) => {
		this.setState({
			...this.state,
			modalVisible: true,
			currentTournamentId: tournamentId
		});
		this.toggleTimer(false);
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
				<Modal show={this.state.modalVisible} onHide={() => {
					this.setState({
						modalVisible: false
					});
					this.toggleTimer(true);
				}} size="xl">
					<Modal.Header closeButton>
						<Modal.Title>Podgląd wyników</Modal.Title>
					</Modal.Header>
					<Modal.Body>
						<MatchesTable tournamentId={this.state.currentTournamentId}/>
					</Modal.Body>
				</Modal>
				<Modal show={this.state.editModalVisible} onHide={() => {
					this.setState({
						editModalVisible: false
					});
					this.toggleTimer(true);
				}}>
					<Modal.Header closeButton>
						<Modal.Title>Edytuj turniej</Modal.Title>
					</Modal.Header>
					<Modal.Body>
						<div>
							<label htmlFor="newTournamentNameBox" className="me-3">Nazwa turnieju</label>
							<input id="newTournamentNameBox" type="text"/>
						</div>
						<div>
							
						</div>
					</Modal.Body>
					<Modal.Footer>
						<Button>Ok</Button>
						<Button>Anuluj</Button>
					</Modal.Footer>
				</Modal>
			</div>
		)
	}
}

export default TournamentsTable