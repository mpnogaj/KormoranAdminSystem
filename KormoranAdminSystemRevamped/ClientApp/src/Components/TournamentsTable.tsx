import React from "react";
import TournamentRow from "./TournamentRow";
import {Modal} from "react-bootstrap";
import MatchesTable from "./MatchesTable";
import axios from "axios";
import ITournamentsResponse from "../Models/Responses/ITournamentsResponse";
import ITournament from "../Models/ITournament";

interface IState{
	tournaments: Array<ITournament>,
	modalVisible: boolean,
	currentTournamentId : number;
}

interface IProps{}


class TournamentsTable extends React.Component<IProps, IState>{

	private timerID: number;
	constructor(props: any) {
		super(props);
		this.state = {
			tournaments: [],
			modalVisible: false,
			currentTournamentId: 0
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
		const response = await axios.get<ITournamentsResponse>("api/Tournaments/GetTournaments", {
			params: []
		});
		console.log("update");
		if(response.status === 200){
			this.setState<"tournaments">({tournaments: response.data.tournaments});
		}
	}

	handleShow = (tournamentId: number) => {
		this.setState<"modalVisible">({modalVisible: true});
		this.setState<"currentTournamentId">({currentTournamentId: tournamentId});
		this.toggleTimer(false);
	}
	handleClose = () => {
		this.setState<"modalVisible">({modalVisible: false});
		this.toggleTimer(true);
	}
	
	
	render(){
		return(
			<div>
				<table className="table table-hover table-bordered">
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
					this.state.tournaments.length > 0
						?
						this.state.tournaments.map((val) => {
							return (
								<TournamentRow key={val.id} id={val.id} name={val.name} state={val.state.name} type={val.tournamentType}
								               discipline={val.discipline.name} showModalCallback={this.handleShow}/>
							);
						})
						:
						<tr>
							<td style={{textAlign: "center"}} colSpan={5}>Ładowanie...</td>
						</tr>
				}
				</tbody>
			</table>
				<Modal show={this.state.modalVisible} onHide={this.handleClose}
		            size="xl">
			<Modal.Header closeButton>
				<Modal.Title>Podgląd wyników</Modal.Title>
			</Modal.Header>
			<Modal.Body>
				<MatchesTable tournamentId={this.state.currentTournamentId}/>
			</Modal.Body>
		</Modal>
			</div>
		)
	}
}

export default TournamentsTable