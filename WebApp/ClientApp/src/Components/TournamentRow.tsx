import React from "react";
import { Badge, Button } from "react-bootstrap";
import ITournament from "../Models/ITournament";

interface IProps {
	tournament: ITournament,
	isEdit: boolean,
	showModalCallback: (tId: number, isEdit: boolean) => void;
}

class TournamentRow extends React.Component<IProps>{
	render(): JSX.Element {
		return (
			<tr>
				<td>{this.props.tournament.name}</td>
				<td>
					<Badge bg="success">{this.props.tournament.state == undefined ? "Ładowanie" : this.props.tournament.state.name}</Badge>
				</td>
				<td>{this.props.tournament.discipline == undefined ? "Ładowanie" : this.props.tournament.discipline.name}</td>
				<td>
					{
						this.props.isEdit
							?
							<div>
								<Button className="ms-3" variant="success" onClick={
									(): void => {
										this.props.showModalCallback(this.props.tournament.tournamentId, true);
									}
								}>Szybka edycja</Button>
								<Button className="ms-3" variant="success" onClick={
									(): void => {
										window.location.href = "/Panel/EditTournament/" + this.props.tournament.tournamentId;
									}
								}>Pełna edycja</Button>
							</div>
							:
							<Button variant="success" onClick={
								(): void => {
									this.props.showModalCallback(this.props.tournament.tournamentId, false);
								}
							}>Podgląd</Button>
					}
				</td>
			</tr>
		);
	}

}
export default TournamentRow;