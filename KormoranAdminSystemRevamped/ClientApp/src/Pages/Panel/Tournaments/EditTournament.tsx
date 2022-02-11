import React from "react";
import { Row, Col, Button, Table } from "react-bootstrap";
import MatchesTable from "../../../Components/MatchesTable";
import withParams from "../../../Helpers/HOC";
import IMatch from "../../../Models/IMatch";
import IState from "../../../Models/IState";
import IDiscipline from "../../../Models/IDiscipline"
import { ElementStorage, StorageTarget } from "../../../Helpers/ElementStorage";
import ITeam from "../../../Models/ITeam";
import axios from "axios";
import ITournament from "../../../Models/ITournament";


interface IParams {
	id: number;
}

interface ICompProps {
	params: IParams;
}

interface ICompState {
	isLoading: boolean,
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
										{/*iks de*/}
										this.setState(prevState => ({
											...prevState,
											tournamentData: {
												...prevState.tournamentData,
												tournament: {
													...prevState.tournamentData.tournament!,
													name: event.target.value
												}
											}
									}))}}></input>
							</div>
							<div className="mt-3 flexbox inline-d">
								<span>Dyscyplina: </span>
								<select>
									{
										this.state.tournamentData.disciplines.map((disc) => {
											return <option key={disc.id} value={disc.id}>{disc.name}</option>
										})
									}
								</select>
							</div>
							<div className="mt-3 flexbox inline-d">
								<span>Stan: </span>
								<select>
									<option>Do rozpoczęcia</option>
									<option>Trwa</option>
									<option>Zakończony</option>
								</select>
							</div>
							<div className="mt-3">
								<span>Drużyny biorące udział: </span><br />
								<select size={5} style={{ width: "200px" }}>
									<option value="test">test</option>
									<option value="test">test</option>
									<option value="test">test</option>
									<option value="test">test</option>
									<option value="test">test</option>
								</select>
								<div className="mt-2 flexbox inline-d">
									<Button className="me-2" variant="success">Dodaj nową</Button>
									<Button className="me-2" variant="success">Usuń zaznaczoną</Button>
									<Button className="me-2" variant="success">Edytuj zaznaczoną</Button>
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
		)
	}
}

export default withParams(EditTournament);