import React from "react";
import { Row, Col, Button, Table } from "react-bootstrap";
import MatchesTable from "../../../Components/MatchesTable";
import withParams from "../../../Helpers/HOC";
import IMatch from "../../../Models/IMatch";
import IState from "../../../Models/IState";
import IDiscipline from "../../../Models/IDiscipline"
import { ElementStorage, StorageTarget } from "../../../Helpers/ElementStorage";
import ITeam from "../../../Models/ITeam";


interface IParams {
	id: number;
}

interface ICompProps {
	params: IParams;
}

interface ICompState {
	isLoading: boolean,
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
			matches: [],
			teams: [],
			states: [],
			disciplines: []
		};
	}

	render() {
		return (
			<div className="container mt-3">
				<div className="logo-container">
					<p>{this.isEdit ? "Edytuj" : "Dodaj"} turniej</p>
				</div>
				<div>
					<div className="flexbox inline-d">
						<span>Nazwa: </span>
						<input type="text" value="test123"></input>
					</div>
					<div className="mt-3 flexbox inline-d">
						<span>Dyscyplina: </span>
						<select>
							<option>Kosz</option>
							<option>Noga</option>
							<option>Siata</option>
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
			</div>
		)
	}
}

export default withParams(EditTournament);