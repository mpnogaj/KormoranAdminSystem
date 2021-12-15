import React from "react";
import {Badge, Button} from "react-bootstrap";

interface IProps{
	id: number
	name: string,
	state: string,
	type: string,
	discipline: string,
	showModalCallback: Function;
}

class TournamentRow extends React.Component<IProps, any>{
	render() {
		return (
			<tr>
				<td>{this.props.name}</td>
				<td>
					<Badge bg="success">{this.props.state}</Badge>
				</td>
				<td>{this.props.discipline}</td>
				<td>{this.props.type}</td>
				<td>
					<Button variant="success" onClick={() => this.props.showModalCallback(this.props.id)}>Podgląd</Button>
				</td>
			</tr>
		);
	}

}
export default TournamentRow