import React from "react";
import {Badge, Button} from "react-bootstrap";
import ITournament from "../Models/ITournament";

interface IProps{
	tournament: ITournament,
	allowEdit: boolean,
	showModalCallback: Function,
}

class TournamentRow extends React.Component<IProps, any>{
	render() {
		return (
			<tr>
				<td>{this.props.tournament.name}</td>
				<td>
					<Badge bg="success">{this.props.tournament.state.name}</Badge>
				</td>
				<td>{this.props.tournament.discipline.name}</td>
				<td>{this.props.tournament.tournamentType}</td>
				<td>
					<Button variant="success" onClick={() => this.props.showModalCallback(this.props.tournament.id, false)}>Podgląd</Button>
					{
						this.props.allowEdit
							? 
								<Button className="ms-3" variant="success" onClick={
									() => this.props.showModalCallback(this.props.tournament.id, true)
								}>Edytuj</Button> 
							:
								null
					}
				</td>
			</tr>
		);
	}

}
export default TournamentRow